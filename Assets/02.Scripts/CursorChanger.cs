using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorTagChecker : MonoBehaviour
{
    public Texture2D BasicCursor;
    public Texture2D DoorCursor;
    public Texture2D ObjectCursor;
    private bool isCursorChanged = false;

    private void Start()
    {
        Cursor.SetCursor(BasicCursor, Vector2.zero, CursorMode.Auto);
        isCursorChanged = false;
    }

    void Update()
    {
        GameObject uiHit = GetUIObjectUnderCursor();

        if (uiHit != null && uiHit.CompareTag("Object"))
        {
            if (!isCursorChanged)
            {
                Cursor.SetCursor(ObjectCursor, Vector2.zero, CursorMode.Auto);
                isCursorChanged = true;
            }
        }
        else if (uiHit != null && uiHit.CompareTag("Door"))
        {
            Cursor.SetCursor(DoorCursor, Vector2.zero, CursorMode.Auto);
            isCursorChanged = true;
        }
        else
        {
            if (isCursorChanged)
            {
                Cursor.SetCursor(BasicCursor, Vector2.zero, CursorMode.Auto);
                isCursorChanged = false;
            }
        }
    }

    GameObject GetUIObjectUnderCursor()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject != null)
            {
                return result.gameObject;
            }
        }

        return null;
    }
}
