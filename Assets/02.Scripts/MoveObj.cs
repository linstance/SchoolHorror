using UnityEngine;

public class MoveObj : MonoBehaviour
{
    private static MoveObj instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // 루트 오브젝트 찾기
            GameObject rootObj = gameObject.transform.root.gameObject;

            DontDestroyOnLoad(rootObj);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
}
