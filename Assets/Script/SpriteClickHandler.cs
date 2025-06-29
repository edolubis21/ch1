using UnityEngine;
using System;

public class SpriteClickHandler : MonoBehaviour
{
    public ModalManager modalManager;
    public string itemKey; // Misalnya: "alat", "pegawai", "lokasi"

 void OnMouseDown()
{
    if (modalManager != null && !modalManager.IsModalActive)
    {
        var cs = GetComponent<ChangeableSprite>();
        var item = GameStateManager.Instance.GetChoiceByKey(itemKey); // Ambil pilihan dari GameStateManager
        if (item != 0)
        {
            Debug.LogWarning($"Tidak ada pilihan untuk kunci: {itemKey}");
            return;
        }
        modalManager.ShowModal(itemKey, () =>
        {

            // Panggil SetTo dengan hasil pilihan
            
            Debug.Log(cs);

            if (cs != null)
            {
                cs.SetTo(); // ✅ Ubah sprite ke murah/premium
            }
        });
    }
}
}
