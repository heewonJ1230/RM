using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemShuffle;

public class DialogueBubble : MonoBehaviour //-- 얘가 사실상 버블메이커
{
    [SerializeField] DialogueEvent dialogue;
    public DialogueDataTag[] dialogueDataTags;
    public GameObject prefabBubble;
    float timer ; //오초 타이머
    public int currentDialogue;
    public int currentBubble;
    GameObject mathergo;
    string mathername;
    public static string newtalkTxt;
    public bool isTalking = false; //-중복으로 떠들지 않게

    CommonDataController cdc;
    CartoonManager cm;
    GameManager gm;

    public DialogueDataTag[] GetDialogueDatas()
    {
        dialogue.dialogueDataTags = DataBaseManager.instance.GetDialogue((int)dialogue.line.x,(int)dialogue.line.y);
        
        return dialogue.dialogueDataTags;
    }
    void Awake()
    {  
        currentDialogue = 0;
        currentBubble = 0;
        timer = 3.0f;
    }
    void Start()
    {
         gm = GameObject.Find("GameManager").GetComponent<GameManager>();
         cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
         cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        ShowDialogue(GetDialogueDatas());
        Array.shuffle(dialogueDataTags);
    }
    void Update()
    {
        
       if (cm.doneCartoon[8] &&!cm.doneCartoon[9])
        {
            isTalking = true;
        }
        else if (gm.creatureCount >= 3 && !cdc.marimoIsSick && !gm.isKilled)
        {
            isTalking = false;

            if (!isTalking)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    isTalking = true;
                    timer += 7.0f;
                    TimerBubble();
                    currentBubble += 1;
                    if (currentBubble >= dialogueDataTags[currentDialogue].innerNum1.Length)
                    {
                        timer += 10.0f;
                        currentBubble = 0;
                        currentDialogue += 1;
                        if (currentDialogue >= dialogueDataTags.Length)
                        {
                            currentBubble = 0;
                            currentDialogue = 0;
                            timer += 10.0f;
                        }
                    }
                    isTalking = false;
                }
            }
        }
        else { isTalking = true; }

       
    }

    //------------------
    public void TimerBubble() 
    {
        FindMather();
    }
    void FindMather()
    {
        if (dialogueDataTags[currentDialogue].speaking_Creature1[currentBubble] == "마리모")
        {
            if (cm.doneCartoon[9] && CommonDataController.cutCount >= 1)
            {
                mathername = "마리모삐짐";
                //isTalking = true;
                //MoveNextDialogue(); //--이거 때문에 처음 말주머니 두개씩 쌩김! 주의하자 꼭 무브넥스트 다얄로그는 한번 실행되어야함
            }
            else
            {
                mathername = "Marimo";
            }
               
        }
        else if (dialogueDataTags[currentDialogue].speaking_Creature1[currentBubble] == "새우")
        {
            mathername = "Shrimps";
        }
        else if (dialogueDataTags[currentDialogue].speaking_Creature1[currentBubble] == "인뽁")
        {
            mathername = "DPs";
        }
        else if(dialogueDataTags[currentDialogue].speaking_Creature1[currentBubble] == "영혼")
        {
            int ranGhost = Random.Range(0, 2);
            if (ranGhost == 0)
            {
                mathername = "Ghost_normalShrimps";
            }else if(ranGhost == 1)
            {
                mathername = "Ghost_horseShrimps";
            }
            else
            {
                mathername = "Ghost_noReDPs";
            }
        }
        //else { isTalking = true;
        //    MoveNextDialogue(); }

        if (null != GameObject.FindGameObjectWithTag(mathername))
        {
            TalkeTxt();
            CreateABubble();
        }
        if (null == GameObject.FindGameObjectWithTag(mathername))
        {
            isTalking = true;
            MoveNextDialogue();
        }
    }
  
    void CreateABubble()//- 크리에잇 쉬림프 격
    {
        GameObject[] mothergos = GameObject.FindGameObjectsWithTag(mathername);
        List<GameObject> motherList = new List<GameObject>();
        for (int i = 0; i < mothergos.Length; i++)
        {
            motherList.Add(mothergos[i]);
        }
        if (motherList.Count == 1)
        {
            mathergo = motherList[0];
        }
        else
        {
            int ranmother = Random.Range(0, mothergos.Length);
            mathergo = motherList[ranmother];
        }

        Vector2 motherSpot = mathergo.transform.position;
        float spotX = motherSpot.x;
        float spotY = motherSpot.y;

        GameObject childBubble = Instantiate(prefabBubble, new Vector2(spotX, spotY), Quaternion.identity);
        childBubble.transform.parent = mathergo.transform.parent;
    }
    void TalkeTxt()
    {
        if (currentBubble >= dialogueDataTags[currentDialogue].speaking_Text1.Length)
        {
            MoveNextDialogue();
        }
        newtalkTxt = dialogueDataTags[currentDialogue].speaking_Text1[currentBubble];
       // Debug.Log("currentDiallogue" + currentDialogue + "currentBubb " + currentBubble);
    }
    void ShowDialogue(DialogueDataTag[] ShowDialogues)
    {
        dialogueDataTags = ShowDialogues;
        Array.shuffle(dialogueDataTags);
    }
    void MoveNextDialogue()
    {
        currentDialogue += 1;
        currentBubble = 0;
        if (currentDialogue >= dialogueDataTags.Length)
        {
            currentBubble = 0;
            currentDialogue = 0;
            timer += 10.0f;
        }
        FindMather();
    }


}
