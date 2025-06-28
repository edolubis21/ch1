using System.IO;
using UnityEngine;

public class ChatSaveManager : MonoBehaviour
{
    private string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/chat_state.json";
    }

    public void SaveChatState(int index)
    {
        ChatState state = new ChatState { currentChatIndex = index };
        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(path, json);
        Debug.Log("Chat state disimpan: index " + index);
    }

    public int LoadChatState()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ChatState state = JsonUtility.FromJson<ChatState>(json);
            Debug.Log("Chat state dimuat: index " + state.currentChatIndex);
            return state.currentChatIndex;
        }
        return 0;
    }

    public void ResetChatState()
    {
        if (File.Exists(path)) File.Delete(path);
    }
}
