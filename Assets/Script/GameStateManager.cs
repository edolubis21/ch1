using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerChoiceData
{
    public int chair = 0;         // "premium" atau "murah"
    public int employee = 0;       // "strategis" atau "biasa"
    public int machine = 0;          // 1 atau 2
    public int accessoris = 0;      // 5_000_000 atau 10_000_000
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public PlayerChoiceData playerChoices;
    private string path;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            path = Application.persistentDataPath + "/player_choices.json";
            LoadChoices();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveChoices()
    {
        string json = JsonUtility.ToJson(playerChoices, true);
        File.WriteAllText(path, json);
        LoadChoices();

    }

    public void LoadChoices()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerChoices = JsonUtility.FromJson<PlayerChoiceData>(json);
        }
        else
        {
            playerChoices = new PlayerChoiceData();
            Debug.Log("Data baru dibuat (belum ada file sebelumnya)");
        }
    }

    public void ResetChoices()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Data pilihan dihapus.");
        }
        playerChoices = new PlayerChoiceData();
    }


      public int GetChoiceByKey(string key)
    {
        return key switch
        {
            "chair" => playerChoices.chair,
            "employee" => playerChoices.employee,
            "machine" => playerChoices.machine,
            "accessoris" => playerChoices.accessoris,
            _ => 0 // default jika key tidak cocok
        };
    }
}
