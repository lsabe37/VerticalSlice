using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ResponseUnderline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text textComponent;
    private FontStyles originalStyle;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            originalStyle = textComponent.fontStyle;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.fontStyle |= FontStyles.Bold;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.fontStyle = originalStyle;
        }
    }
}
