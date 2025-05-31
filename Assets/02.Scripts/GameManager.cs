using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text TimerText;
    public GameObject InventoryObj;
    public Inventory inventory;
    public TextManager textManager; // TextManager 직접 참조

    private GameObject DangerEffect;
    private GameObject Ghost;
    private Image Character;

    public GameObject ghostImage;

    private Color EffectColor;
    private Color GhostColor;

    [Header("게임 시간/체력 설정")]
    public float maxTime = 85f;
    private float currentTime;
    public bool isTimePaused = false;

    public int maxHP = 4;
    private int currentHP;

    private bool damage60Given = false;
    private bool damage40Given = false;
    private bool damage20Given = false;
    private bool ghostShown = false;

    [Header("클리어 체크")]
    public bool[] classroomClears = new bool[4];
    public bool infirmaryClear = false;
    public bool isGoodClear = false;
    public bool isBadClear = false;
    public bool isDie = false;


    [Header("체력 이미지")]
    public Sprite[] hpSprites;

    [Header("귀신 이미지 (60초, 40초, 20초)")]
    public Sprite[] ghostSprites;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentHP = maxHP;
        isTimePaused = false;
        InitSceneObjects();
        ResetTimer();
        UpdateCharacterImage();
    }

    void Update()
    {
        Amulet();
        FamilyPhoto();
        CheckEnding();


        if (isTimePaused) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 60f && !damage60Given)
        {
            ApplyDangerEffect(0.3f, 0.2f, ref damage60Given);
            UpdateGhostImageByTime(0);
        }
        if (currentTime <= 40f && !damage40Given)
        {
            ApplyDangerEffect(0.6f, 0.5f, ref damage40Given);
            UpdateGhostImageByTime(1);
        }
        if (currentTime <= 20f && !damage20Given)
        {
            ApplyDangerEffect(0.9f, 0.9f, ref damage20Given);
            UpdateGhostImageByTime(2);
        }

        if (currentTime <= 10f && !ghostShown)
            ShowGhost();

        if (currentTime <= 0 || currentHP <= 0)
            GameOver("시간 초과 또는 체력 소진");
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();
        ResetDamageFlags();
        InitSceneObjects();
        UpdateCharacterImage();
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

        GameObject characterObj = GameObject.Find("ChCanvas/Character");
        if (characterObj != null)
            Character = characterObj.GetComponent<Image>();
        else
            Debug.LogWarning("Character Image를 찾지 못했습니다: ChCanvas/Character");
    }

    void ResetTimer() => currentTime = maxTime;

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
        UpdateCharacterImage();

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

    void UpdateGhostImageByTime(int index)
    {
        if (ghostSprites == null || ghostSprites.Length <= index) return;

        if (Ghost != null)
        {
            Image ghostImageComponent = Ghost.GetComponent<Image>();
            if (ghostImageComponent != null)
                ghostImageComponent.sprite = ghostSprites[index];
        }
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

    void UpdateCharacterImage()
    {
        if (Character == null || hpSprites == null || hpSprites.Length == 0) return;
        int index = Mathf.Clamp(currentHP, 0, hpSprites.Length - 1);
        Character.sprite = hpSprites[index];
    }

    public void AddBonusTime(float time)
    {
        currentTime += time;
        if (currentTime > maxTime)
            currentTime = maxTime;
    }

    public void PauseTimer(bool pause) => isTimePaused = pause;

    void GameOver(string reason)
    {
        SceneManager.LoadScene("사망엔딩");
    }

    public void CheckEnding()
    {
        bool allCleared = classroomClears.All(c => c);
        if (allCleared)
        {
            isGoodClear = true;
            isBadClear = false;
            Debug.Log("해피엔딩! (모든 교실 클리어)"+ isGoodClear);
        }
        else
        {
            isBadClear = true;
            isGoodClear = false;
            Debug.Log("배드엔딩!" + isBadClear);
        }

    }

    // ===== 인벤토리 기능 =====

    public void InventoryOpen()
    {
        InventoryObj.SetActive(true);
        if (inventory != null)
            inventory.FreshSlot();
    }

    public void InventoryClose() => InventoryObj.SetActive(false);

    public void AddItemToInventory(Item item)
    {
        if (inventory != null && item != null)
            inventory.AddItem(item);
    }

    public void ApplyPenalty(int damage = 1)
    {
        currentHP -= damage;
        UpdateCharacterImage();

        Debug.Log($"Penalty applied. Current HP: {currentHP}");
        if (currentHP <= 0)
            GameOver("체력 소진");
    }


    private bool familyPhotoUsed = false;

    public void FamilyPhoto()
    {
        if (!familyPhotoUsed && currentHP < 2)
        {
            if (inventory.HasItem("가족 사진"))
            {
                textManager.ShowMessage("가족 사진을 보고 힘을 얻었다");
                currentHP += 1;
                inventory.RemoveItemByName("가족 사진");
                familyPhotoUsed = true;
                UpdateCharacterImage();
            }
        }
    }


    private bool amuletUsed = false;  // 한번만 사용하도록 플래그 추가

    public void Amulet()
    {
        if (!amuletUsed && currentTime <= 10f)
        {
            if (inventory.HasItem("부적"))
            {
                textManager.ShowMessage("부적의 힘이 내게 도망칠 시간을 만들어 줬다.");
                currentTime += 20;
                inventory.RemoveItemByName("부적");
                amuletUsed = true;
            }
        }
    }



    // === 아이템 체크 후 씬 이동 ===
    public void TryLoadSceneWithItem(string sceneName, string requiredItem)
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory가 할당되지 않았습니다.");
            return;
        }

        if (inventory.HasItem(requiredItem))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            if (textManager != null)
                textManager.ShowMessage("문이 잠겨 있다.");
            else
                Debug.LogWarning("TextManager가 할당되지 않았습니다.");
        }
    }
}
