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
        Debug.Log("Pilihan pemain disimpan ke: " + path);
                  Debug.Log("==============" );
        LoadChoices();
          Debug.Log("==============" );
    }

    public void LoadChoices()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerChoices = JsonUtility.FromJson<PlayerChoiceData>(json);
            Debug.Log("playerChoices.chair: " + playerChoices.chair);
            Debug.Log("playerChoices.employee: " + playerChoices.employee);
            Debug.Log("playerChoices.machine: " + playerChoices.machine);
            Debug.Log("playerChoices.accessoris: " + playerChoices.accessoris);
            Debug.Log("Pilihan pemain dimuat dari: " + path);
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
}
