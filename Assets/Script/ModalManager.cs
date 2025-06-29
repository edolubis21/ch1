using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ModalManager : MonoBehaviour
{
    public GameObject modalPanel;
    public Button expensiveButton;
    public Button cheapButton;
    public Button closeButton;

    public CameraTouchDrag cameraTouchDrag; // ✅ Referensi CameraTouchDrag
    public CanvasGroup canvasGroup; // ✅ Tambahkan CanvasGroup di Inspector

    public bool IsModalActive => modalPanel.activeSelf;
    private Vector3 originalScale = Vector3.one;

    private string currentItemKey; 

        private Action onChoiceCallback;

    void Start()
    {
        if (canvasGroup != null)
        {
            modalPanel.transform.localScale = Vector3.zero;
            canvasGroup.alpha = 0;
            modalPanel.SetActive(false);
        }
    }

    public void ShowModal(string itemKey, Action onChoice)
    {
        onChoiceCallback = onChoice;

        currentItemKey = itemKey; // Simpan kunci item yang sedang ditampilkan
        modalPanel.SetActive(true);

        if (cameraTouchDrag != null)
            cameraTouchDrag.isDraggingEnabled = false; // ✅ Nonaktifkan drag kamera

        expensiveButton.onClick.RemoveAllListeners();
        cheapButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        expensiveButton.onClick.AddListener(() =>
        {
            SavePlayerChoice("premium");
            onChoiceCallback?.Invoke(); 
            StartCoroutine(HideModal());
        });

        cheapButton.onClick.AddListener(() =>
        {
            SavePlayerChoice("murah");
            onChoiceCallback?.Invoke(); 
            StartCoroutine(HideModal());
        });

        closeButton.onClick.AddListener(() =>
        {
            StartCoroutine(HideModal());
        });

        StopAllCoroutines();
        StartCoroutine(AnimateModalIn());
    }

    private void SavePlayerChoice(string value)
    {
        var data = GameStateManager.Instance.playerChoices;

        switch (currentItemKey)
        {
            case "chair":
                data.chair = (value == "premium") ? 2 : 1;
                break;
            case "employee":
                data.employee = (value == "premium") ? 2 : 1;
                break;
            case "machine":
                data.machine = (value == "premium") ? 2 : 1;
                break;
            case "accessoris":
                data.accessoris = (value == "premium") ? 2 : 1;
                break;
        }

        GameStateManager.Instance.SaveChoices();
    }

    private IEnumerator AnimateModalIn()
    {
        float duration = 0.3f;
        float time = 0f;

        modalPanel.transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float smoothT = t * t * (3f - 2f * t); // Smoothstep

            canvasGroup.alpha = Mathf.Lerp(0, 1, smoothT);
            modalPanel.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, smoothT);

            yield return null;
        }

        canvasGroup.alpha = 1;
        modalPanel.transform.localScale = originalScale;
    }

    private IEnumerator HideModal()
    {
        float duration = 0.2f;
        float time = 0f;

        Vector3 startScale = modalPanel.transform.localScale;
        float startAlpha = canvasGroup.alpha;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float smoothT = t * t * (3f - 2f * t); // Smoothstep

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, smoothT);
            modalPanel.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, smoothT);

            yield return null;
        }

        canvasGroup.alpha = 0;
        modalPanel.transform.localScale = Vector3.zero;
        modalPanel.SetActive(false);

        if (cameraTouchDrag != null)
            cameraTouchDrag.isDraggingEnabled = true; // ✅ Aktifkan kembali drag kamera
    }
}
