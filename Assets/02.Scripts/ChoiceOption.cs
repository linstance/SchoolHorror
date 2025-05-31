using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ChoiceOption : MonoBehaviour, IPointerClickHandler
{
    public int choiceIndex;              // 선택지 번호 (0~3)
    public QuestionController controller;

    private TextMeshProUGUI tmpText;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.OnChoiceSelected(this);
    }

    public void SetColor(Color color)
    {
        if (tmpText != null)
            tmpText.color = color;
    }

    public void ResetColor(Color defaultColor)
    {
        if (tmpText != null)
            tmpText.color = defaultColor;
    }
}
