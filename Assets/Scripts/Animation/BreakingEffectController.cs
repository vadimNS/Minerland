using UnityEngine;

public class BreakingEffectController : MonoBehaviour
{
    [SerializeField] private Sprite[] crackSprites;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Hide();
    }

    public void Show(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetProgress(float progress)
    {
        if (crackSprites.Length == 0) return;

        int index = Mathf.FloorToInt(progress * (crackSprites.Length - 1));
        index = Mathf.Clamp(index, 0, crackSprites.Length - 1);
        spriteRenderer.sprite = crackSprites[index];
    }
}
