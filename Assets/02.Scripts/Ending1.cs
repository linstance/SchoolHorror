using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Ending : MonoBehaviour
{
    private int ClickCount = 0;
    private List<Dictionary<string, object>> Main_Date;

    public TMP_Text TextBar;
    public TMP_Text NameBar;

    private string currentText;
    private string currentName;

    void Awake()
    {
        Main_Date = CSVReader.Read("Sheet/Ending1");
    }

    void Start()
    {
        ShowCurrentLine();
    }

    // 클릭해서 다음 대사 진행
    public void next()
    {
        ClickCount++;

        if (ClickCount >= Main_Date.Count)
        {
            // 엔딩 텍스트가 모두 끝나면 타이틀로 이동
            SceneManager.LoadScene("Title");
        }
        else
        {
            ShowCurrentLine();
        }
    }

    void ShowCurrentLine()
    {
        currentName = Main_Date[ClickCount]["Name"].ToString();
        currentText = Main_Date[ClickCount]["Text"].ToString();

        NameBar.text = currentName;
        TextBar.text = currentText;
    }
}
