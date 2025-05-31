using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    GameManager gm;


    void Start()
    {
        gm = GameManager.Instance;
    }


    public void Classroom1_1()
    {
        SceneManager.LoadScene("교실 1-1");
    }

    public void Classroom1_2()
    {
        SceneManager.LoadScene("교실 1-2");
    }

    public void Classroom1_3()
    {
        SceneManager.LoadScene("교실 1-3");
    }

    public void Classroom1_4()
    {
        SceneManager.LoadScene("교실 1-4");
    }

    public void I_shapedCorridor1()
    {
        SceneManager.LoadScene("일자형 복도1");
    }

    public void I_shapedCorridor2()
    {
        SceneManager.LoadScene("일자형 복도2");
    }

    public void I_shapedCorridor3()
    {
        SceneManager.LoadScene("일자형 복도3");
    }

    public void I_shapedCorridor4()
    {
        SceneManager.LoadScene("일자형 복도4");
    }

    public void I_shapedCorridor5()
    {
        SceneManager.LoadScene("일자형 복도5");
    }

    public void I_shapedCorridor6()
    {
        SceneManager.LoadScene("일자형 복도6");
    }


    public void T_shapedCorridor1()
    {
        SceneManager.LoadScene("T자형 복도1");
    }

    public void T_shapedCorridor2()
    {
        SceneManager.LoadScene("T자형 복도2");
    }

    public void T_shapedCorridor3()
    {
        SceneManager.LoadScene("T자형 복도3");
    }


    public void Blackbord1()
    {
        SceneManager.LoadScene("칠판1");
    }

    public void Blackbord2()
    {
        SceneManager.LoadScene("칠판2");
    }

    public void Blackbord3()
    {
        SceneManager.LoadScene("칠판3");
    }

    public void Blackbord4()
    {
        SceneManager.LoadScene("칠판4");
    }


    public void nursesOffice()
    {
        GameManager.Instance.TryLoadSceneWithItem("양호실", "보건실 열쇠");
    }

    public void Porch()
    {
        SceneManager.LoadScene("현관");
    }

    public void Restroom()
    {
        SceneManager.LoadScene("화장실");
    }

    public void Ending()
    {
        if (gm.isGoodClear == true)
        {
            GameManager.Instance.TryLoadSceneWithItem("해피엔딩", "현관 열쇠");
        }

        if(gm.isBadClear == true)
        {
            GameManager.Instance.TryLoadSceneWithItem("배드엔딩", "현관 열쇠");
        }
    }
}
