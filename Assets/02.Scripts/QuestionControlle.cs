using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuestionController : MonoBehaviour
{
    [Header("선택지 옵션들")]
    public ChoiceOption[] choiceOptions;    // 4개의 ChoiceOption 컴포넌트

    [Header("문제 정보")]
    public int correctAnswerIndex = -1;     // 정답 인덱스 (안전상 -1로 초기화)
    public int questionIndex;               // 씬별 문제 번호

    private Color normalColor = Color.black;
    private Color correctColor = Color.gray;
    private Color wrongColor = Color.red;

    private bool questionEnded = false;

    void Start()
    {
        Debug.Log($"[QuestionController] Start(): questionIndex={questionIndex}, correctAnswerIndex={correctAnswerIndex}");

        // 정답 인덱스 유효성 검사
        if (correctAnswerIndex < 0 || correctAnswerIndex >= choiceOptions.Length)
        {
            Debug.LogError($"[QuestionController] 잘못된 correctAnswerIndex: {correctAnswerIndex}, choiceOptions 길이: {choiceOptions.Length}");
            return;
        }

        // 이미 문제를 클리어한 상태라면
        if (GameManager.Instance != null && GameManager.Instance.classroomClears.Length > questionIndex)
        {
            if (GameManager.Instance.classroomClears[questionIndex])
            {
                questionEnded = true;

                // 모든 선택지 비활성화
                foreach (var choice in choiceOptions)
                {
                    var selectable = choice.GetComponent<Selectable>();
                    if (selectable != null)
                        selectable.interactable = false;
                }

                // 정답 선택지 회색으로 표시
                choiceOptions[correctAnswerIndex].SetColor(correctColor);
                return;
            }
        }

        // 초기화: 모든 선택지 기본색으로 초기화 및 인터랙션 설정
        foreach (var choice in choiceOptions)
        {
            choice.controller = this;
            choice.ResetColor(normalColor);
            choice.GetComponent<TextMeshProUGUI>().color = normalColor;

            var selectable = choice.GetComponent<Selectable>();
            if (selectable != null)
                selectable.interactable = true;
        }

        questionEnded = false;
    }

    public void OnChoiceSelected(ChoiceOption selectedOption)
    {
        if (questionEnded)
            return;

        int selectedIndex = selectedOption.choiceIndex;

        if (selectedIndex == correctAnswerIndex)
        {
            // 정답 처리
            selectedOption.SetColor(correctColor);
            questionEnded = true;

            foreach (var choice in choiceOptions)
            {
                var selectable = choice.GetComponent<Selectable>();
                if (selectable != null)
                    selectable.interactable = false;
            }

            if (GameManager.Instance != null && GameManager.Instance.classroomClears.Length > questionIndex)
            {
                GameManager.Instance.classroomClears[questionIndex] = true;
                Debug.Log($"[QuestionController] 문제 {questionIndex} 클리어 저장됨");
            }
        }
        else
        {
            // 오답 처리
            StartCoroutine(WrongAnswerRoutine(selectedOption));

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ApplyPenalty(1);
            }
        }
    }

    private IEnumerator WrongAnswerRoutine(ChoiceOption wrongOption)
    {
        wrongOption.SetColor(wrongColor);

        var selectable = wrongOption.GetComponent<Selectable>();
        if (selectable != null)
            selectable.interactable = false;

        yield return new WaitForSeconds(0.5f);

        wrongOption.ResetColor(normalColor);
        if (selectable != null)
            selectable.interactable = true;
    }
}
