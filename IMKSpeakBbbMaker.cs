using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMKSpeakBbbMaker : MonoBehaviour
{//케이디 강의에서 인터렉션이벤트임.

    [Header("버블관련")]
    public GameObject prefabBbb;
    public Vector2 bBbSpawnSpot;
    [Header("이게 대체왜두번있어?")] //ㅇㅐ가 먼저 받아서 넘겨주는 ㅑ
    IMKSpeakTag[] iMKSpeakTagss;

    [Header("넘기기 타이머?인가?")]
    public float maxTimer;
    public float timer;
    [Header("다음 말주머니")]
    public int currentSpeak;

    public static string newSpeakTxt;

    public bool isSpeaking;

    [SerializeField] public SpeakEvent iMKspeak;//불러오는 장소역ㅒ

    public IMKSpeakTag[] GetIMKSpeaks()
    {
        iMKspeak.iMKSpeakTags = IMKSpeakDataManager.instance.GetIMKSpeak((int)iMKspeak.line_IML.x, (int)iMKspeak.line_IML.y);

        return iMKspeak.iMKSpeakTags;
    }


    private void Awake()
    {
        timer = maxTimer;
        currentSpeak = 0;
        isSpeaking = false;
    }

    private void Start()
    {
        ShowSpeak(GetIMKSpeaks());
    }

    public void ShowSpeak(IMKSpeakTag[] p_showSpeaks)
    {
        iMKSpeakTagss = p_showSpeaks;
    }

    public void CreateBubbleIMK()
    {
        if (!isSpeaking)
        {
            isSpeaking = true;
            GameObject mSpeakBbb = Instantiate(prefabBbb, bBbSpawnSpot, Quaternion.identity);
            mSpeakBbb.transform.parent = GameObject.Find("MarimoInMK").transform;
            
        }
    }
    public void SpeakTxt(int i, int j)//여기서 머헬스단계 만들어야하나
    {
        newSpeakTxt = iMKSpeakTagss[i].speakingTexts_IMK[j];
    }
}
