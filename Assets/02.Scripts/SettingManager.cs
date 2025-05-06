using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    FullScreenMode screenMode = FullScreenMode.FullScreenWindow;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    public List<TextMeshProUGUI> tmpTextsToScale;

    // 선택 가능한 해상도 목록 (필요한 해상도만 추가)
    private List<Resolution> customResolutions = new List<Resolution>()
    {
        new Resolution { width = 1280, height = 720, refreshRate = 60 },
        new Resolution { width = 1600, height = 900, refreshRate = 60 },
        new Resolution { width = 1920, height = 1080, refreshRate = 60 },
        new Resolution { width = 2560, height = 1440, refreshRate = 60 }
    };

    int resolutionNum = 0;

    private Vector2 referenceResolution = new Vector2(1920, 1080);
    private Matrix4x4 guiMatrix;
    private float currentScaleFactor = 1f;

    void Start()
    {
        InitUI();
        ApplyInitialResolution(); //실행 시 1920x1080 해상도 적용
        UpdateGUIMatrix();
        AdjustTMPTextSizes();
    }

    void InitUI()
    {
        resolutionDropdown.options.Clear();
        int targetIndex = -1;

        // customResolutions에 있는 해상도만 드롭다운에 추가
        for (int i = 0; i < customResolutions.Count; i++)
        {
            var res = customResolutions[i];
            var option = new TMP_Dropdown.OptionData($"{res.width} x {res.height} {res.refreshRate}hz");
            resolutionDropdown.options.Add(option);

            // 기본 해상도 설정 (1920x1080 60Hz)
            if (res.width == 1920 && res.height == 1080 && res.refreshRate == 60)
            {
                targetIndex = i;
            }
        }

        // 기본값을 1920x1080으로 설정
        resolutionDropdown.value = targetIndex;
        resolutionNum = targetIndex;

        resolutionDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
    }

    void ApplyInitialResolution()
    {
        if (customResolutions.Count > resolutionNum)
        {
            var res = customResolutions[resolutionNum];
            Screen.SetResolution(res.width, res.height, screenMode);
            Debug.Log($"초기 해상도 적용: {res.width}x{res.height} @ {res.refreshRate}Hz");
        }
    }

    public void DropboxOptionChange(int index)
    {
        resolutionNum = index;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OkBtnClick()
    {
        if (customResolutions.Count > resolutionNum)
        {
            var res = customResolutions[resolutionNum];
            Screen.SetResolution(res.width, res.height, screenMode);
            Debug.Log($"해상도 변경: {res.width}x{res.height}, 모드: {screenMode}");

            UpdateGUIMatrix();
            AdjustTMPTextSizes();
        }
    }

    void UpdateGUIMatrix()
    {
        float scaleX = Screen.width / referenceResolution.x;
        float scaleY = Screen.height / referenceResolution.y;
        currentScaleFactor = Mathf.Min(scaleX, scaleY);
        guiMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scaleX, scaleY, 1));
    }

    void AdjustTMPTextSizes()
    {
        foreach (var tmp in tmpTextsToScale)
        {
            if (tmp == null) continue;
            float baseSize = tmp.fontSize / currentScaleFactor; // 기준 크기 역산
            tmp.fontSize = baseSize * currentScaleFactor;
        }
    }
}
