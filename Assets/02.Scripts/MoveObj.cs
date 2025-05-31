using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObj : MonoBehaviour
{
    private static MoveObj instance;

    // 특정 씬에서만 이 오브젝트를 파괴하고 싶을 때
    public string[] forbiddenScenes;  // 이 씬들에서는 삭제

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // 최상위 루트 오브젝트만 DontDestroyOnLoad 적용
            GameObject rootObj = transform.root.gameObject;
            DontDestroyOnLoad(rootObj);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var name in forbiddenScenes)
        {
            if (scene.name == name)
            {
                Debug.Log($"[MoveObj] '{scene.name}' 씬에 진입했으므로 오브젝트 제거");
                Destroy(gameObject);
                return;
            }
        }
    }
}
