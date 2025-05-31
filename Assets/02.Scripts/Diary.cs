using UnityEngine;

public class Diary : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject DiaryPopUp;



    public void DiaryPopUpTrue()
    {
        DiaryPopUp.SetActive(true);
    }

    public void DiaryPopUpFalse()
    {
        DiaryPopUp.SetActive(false);
    }

}
