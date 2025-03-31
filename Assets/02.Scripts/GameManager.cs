using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

    private GameObject DangerEffect;
    private Color Effectcolor;

    public Text Timebar;

    private string Time;
    private static int Hour = 0; // �ð�
    private static int Minute = 0; // ��

    void Start()
    {
        DangerEffect = GameObject.Find("DontDestroyObject/UI/DangerEffect");
        Effectcolor = DangerEffect.GetComponent<Image>().color;
    }
    //GameObject.FindWithTag("DangerEffect")

    void Update()
    {
        EffectTransparency();
        TimeUpdater();
        Debug.Log(Effectcolor);
        if (DangerEffect == null)
        {
            Debug.Log("����");
            
        }
    }


    private void EffectTransparency()
    {
       

        if (Minute == 40)
        {
            Effectcolor.a = 0.5f;
            DangerEffect.GetComponent<Image>().color = Effectcolor;
        }
        else if(Minute == 50)
        {
            Effectcolor.a = 0.7f;
            DangerEffect.GetComponent<Image>().color = Effectcolor;
        }
    }


    public void TimeUpdater()
    {
        // "00:00" �������� ���
        Time = string.Format("{0:D2}:{1:D2}", Hour, Minute);  // �� �ڸ� ���� �������� ���
        Timebar.text = Time;
    }


    public void InputObject()
    {
        Minute += 10;  // ���� 10�� ������Ŵ

        if (Minute >= 60)
        {
            Minute = 0;
            Hour += 1;

            if (Hour >= 24)
            {
                Hour = 0;
            }
        }

        Debug.Log("�ð�: " + Hour + ", ��: " + Minute);
        
    }
}