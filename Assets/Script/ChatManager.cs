using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject chatItemPrefab;
    public Transform contentTransform;
    public ScrollRect scrollRect;

    [Header("Chat Settings")]
    public float delay = 1.5f;

    private ChatSaveManager chatSaveManager;
    private int currentIndex = 0;

    private List<ChatMessage> messages;

    private void Start()
    {

        chatSaveManager = FindObjectOfType<ChatSaveManager>();
        messages = new List<ChatMessage>
        {
            new ChatMessage("Hari ini... ", true, true),
            new ChatMessage("akhirnya kafe impianku dibuka!", false, true),
            new ChatMessage("Setelah bertahun-tahun belajar kuliner dan mengumpulkan modal, ini saatnya mewujudkan mimpi!", false, true),
            new ChatMessage("Dengan pelayanan yang ramah dan tempat yang nyaman, pasti aku akan untung besar! HAHAHA!", false, true),
            new ChatMessage("Tapi tunggu dulu, Aruna…", true, false),
            new ChatMessage("Semangatmu memang membara, tapi bisnis bukan cuma soal rasa—ini juga soal angka. ", false, false),
            new ChatMessage("Ingat, tidak semua harus mahal. Yang penting berfungsi, efisien, dan bertahan lama. Salah kelola di awal bisa bikin kamu tekor di tengah jalan. ", false, false),
            new ChatMessage("Tips Keuangan: Prinsip 50/30/20!\n\n50% untuk kebutuhan pokok: sewa, listrik, bahan baku, gaji pegawai.\n\n30% untuk keinginan: dekorasi lucu, alat keren, diskon promosi.\n\n20% untuk tabungan: dana darurat dan peluang tak terduga.", false, false),
            new ChatMessage("Hmm… jadi, sebelum aku mulai belanja alat dan sewa tempat, aku harus pikirkan baik-baik.", true, true),
            new ChatMessage("Bisa hemat di awal = bisa bertahan lebih lama.", false, true),
            new ChatMessage("Oke! Waktunya menyusun strategi belanja. Modalku Rp50.000.000, aku nggak boleh asal pakai!", false, true),
        };


    currentIndex = chatSaveManager.LoadChatState();

    StartCoroutine(ShowMessagesFrom(currentIndex));
    }

    IEnumerator ShowMessagesFrom(int startIndex)
    {
        float screenWidth = Screen.width;
        float targetWidth = screenWidth * 0.75f;

        for (int i = startIndex; i < messages.Count; i++)
        {
        GameObject newChat = Instantiate(chatItemPrefab, contentTransform);
         ChatMessage msg = messages[i];

            // Atur HorizontalLayoutGroup untuk kanan/kiri dan reverse arrangement
            HorizontalLayoutGroup hlg = newChat.GetComponent<HorizontalLayoutGroup>();
            if (hlg != null)
            {
                if (msg.IsMe)
                {
                    hlg.childAlignment = TextAnchor.UpperRight;
                    hlg.reverseArrangement = true;
                }
                else
                {
                    hlg.childAlignment = TextAnchor.UpperLeft;
                    hlg.reverseArrangement = false;
                }
            }

            // Cari elemen-elemen penting
            Transform profileObj = newChat.transform.Find("ProfileContainer/Profile");
            RectTransform textContainer = newChat.transform.Find("TextContainer")?.GetComponent<RectTransform>();
            RectTransform background = newChat.transform.Find("TextContainer/TextBackground")?.GetComponent<RectTransform>();
            RectTransform chatText = newChat.transform.Find("TextContainer/TextBackground/Chat")?.GetComponent<RectTransform>();

            // Set lebar maksimal text container, background, dan text
            ApplyWidthSettings(textContainer, targetWidth);
            ApplyWidthSettings(background, targetWidth);
            ApplyWidthSettings(chatText, targetWidth);

            // Set teks chat
            TMP_Text textComponent = chatText?.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = msg.Teks;
            }

            // Show/hide profil dan atur padding text container
            if (profileObj != null)
            {
                profileObj.gameObject.SetActive(msg.IsShowProfile);
            }

            if (textContainer != null)
            {
                var layoutGroup = textContainer.GetComponent<LayoutGroup>();
                if (layoutGroup != null)
                {
                    if (!msg.IsShowProfile)
                    {
                        // Padding top 0 kalau profil tidak ditampilkan
                        layoutGroup.padding.top = 0;
                    }
                    else
                    {
                        // Padding top default 10 (ubah sesuai prefabmu)
                        layoutGroup.padding.top = 10;
                    }
                    layoutGroup.SetLayoutHorizontal();
                    layoutGroup.SetLayoutVertical();
                }
            }
            chatSaveManager.SaveChatState(i + 1); 

            yield return new WaitForSeconds(delay);
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainScene");
    }

    void ApplyWidthSettings(RectTransform rt, float width)
    {
        if (rt == null) return;

        // Pastikan anchor & pivot agar tidak auto-stretch
        rt.anchorMin = new Vector2(0, rt.anchorMin.y);
        rt.anchorMax = new Vector2(0, rt.anchorMax.y);
        rt.pivot = new Vector2(0, rt.pivot.y);

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}

public class ChatMessage
{
    public string Teks { get; set; }
    public bool IsShowProfile { get; set; }
    public bool IsMe { get; set; }

    public ChatMessage(string teks, bool isShowProfile, bool isMe)
    {
        Teks = teks;
        IsShowProfile = isShowProfile;
        IsMe = isMe;
    }
}
