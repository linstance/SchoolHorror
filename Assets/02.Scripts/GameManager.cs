using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text TimerText;
    public GameObject InventoryObj;
    public Inventory inventory;

    private GameObject DangerEffect;
    private GameObject Ghost;
    public GameObject ghostImage;

    private Color EffectColor;
    private Color GhostColor;

    [Header("게임 시간/체력 설정")]
    public float maxTime = 120f;
    private float currentTime;
    public bool isTimePaused = false;

    public int maxHP = 6;
    private int currentHP;

    private bool damage60Given = false;
    private bool damage40Given = false;
    private bool damage20Given = false;
    private bool ghostShown = false;

    [Header("클리어 체크")]
    public bool[] classroomClears = new bool[4]; // 1-1 ~ 1-4
    public bool infirmaryClear = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 상위에서 DontDestroyOnLoad 처리됐으므로 생략
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
    }

    void Start()
    {
        currentHP = maxHP;
        isTimePaused = false;
        InitSceneObjects();
        ResetTimer();
    }

    void Update()
    {
        if (isTimePaused) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 60f && !damage60Given)
            ApplyDangerEffect(0.3f, 0.2f, ref damage60Given);
        if (currentTime <= 40f && !damage40Given)
            ApplyDangerEffect(0.6f, 0.5f, ref damage40Given);
        if (currentTime <= 20f && !damage20Given)
            ApplyDangerEffect(0.9f, 0.9f, ref damage20Given);

        if (currentTime <= 10f && !ghostShown)
            ShowGhost();

        if (currentTime <= 0 || currentHP <= 0)
            GameOver("시간 초과 또는 체력 소진");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();
        ResetDamageFlags();
        InitSceneObjects();
    }

    void InitSceneObjects()
    {
        DangerEffect = GameObject.Find("DontDestroyObject/UI/DangerEffect");
        if (DangerEffect != null)
            EffectColor = DangerEffect.GetComponent<Image>().color;

        Ghost = GameObject.Find("DontDestroyObject/UI/Ghost");
        if (Ghost != null)
        {
            GhostColor = Ghost.GetComponent<Image>().color;
            GhostColor.a = 0f;
            Ghost.GetComponent<Image>().color = GhostColor;
        }

        GameObject ghostObj = GameObject.Find("GhostImage");
        if (ghostObj != null)
        {
            ghostImage = ghostObj;
            ghostImage.SetActive(false);
        }

        GameObject timerObj = GameObject.Find("TimerText");
        if (timerObj != null)
            TimerText = timerObj.GetComponent<TMP_Text>();
    }

    void ResetTimer()
    {
        currentTime = maxTime;
    }

    void ResetDamageFlags()
    {
        damage60Given = false;
        damage40Given = false;
        damage20Given = false;
        ghostShown = false;
    }

    void ApplyDangerEffect(float dangerAlpha, float ghostAlpha, ref bool flag)
    {
        flag = true;
        currentHP--;

        if (DangerEffect != null)
        {
            EffectColor.a = dangerAlpha;
            DangerEffect.GetComponent<Image>().color = EffectColor;
        }

        if (Ghost != null)
        {
            GhostColor.a = ghostAlpha;
            Ghost.GetComponent<Image>().color = GhostColor;
        }

        Debug.Log($"HP 감소: {currentHP}");

        if (currentHP <= 0)
            GameOver("체력 소진");
    }

    void ShowGhost()
    {
        ghostShown = true;

        if (Ghost != null)
        {
            GhostColor.a = 1.0f;
            Ghost.GetComponent<Image>().color = GhostColor;
        }

        if (ghostImage != null)
            ghostImage.SetActive(true);
    }

    void UpdateTimerUI()
    {
        if (TimerText != null)
            TimerText.text = Mathf.Ceil(currentTime).ToString("0") + "s";
    }

    public void AddBonusTime(float time)
    {
        currentTime += time;
        if (currentTime > maxTime)
            currentTime = maxTime;
    }

    public void PauseTimer(bool pause)
    {
        isTimePaused = pause;
    }

    void GameOver(string reason)
    {
        Debug.Log("Game Over: " + reason);
        // TODO: 게임 오버 연출, 씬 전환, UI 표시 등
    }

    public void CheckEnding()
    {
        bool allCleared = classroomClears.All(c => c);

        if (allCleared)
        {
            Debug.Log("해피엔딩!");
            // TODO: 해피엔딩 처리
        }
        else if (infirmaryClear)
        {
            Debug.Log("보건실만 클리어 → 배드엔딩");
            // TODO: 배드엔딩 처리
        }
        else
        {
            Debug.Log("엔딩 조건 미달 → 배드엔딩");
            // TODO: 배드엔딩 처리
        }
    }

    // ===== 인벤토리 기능 =====

    public void InventoryOpen()
    {
        InventoryObj.SetActive(true);

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
            Debug.LogError("Inventory 또는 Item이 null입니다.");
        }
    }
}
