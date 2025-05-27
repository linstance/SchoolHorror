using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TextManager : MonoBehaviour
{

    public TMP_Text NameBar;
    public TMP_Text TextBar;
    public GameObject Bar;

    private int ClickCount = 0;
    private List<Dictionary<string, object>> Main_Date;
    private GameManager gm;

    private string currentText;
    private string currentName;
    private string currentActive;

    private void Awake()
    {
        Main_Date = CSVReader.Read("Sheet/Main");
        gm = GameManager.Instance;
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

        if (ClickCount < Main_Date.Count)
        {
            UpdateGameData(ClickCount);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickCount += 1;
        }

        ActiveUpdater();
        TimeStoper();
    }

    private void TimeStoper()
    {
        if (currentActive == "TRUE")
        {
            gm.PauseTimer(true);
        }
           
    }

    private void ActiveUpdater()
    {
        if(currentActive == "FALSE")
        {
            Bar.SetActive(false);
            gm.PauseTimer(false);

            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.CompareTag("Object") && obj.scene.isLoaded)
                {
                    obj.SetActive(true);
                }
            }
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
