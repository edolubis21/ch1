using UnityEngine;

public class ChangeableSprite : MonoBehaviour
{
    public GameObject murahPrefab;
    public GameObject premiumPrefab;
    public GameObject highlightPrefab;

    [Tooltip("chair, machine, employee, accessoris")]
    public string itemKey = "chair";

    public Transform visualContainer; // assign ini ke GameObject kosong child (misal: Visual)

    void Start()
    {

        if (GameStateManager.Instance == null || GameStateManager.Instance.playerChoices == null)
            return;
        string tipe = GetChoiceFromKey(itemKey);
        ReplaceWithPrefab(tipe);
    }

    public void SetTo()
    {
        if (GameStateManager.Instance == null || GameStateManager.Instance.playerChoices == null)
            return;

        string tipe = GetChoiceFromKey(itemKey);
        ReplaceWithPrefab(tipe);
    }

    public void ReplaceWithPrefab(string tipe)
    {
        GameObject prefabToSpawn = tipe switch
        {
            "murah" => murahPrefab,
            "premium" => premiumPrefab,
            "highlight" => highlightPrefab,
            _ => null
        };

        if (prefabToSpawn != null && visualContainer != null)
        {
            // Hapus anak sebelumnya jika ada
            foreach (Transform child in visualContainer)
            {
                Destroy(child.gameObject);
            }

            // Buat prefab baru sebagai child dari visual container
            Instantiate(prefabToSpawn, visualContainer.position, Quaternion.identity, visualContainer);
        }
        else
        {
            Debug.LogWarning($"Prefab atau visual container belum diset untuk tipe: {tipe}");
        }
    }

    private string GetChoiceFromKey(string key)
    {
        var data = GameStateManager.Instance.playerChoices;

        int value = key switch
        {
            "chair" => data.chair,
            "machine" => data.machine,
            "employee" => data.employee,
            "accessoris" => data.accessoris,
            _ => 0
        };

        return value switch
        {
            1 => "murah",
            2 => "premium",
            _ => "highlight"
        };
    }
}
