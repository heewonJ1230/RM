using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IMKSpeakTag
{
    [Header("각 버블 아이디_마리모킬")]
    public string situationNum_IMK;
    [Header("말하ㅡ는 상황_마리모킬")]
    public string situationIMK;
    [Header("말하는 상황번호_마리모킬")]
    public string[] ID ;
    [Header("말하는 내용 _마리모킬")]
    public string[] speakingTexts_IMK;
}
[System.Serializable]
public class SpeakEvent
{
    public Vector2 line_IML;
    public IMKSpeakTag[] iMKSpeakTags;
}
