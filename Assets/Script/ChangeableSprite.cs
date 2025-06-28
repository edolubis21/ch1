using UnityEngine;

public class ChangeableSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite murahSprite;
    public Sprite premiumSprite;

    public void SetTo(string tipe)
    {
        if (tipe == "murah")
        {
            spriteRenderer.sprite = murahSprite;
        }
        else if (tipe == "premium")
        {
            spriteRenderer.sprite = premiumSprite;
        }
    }
}
