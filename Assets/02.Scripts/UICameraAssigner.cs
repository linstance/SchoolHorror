using UnityEngine;

public class UICameraAssigner : MonoBehaviour
{
    private Canvas uiCanvas;
    private Camera lastAssignedCamera;

    void Awake()
    {
        uiCanvas = GetComponent<Canvas>();

        if (uiCanvas.renderMode != RenderMode.ScreenSpaceCamera)
        {
            Debug.LogWarning("[UICameraAssigner] Canvas Render Mode가 Screen Space - Camera가 아닙니다.");
        }
    }

    void Update()
    {
        Camera currentMainCamera = Camera.main;

        // 1. worldCamera가 null인 경우도 체크
        if ((uiCanvas.worldCamera == null || currentMainCamera != lastAssignedCamera) && currentMainCamera != null)
        {
            uiCanvas.worldCamera = currentMainCamera;
            lastAssignedCamera = currentMainCamera;
         
        }
        else if (currentMainCamera == null)
        {
            Debug.LogWarning("[UICameraAssigner] Camera.main이 null입니다. 카메라 태그 확인 필요");
        }
    }
}
