using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance; 

    [SerializeField] string csv_FileName;

    Dictionary<int, DialogueDataTag> dialogueDic = new Dictionary<int, DialogueDataTag>();

    public static bool isFinish = false; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ParserDialogue theParser = GetComponent<ParserDialogue>();
            DialogueDataTag[] dialogues = theParser.Parse(csv_FileName);
         
            for(int i= 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            isFinish = true;
        }
    }

    public DialogueDataTag[] GetDialogue(int _startNum, int _endNum)
    {
        List<DialogueDataTag> dialoguesList = new List<DialogueDataTag>();
        
        for (int i = 0; i <= _endNum - _startNum; i++)
        {
            dialoguesList.Add(dialogueDic[_startNum + i]); 
        }
 
        return dialoguesList.ToArray();
    }
    /*
     희워니 여기서 부터 함 보자

    Dictionary<long(새우숫자), "데이터테그(니 봇대로 함번 만들어)"
    포 이치로 데이터 넣고 
   */

}
