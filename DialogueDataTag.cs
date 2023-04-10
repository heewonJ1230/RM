using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueDataTag
{
    [Tooltip("대화 아이디")]
    public int ID;
    [Tooltip("하위 대화 번호1")]
    public string[] innerNum1;
    [Tooltip("말하는 주체1")]
    public string[] speaking_Creature1;
    [Tooltip("말내용1")]
    public string[] speaking_Text1;
}
[System.Serializable] //배열을 만들어줌.
public class DialogueEvent
{
    public Vector2 line;
    public DialogueDataTag[] dialogueDataTags;
}
