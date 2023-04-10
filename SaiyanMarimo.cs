using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SaiyanMarimo : MonoBehaviour
{
    private bool bPause;
    public Image skillFilter;
    public Text coolTimeCounter;
    public Text admssg;
    public Button saiyanMCBttn;
    public GameObject adPanel;

    public int coolTime_Min;
    public int coolTime_Sec;
    public int saiyanTime;
    public bool isSaiyanTime;
    public bool isAdsDone;
    public bool isCoolTime = true;
    bool nomoreSick = false;
    GameManager gm;

    public int currentCoolTime;
    private int currentSaiyanTime;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        isAdsDone = false;
        SaiyanMarimoStart();
    }
    void Update()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (isAdsDone)
        {
            FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
            fXController_ep1.SaiyanReadySFX();
            StopCoroutine("SaiyanTime");
            StopCoroutine("SaiyanTimeCounter");
            StopCoroutine("CoolTime");
            StopCoroutine("CoolTimeCounter");
            isSaiyanTime = false;
            isCoolTime = false;
            gm.isCoolTime = isCoolTime;
            //currentCoolTime = 0;
            gm.currentCoolTime = currentCoolTime;
            skillFilter.fillAmount = 0;
            coolTimeCounter.text = "사용 가능";
            
            SaveData saveData = new SaveData();
            saveData.saiyanMrimoCoolTimeAd = currentCoolTime;
            saveData.isAdCoolTime = isCoolTime;
            gm.Save();
            isAdsDone = false;
        }
        else
        {
            ;
        }
        SaiyanButtonCheck();
        MarimoSickCheckforSM();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            isSaiyanTime = false;
            isCoolTime = true;
            //StopAllCoroutines();
            StopCoroutine("SaiyanTime");
            StopCoroutine("SaiyanTimeCounter");
            StopCoroutine("CoolTime");
            StopCoroutine("CoolTimeCounter");
    
            bPause = true;
            //SaiyanMarimoStart();
        }
        else
        {
            if (bPause)
            {
                bPause = false;
                StopCoroutine("SaiyanTime");
                StopCoroutine("SaiyanTimeCounter");
                StopCoroutine("CoolTime");
                StopCoroutine("CoolTimeCounter");
                isSaiyanTime = false;
                isCoolTime = true;
                SaiyanMarimoStart();
            }
        }
    }
    //--------------------------
    public void SaiyanMarimoStart()
    {
        isSaiyanTime = false;
        isCoolTime = true;

        if (!isCoolTime)
        {
            currentCoolTime = coolTime_Min;
            skillFilter.fillAmount = 0;
        }
        else
        {
            if (isCoolTime)
            {
                skillFilter.fillAmount = (float)currentCoolTime / coolTime_Min;

                StartCoroutine("CoolTime");
                StartCoroutine("CoolTimeCounter");
            }
        }
    }
    void MarimoSickCheckforSM()
    {
       CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        
        if (cdc.marimoIsSick)
        {
            StopCoroutine("SaiyanTime");
            StopCoroutine("SaiyanTimeCounter");
            StopCoroutine("CoolTime");
            StopCoroutine("CoolTimeCounter");
            isSaiyanTime = false;
            nomoreSick = true;
        }
        else
        {
            if (nomoreSick == true)
            {
                StartCoroutine("CoolTime");
                StartCoroutine("CoolTimeCounter");
                nomoreSick = false;
            }
        }
    }
    public void UseSkill()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

        if (!isSaiyanTime && isCoolTime) 
        {
            fXController_ep1.EndSFX();
            adPanel.SetActive(true);
        }
        if (!isCoolTime && !isSaiyanTime)
        {
            fXController_ep1.SaiyanMarimoClickFX();
            isCoolTime = true;
            isSaiyanTime = true;
            currentCoolTime = coolTime_Min;
            currentSaiyanTime = saiyanTime;
            StartCoroutine("SaiyanTime");
            StartCoroutine("SaiyanTimeCounter");
            //Debug.Log("좋아좋아 좋을때 누름");
        }
    }
    IEnumerator SaiyanTime()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
        currentCoolTime = coolTime_Min;
        isCoolTime = true;
        while (skillFilter.fillAmount < 1)
        {
            skillFilter.fillAmount += 1 * Time.smoothDeltaTime / saiyanTime;
            yield return null;
        }
        skillFilter.fillAmount = 1;
        fXController_ep1.EndSFX();
        
        StartCoroutine("CoolTime");
    }
    IEnumerator SaiyanTimeCounter()
    {
        currentCoolTime = coolTime_Min;
        isCoolTime = true;
        coolTimeCounter.text = "남은 시간 : " + currentSaiyanTime + " 초";
        while (currentSaiyanTime > 0)
        {

            yield return new WaitForSeconds(1.0f); 

            currentSaiyanTime -= 1;

            coolTimeCounter.text = "남은 시간 : " + currentSaiyanTime + " 초";
        }
        isSaiyanTime = false;
        StartCoroutine("CoolTimeCounter");
    }
    IEnumerator CoolTime()
    {
        while (skillFilter.fillAmount >= 0)
        {
            skillFilter.fillAmount -= 1/60f * (float)Time.smoothDeltaTime / currentCoolTime;
            yield return null;
        }
       
        yield break;
    }
    IEnumerator CoolTimeCounter()
    {
        coolTimeCounter.text = "" + currentCoolTime + " 분" + coolTime_Sec + " 초 뒤" + "\n 사용가능";
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            coolTime_Sec -= 1;
            if(coolTime_Sec <= 0)
            {
                currentCoolTime -= 1;
                coolTime_Sec += 60;
            }
            coolTimeCounter.text = "" + currentCoolTime + " 분 "+coolTime_Sec+" 초 뒤"+"\n사용가능";
        }
        isCoolTime = false;
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
/*        if (!gm.isKilled)
        {
            fXController_ep1.SaiyanReadySFX();
        }*/
        yield break;
    }

    void SaiyanButtonCheck()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        if (gm.money >10 && !isSaiyanTime &&!isCoolTime && !cdc.marimoIsSick)
        {
            saiyanMCBttn.interactable = true;
            coolTimeCounter.text = "사용 가능";
            admssg.text = "초사이언 마리모";
        }
       else if(isSaiyanTime) 
       {
            saiyanMCBttn.interactable = false;
            admssg.text = "탭하면 물력 3배!";
       }
       else if(cdc.marimoIsSick)
        {
            saiyanMCBttn.interactable = false;
            coolTimeCounter.text = "병든 마리모 사용 불가";
            admssg.text = "초사이언 마리모";
            nomoreSick = true;
        }
        else if(!isSaiyanTime && !isCoolTime && gm.money < 10)
       {
            saiyanMCBttn.interactable = false;
            coolTimeCounter.text = "아직 사용 불가";
            admssg.text = "초사이언 마리모";
        }
       else if (isCoolTime && !cdc.marimoIsSick)
        {
            admssg.text = "에너지 모으는 중 :\n 눌러서 광고보기";
            saiyanMCBttn.interactable = true;
        }
        else {
            saiyanMCBttn.interactable = false;
        }

    }
}
