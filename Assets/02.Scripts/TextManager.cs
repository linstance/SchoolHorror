using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    public TMP_Text NameBar;
    public TMP_Text TextBar;          // 메시지 출력용 TMP_Text
    public GameObject Bar;            // 텍스트 박스 전체 오브젝트 (활성/비활성 제어용)

    private int ClickCount = 0;
    private List<Dictionary<string, object>> Main_Date;
    private GameManager gm;

    private string currentText;
    private string currentName;
    private string currentActive;

    private bool isShowingMessage = false; // 메시지 출력 중 상태 플래그

    private void Awake()
    {
        Main_Date = CSVReader.Read("Sheet/Main");
        gm = GameManager.Instance;
    }

    void Start()
    {
        UpdateGameData(0);
    }

    void Update()
    {
        NameBar.text = currentName;

        if (!isShowingMessage)
        {
            TextBar.text = currentText;
        }

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
        if (isShowingMessage) return;  // 메시지 출력 중에는 무시

        if (currentActive == "FALSE")
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
        else
        {
            Bar.SetActive(true);
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

    /// <summary>
    /// 외부에서 메시지를 출력하고, 일정 시간 후 자동으로 TextBar를 비활성화합니다.
    /// </summary>
    public void ShowMessage(string message)
    {
        Debug.Log($"ShowMessage 호출됨, 메시지: [{message}], 인스턴스 이름: {gameObject.name}");

        isShowingMessage = true; // 메시지 출력 중 상태 ON
        Bar.SetActive(true);
        TextBar.text = message;

        StartCoroutine(HideBarAfterDelay(1.5f));
    }

    private IEnumerator HideBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Bar.SetActive(false);
        isShowingMessage = false; // 메시지 출력 중 상태 OFF
    }
}
