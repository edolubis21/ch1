using UnityEngine;

public class SpriteClickHandler : MonoBehaviour
{
    public ModalManager modalManager;
    public string itemKey; // Misalnya: "alat", "pegawai", "lokasi"

    void OnMouseDown()
    {
        if (modalManager != null && !modalManager.IsModalActive)
        {
            modalManager.ShowModal(itemKey);
        }
    }
}
