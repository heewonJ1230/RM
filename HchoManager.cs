using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HchoManager : MonoBehaviour //--야는 버튼이랑 텍스트 컨트롤만 해 다른 마리모 씩효과 없음
{
    //스위치는 cmc얘는 작동
    public GameObject hchoBttn_go;
    public GameObject hcho_LockedTxt;
    public Button hchoBttn;
    public Text HcholoseCoolTimeCounter;
    public int loseCoolTime_Min;
    public int loseCoolTime_Sec;
    public int currentHchoCoolTime;

    public bool isHchoLose;//-이게 쿨타임대신하는 거
    private bool bePause;

    SaveData saveData;
    GameManager gm;
   
    private void Start()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        /*loseCoolTime_Min = cdc.loseCoolTime_Min;
        loseCoolTime_Sec = cdc.loseCoolTime_Sec;
        currentHchoCoolTime = cdc.currentHchoCoolTime;*/

        if (cm.doneCartoon[7] && !cm.doneCartoon[9])
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
        }
        if (cm.doneCartoon[4])
        {
            hchoBttn_go.SetActive(true);
            hcho_LockedTxt.SetActive(false);
            HchoLoseStart();
            HchoBttnCondition();
        }
        else
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
            HcholoseCoolTimeCounter.text = "";
        }
        if (cdc.marimoIsSick)
        {
            //isHchoLose = true;
            currentHchoCoolTime = cdc.currentHchoCoolTime;
        }

        if (gm.isKilled)
        {
            StopAllCoroutines();
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        if (pause)
        {
           bePause = true;
           HcholoseCoolTimeCounter.text = "";
            if (cm.doneCartoon[4])
            {
                StopCoroutine("LoseCoolTimeCounter");
            }
        }
        else{
            if (bePause)
            {
                bePause = false;
                HcholoseCoolTimeCounter.text = "";
                if (cm.doneCartoon[4])
                {
                    StartCoroutine("LoseCoolTimeCounter");
                }
                //HchoLoseStart();
            }
        }
    }
    private void Update()
    {
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        HchoBttnCondition();
        if (cdc.islose)
        {
            OnGoMarimoSick();
        }
        else
        {
            StopCoroutine("LoseCoolTimeCounter");
            currentHchoCoolTime = loseCoolTime_Min;
            cdc.currentHchoCoolTime = currentHchoCoolTime;
        }
        /*if (!isHchoLose)
          {
              if (cdc.marimoIsSick)
              {
                  isHchoLose = true;
                  currentHchoCoolTime = loseCoolTime_Min;
                  StartCoroutine("LoseCoolTimeCounter");
                  Debug.Log("마리모씩 시작됨?");
              }
          }*/
        if (cdc.isKilled)
        {
            StopAllCoroutines();

        }
       /* if (gm.isKilled)
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
        }*/

      /*  if (cm.done9wha && !cm.done10wha)
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
        }*/

    }

    public void OnClickHcho() //-ㅎ초타임으로 보내주고 1
    {
      
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();

        gm.Save();
        LoadingManager.LoadScene("4H_Cho");
        cdc.hcho_Count += 1;
        Debug.Log("홍초메니저의 홍초 카운트 갑 "+ cdc.hcho_Count);
        cdc.OnclickHchoReset();
    }
    void HchoBttnCondition()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        if (cm.doneCartoon[4])
        {
            if (cdc.marimoIsSick)
            { hchoBttn.interactable = false; }
            else
            {
                hchoBttn_go.SetActive(true);
                hcho_LockedTxt.SetActive(false);
                hchoBttn.interactable = true;
                HcholoseCoolTimeCounter.text = "";
            }
            if (cdc.isKilled)
            {
                hchoBttn_go.SetActive(false);
                hcho_LockedTxt.SetActive(true);
                HcholoseCoolTimeCounter.text = "";
            }
        }
        else
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
            HcholoseCoolTimeCounter.text = "";
        }
        if(cm.doneCartoon[8] && !cm.doneCartoon[9])
        {
            hchoBttn_go.SetActive(false);
            hcho_LockedTxt.SetActive(true);
            HcholoseCoolTimeCounter.text = "";
        }
        else if(cm.doneCartoon[9] && gm.isForgived)
        {
            hchoBttn_go.SetActive(true);
            hcho_LockedTxt.SetActive(false);
        }
    }
    public void HchoLoseStart()//--SaiyanMarimoStart
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();

//        cdc.islose = true;
        
        if (!cdc.islose)
        {
            HcholoseCoolTimeCounter.text = "";
            currentHchoCoolTime = loseCoolTime_Min;
            //cdc.currentHchoCoolTime = currentHchoCoolTime;
        }
        else
        {
            StartCoroutine("LoseCoolTimeCounter");
        }
    }

    public void OnGoMarimoSick()//-스위치는cdc.islose;마리모식도 제어 할거야. SaiyanMarimo에서는UseSkill이야
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        if (isHchoLose && !cdc.loseActive)
        {
            cdc.loseActive = true;
            StartCoroutine("LoseCoolTimeCounter");
            cdc.marimoIsSick = true;
            isHchoLose = false;
        }

    }
    IEnumerator LoseCoolTimeCounter()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();

        HcholoseCoolTimeCounter.text = " " + currentHchoCoolTime + "분 " + loseCoolTime_Sec + "초 후" + "\n 치료완료";
        while(currentHchoCoolTime >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            loseCoolTime_Sec -= 1;
            if(loseCoolTime_Sec <= 0)
            {
                currentHchoCoolTime -= 1;
                loseCoolTime_Sec += 60;
            }
            HcholoseCoolTimeCounter.text = " " + currentHchoCoolTime + "분 " + loseCoolTime_Sec + "초 후" + "\n 치료완료";
        }
        cdc.loseActive = false;
        cdc.islose = false;
        cdc.marimoIsSick = false;
        hchoBttn.interactable = true;
        HcholoseCoolTimeCounter.text = "";
        yield break;
    }
}
