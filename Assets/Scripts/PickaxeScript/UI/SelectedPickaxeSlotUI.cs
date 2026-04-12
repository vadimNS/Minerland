using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedPickaxeSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;

    public void SetPickaxe(PickaxeData data)
    {
        if (data == null)
        {
            Clear();
            return;
        }

        icon.sprite = data.sprite;
        icon.gameObject.SetActive(data.sprite != null);

        if (nameText != null)
            nameText.text = data.type.ToString();
    }

    public void Clear()
    {
        if (icon != null)
            icon.gameObject.SetActive(false);

        if (nameText != null)
            nameText.text = "";
    }
}
