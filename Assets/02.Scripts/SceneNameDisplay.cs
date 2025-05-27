using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI sceneNameText;

    void Update()
    {
        if (sceneNameText != null)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            sceneNameText.text = currentSceneName;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI가 할당되지 않았습니다.");
        }
    }
}
