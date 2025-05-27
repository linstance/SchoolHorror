using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleSceneManager : MonoBehaviour
{

    public GameObject PopUpSetting;

    public void StartGame()
    {
        SceneManager.LoadScene("교실 1-1");
    }


    public void OutGame()
    {
        Application.Quit();
    }
   
    public void OpenPopUp()
    {
        PopUpSetting.SetActive(true);
    }


    public void ClosePopUp()
    {
        PopUpSetting.SetActive(false);
    }

}
