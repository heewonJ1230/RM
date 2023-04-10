using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KillUISystem : MonoBehaviour
{
    public Image hpFill;
    public Image mpFill;

    public GameObject forgiveBttn;

    [Header("죽었을때 나오는 창과 글")]
    public GameObject gameoverPanel;
    public GameObject gameovertxt;
    public Text gameOverMsg;
    public string gameOverTXT;
    public GameObject goBackBttn;
    public float showingBttnTime;
    public Button forgiveBttnb;

    bool isDeathOnce;
    bool speakingdelayonce = false;
    MKmanager mkm;
    IMKSpeakBbbMaker imkBB;

    private void Start()
    {
        gameovertxt.SetActive(false);
        goBackBttn.SetActive(false);
        hpFill.fillAmount = 1.0f;
        mkm = GameObject.Find("marimoInMarimoKill").GetComponent<MKmanager>();
        imkBB = GameObject.Find("IMKSpeakBbbMaker").GetComponent<IMKSpeakBbbMaker>();
    }
    private void Update()
    {
        
       if(mkm.cutCount == 0 && mkm.sdgCount == 0)
        {
            hpFill.fillAmount = 1.0f;
        }
        else if(mkm.cutCount >= 5)
        {
            hpFill.fillAmount = 0;
        }
        if (hpFill.fillAmount == 0)
        {
            IMK_FXController imkFx = GameObject.Find("IMK_FXController").GetComponent<IMK_FXController>();

            if (!isDeathOnce)
            {
                imkFx.DeathFX();
                isDeathOnce = true;

                Invoke("GameOverUIShow", 0.5f);
               
                forgiveBttn.SetActive(false);
            }
           
        }
        if (imkBB.isSpeaking)
        {
            forgiveBttnb.interactable = false;
            speakingdelayonce = false;
            //Debug.Log("인터렉티브 앙대");
        }
        
        else if (!imkBB.isSpeaking && !speakingdelayonce)
        {
            forgiveBttnb.interactable = false;
            speakingdelayonce = true;
            Invoke("ForgiveDelay", 0.5f);
        }
    }
    void ForgiveDelay()
    {
        forgiveBttnb.interactable = true;
        
        //Debug.Log("포기브 버튼 인터렉티브 되는겨");
    }
    public void CutHp()
    {
        hpFill.fillAmount -= 1/5f;
    }
    public void HitHp()
    {
        hpFill.fillAmount -= 0.01f;
    }

    public void GameOverUIShow()
    {
        gameoverPanel.SetActive(true);
        StartCoroutine(AppearGameOverMSG(0.1f));
        Invoke("ShowBttn", showingBttnTime);
    }
    void ShowBttn()
    {
        goBackBttn.SetActive(true);
        gameovertxt.SetActive(true);
    }
    IEnumerator AppearGameOverMSG(float interval)
    {
       
        int index = 0;
       
        while (index <= gameOverTXT.Length)
        {
            gameOverMsg.text = gameOverTXT.Substring(0, index);
            yield return new WaitForSeconds(interval);
            index++;
        }
    }

}
