using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text Timebar;
    public GameObject InventoryObj;             // 인벤토리 UI 오브젝트 (SetActive용)
    public Inventory inventory;              // Inventory 스크립트 (에디터에서 직접 참조할 것)

    private GameObject DangerEffect;
    private GameObject Tomari;

    private Color EffectColor;
    private Color TomariColor;

    private string Time;
    private static int Hour = 0;
    private static int Minute = 0;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // UI 오브젝트 찾기 (씬 내에 반드시 있어야 함)
        DangerEffect = GameObject.Find("DontDestroyObject/UI/DangerEffect");
        if (DangerEffect != null)
            EffectColor = DangerEffect.GetComponent<Image>().color;

        Tomari = GameObject.Find("DontDestroyObject/UI/Tomari");
        if (Tomari != null)
            TomariColor = Tomari.GetComponent<Image>().color;
    }

    void Update()
    {
        EffectTransparency();
        TimeUpdater();
    }

    private void EffectTransparency()
    {
        if (Minute == 40)
        {
            SetEffectAlpha(0.5f);
        }
        else if (Minute == 50)
        {
            SetEffectAlpha(0.7f);
            SetTomariAlpha(1.0f);
        }
    }

    private void SetEffectAlpha(float alpha)
    {
        if (DangerEffect != null)
        {
            EffectColor.a = alpha;
            DangerEffect.GetComponent<Image>().color = EffectColor;
        }
    }

    private void SetTomariAlpha(float alpha)
    {
        if (Tomari != null)
        {
            TomariColor.a = alpha;
            Tomari.GetComponent<Image>().color = TomariColor;
        }
    }

    public void TimeUpdater()
    {
        Time = string.Format("{0:D2}:{1:D2}", Hour, Minute);
        if (Timebar != null)
            Timebar.text = Time;
    }

    public void InputObject()
    {
        Minute += 10;
        if (Minute >= 60)
        {
            Minute = 0;
            Hour += 1;
            if (Hour >= 24)
                Hour = 0;
        }

        Debug.Log("시간: " + Hour + ", 분: " + Minute);
    }

    public void InventoryOpen()
    {
            InventoryObj.SetActive(true);

            // 인벤토리 UI 갱신
            if (inventory != null)
                inventory.FreshSlot();
            else
                Debug.LogError("Inventory 스크립트가 참조되지 않았습니다.");
    }

    public void InventoryClose()
    {
        InventoryObj.SetActive(false);
    }

    public void AddItemToInventory(Item item)
    {
        if (inventory != null && item != null)
        {
            inventory.AddItem(item);
        }
        else
        {
            Debug.LogError("Inventory 또는 Item이 null입니다. 확인하세요.");
        }
    }
}
