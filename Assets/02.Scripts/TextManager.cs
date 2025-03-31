using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{

    public Text NameBar;
    public Text TextBar;
    public GameObject Bar;

    private int ClickCount = 0;
    private List<Dictionary<string, object>> Main_Date;


    private string currentText;
    private string currentName;
    private string currentActive;

    private void Awake()
    {
        Main_Date = CSVReader.Read("Sheet/Main");
    }


    void Start()
    {
        UpdateGameData(0);
    }

    // Update is called once per frame
    void Update()
    {
        NameBar.text = currentName;
        TextBar.text = currentText;

        Debug.Log(currentActive);

        if (ClickCount < Main_Date.Count)
        {
            UpdateGameData(ClickCount);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickCount += 1;
        }

        ActiveUpdater();
    }

    private void ActiveUpdater()
    {
        if(currentActive == "FALSE")
        {
            Bar.SetActive(false);
            GameObject.Find("ClassroomObj").transform.GetChild(0).gameObject.SetActive(true);
        }
    }



    private void UpdateGameData(int index)
    {
        currentName = Main_Date[index]["Name"].ToString();
        currentText = Main_Date[index]["Text"].ToString();
        currentActive = Main_Date[index]["Active"].ToString();
    }

    public void next()
    {
        ClickCount += 1;
    }
}
