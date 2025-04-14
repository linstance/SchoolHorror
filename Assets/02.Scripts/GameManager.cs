using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

    private GameObject DangerEffect;
    private GameObject Tomari;


    private Color EffectColor; // 색상 투명도
    private Color TomariColor;

    public Text Timebar;

    private string Time;
    private static int Hour = 0; // 시간
    private static int Minute = 0; // 분

    void Start()
    {
        DangerEffect = GameObject.Find("DontDestroyObject/UI/DangerEffect");
        EffectColor = DangerEffect.GetComponent<Image>().color;

        Tomari = GameObject.Find("DontDestroyObject/UI/Tomari");
        TomariColor = Tomari.GetComponent<Image>().color;

    }
    //GameObject.FindWithTag("DangerEffect")

    void Update()
    {
        EffectTransparency();
        TimeUpdater();
        Debug.Log(EffectColor);
        if (DangerEffect == null)
        {
            Debug.Log("널임");
            
        }
    }


    private void EffectTransparency()
    {
       

        if (Minute == 40)
        {
            EffectColor.a = 0.5f;
            DangerEffect.GetComponent<Image>().color = EffectColor;

            TomariColor.a = 0.5f;
            Tomari.GetComponent<Image>().color = TomariColor;
        }
        else if(Minute == 50)
        {
            EffectColor.a = 0.7f;
            DangerEffect.GetComponent<Image>().color = EffectColor;

            TomariColor.a = 1f;
            Tomari.GetComponent<Image>().color = TomariColor;
        }
    }


    public void TimeUpdater()
    {
        // "00:00" 형식으로 출력
        Time = string.Format("{0:D2}:{1:D2}", Hour, Minute);  // 두 자리 숫자 형식으로 출력
        Timebar.text = Time;
    }


    public void InputObject()
    {
        Minute += 10;  // 분을 10분 증가시킴

        if (Minute >= 60)
        {
            Minute = 0;
            Hour += 1;

            if (Hour >= 24)
            {
                Hour = 0;
            }
        }

        Debug.Log("시간: " + Hour + ", 분: " + Minute);
        
    }
}