using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //--ㅅㅣ계 돌아가는것 관련
    public GameObject sandTimer_go;
    Vector2 timerOrginPosition;

    public GameObject turnBackMK_Panel;
    public Text currentMoney;
    public GameObject sandTimericonNtext;
    public GameObject marimo_go;
    public GameObject isKilledPanel;
    public GameObject turnback_OK_Bttn;
    public GameObject turnback_exp;

    bool isOpen;
    GameManager gm;
    CommonDataController cdc;
    CartoonManager cm;
    SuchoManager sm;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        sm = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        timerOrginPosition = sandTimer_go.transform.position;
        currentMoney.text = "비용 : " + gm.money.ToString("###,###") + " 물력";
        if (!isOpen)
        {
            sandTimericonNtext.SetActive(false);
        }
        KillPanelUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            sandTimericonNtext.SetActive(false);
        }

        KillPanelUpdate();
        if (!isOpen)
        {
            sandTimer_go.transform.Rotate(new Vector3(0,0,0));
        }
    }

    public void OnClickTurnBackMK()
    {
        if (gm.killCount >= 1)
        {
            cdc.isTurnBack = true;
            cm.doneCartoon[9] = false;
            CartoonManager.curIndex -=1;

            gm.money = 0;
            cdc.isKilled = false;
            cdc.marimoIsSick = false;
            cdc.islose = false;
            cdc.loseActive = false;
            if(CommonDataController.cutCount >= 5)
            {
                CommonDataController.cutCount -= 5;
                gm.cutCount = CommonDataController.cutCount;
            }

            gm.isKilled = false;
            sandTimericonNtext.SetActive(false);
            isKilledPanel.SetActive(false);
            sandTimer_go.transform.Rotate(0, 0, 0);
            isOpen = false;
            marimo_go.SetActive(true);

            if (gm.killCount >= 2)
            {
                gm.ghost_CreatureCount = 0;
                gm.ghost_ShrimpsCount = 0;
                gm.horse_ShrimpsCount = 0;
                gm.noRe_DPsCount = 0;
                if (null != GameObject.FindGameObjectWithTag("Ghost_normalShrimps"))
                {
                    GameObject[] g_shrimps = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");
                    for(int i=0; i < g_shrimps.Length; ++i)
                    {
                        Destroy(g_shrimps[i]);
                    }
                }
                if (null != GameObject.FindGameObjectWithTag("Ghost_horseShrimps"))
                {
                    GameObject[] h_shrimps = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");
                    for (int i = 0; i < h_shrimps.Length; ++i)
                    {
                        Destroy(h_shrimps[i]);
                    }
                }
                if(null != GameObject.FindGameObjectsWithTag("Ghost_noReDPs"))
                {
                    GameObject[] noRe_DPs = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");
                   for(int i = 0; i< noRe_DPs.Length; ++i)
                    {
                        Destroy(noRe_DPs[i]);   
                    }
                }

                if(gm.killCount >= 3)
                {
                    gm.creatureCount = 0;
                    gm.ASCount = 0;
                    gm.CHSCount = 0;
                    gm.shrimpsCount = 0;
                    gm.dpsCount = 0;
                    gm.SDPCount = 0;
                    gm.angryDPCount = 0;

                    if (null != GameObject.FindGameObjectWithTag("DPs"))
                    {
                        GameObject[] dps = GameObject.FindGameObjectsWithTag("DPs");
                        if (dps.Length >= 1)
                        {
                            for (int i = 0; i < dps.Length; ++i)
                            {
                                Destroy(dps[i]);
                            }
                        }
                    }
                    if (null != GameObject.FindGameObjectWithTag("SupriseDP"))
                    {
                        GameObject[] sdps = GameObject.FindGameObjectsWithTag("SupriseDP");
                        if (sdps.Length >= 1)
                        {
                            for (int i = 0; i < sdps.Length; ++i)
                            {
                                Destroy(sdps[i]);
                            }
                        }
                    }
                    if (null != GameObject.FindGameObjectWithTag("AngryPuffer"))
                    {
                        GameObject[] adp = GameObject.FindGameObjectsWithTag("AngryPuffer");
                        if (adp.Length >= 1)
                        {
                            for (int i = 0; i < adp.Length; ++i)
                            {
                                Destroy(adp[i]);
                            }
                           
                        }
                        // Debug.Log("앵그리복어 안지워지는거??");
                    }
                    if(null != GameObject.FindGameObjectWithTag("Shrimps"))
                    {
                        GameObject[] shrimps = GameObject.FindGameObjectsWithTag("Shrimps");
                        if (shrimps.Length >= 1)
                        {
                            for (int i= 0;i<shrimps.Length; ++i)
                            {
                                Destroy(shrimps[i]);
                            }
                        }
                    }
                    if (null != GameObject.FindGameObjectWithTag("CHS"))
                    {
                        GameObject[] chs = GameObject.FindGameObjectsWithTag("CHS");
                        if (chs.Length >= 1)
                        {
                            for (int i = 0; i < chs.Length; ++i)
                            {
                                Destroy(chs[i]);
                            }
                        }
                    }
                    if (null != GameObject.FindGameObjectWithTag("AstronautShrimp"))
                    {
                        GameObject[] ass = GameObject.FindGameObjectsWithTag("AstronautShrimp");
                        if (ass.Length >= 1)
                        {
                            for (int i = 0; i < ass.Length; ++i)
                            {
                                Destroy(ass[i]);
                            }
                        }
                    }

                    if (gm.killCount >= 4)
                    {
                        sm.valCount = 0;
                        sm.ludCount = 0;

                        if (null != GameObject.FindGameObjectWithTag("Vall"))
                        {
                            GameObject[] val = GameObject.FindGameObjectsWithTag("Vall");
                            if (val.Length >= 1)
                            {
                                for (int i = 0; i < val.Length; ++i)
                                {
                                    Destroy(val[i]);
                                }
                            }
                        }
                        if (null != GameObject.FindGameObjectWithTag("Ludwigia"))
                        {
                            GameObject[] lud = GameObject.FindGameObjectsWithTag("Ludwigia");
                            if (lud.Length >= 1)
                            {
                                for (int i = 0; i < lud.Length; ++i)
                                {
                                    Destroy(lud[i]);
                                }
                            }
                        }
                    }
                }
            }
            gm.Save();
            gm.GoSpecialCartoon();
        }
     

        //--ㅎ초주스기능켜기
        HchoManager hcho = GameObject.Find("HchoManager").GetComponent<HchoManager>();

        hcho.hchoBttn_go.SetActive(true);
        hcho.hcho_LockedTxt.SetActive(false);
        hcho.HchoLoseStart();

        Debug.Log(cdc.isTurnBack);//마리모 안생김 그리고 그 아이콘 없어지지도 않
    }
    public void OnClickSandTimer()
    {
        isOpen = true;
        turnBackMK_Panel.SetActive(true);
    }
    public void OnClickCancle()
    {
        isOpen = false;
        sandTimer_go.transform.Rotate(new Vector3(0, 0, 0));
    }

    void KillPanelUpdate()
    {
        if (gm.isKilled)
        {
            sandTimer_go.SetActive(true);
            sandTimericonNtext.SetActive(true);
            isKilledPanel.SetActive(true);
            if (gm.killCount >= 1)
            {
                currentMoney.text = "비용 : " + gm.money.ToString("###,###") + " 물력\n";
                if (gm.killCount == 1)
                {
                    turnback_exp.SetActive(true);
                    currentMoney.text += " 용서에는 비용이 들지 않았을 텐데...";
                }
             
                if (gm.killCount >= 2)
                {
                    turnback_exp.SetActive(false);

                    if (gm.ghost_CreatureCount > 0)
                    {
                        currentMoney.text += " + 고스트 생물 " + gm.ghost_CreatureCount.ToString("###,###") + " 마리";
                    }
                   

                    if (gm.killCount >= 3)
                    {
                        if (gm.killCount >= 4)
                        {
                            if (sm.valCount + sm.ludCount > 0)
                            {
                                currentMoney.text += " + 어항 안의 모든 생명체 \n";
                                if (gm.killCount == 4)
                                {
                                    currentMoney.text += " 왜 계속 같은 행동을 반복하는 건가요? 용서해야 미래가 있는 것...";

                                }
                            }
                            else
                            {
                                currentMoney.text += " + 수중 식물 필요 \n";
                                if(gm.killCount == 4)
                                {
                                    currentMoney.text += "  왜 같은 행동을 반복하죠? 용서해야 미래가 있는 것...";
                                }
                            }

                            if (gm.killCount >= 5)
                            {
                                currentMoney.text = "나의 호의는 여기까지입니다. \n 이제 비용을 지급할 만한 것도 남지 않았군요...";
                            }
                        }
                        else if (gm.creatureCount > 0)
                        {
                            currentMoney.text += " + 식물을 제외한 모든 생물";
                        }
                        else if (gm.creatureCount <= 0)
                        {
                            currentMoney.text += " + 생명체 하나라도...";
                        }
                    }
                    else
                    {
                        currentMoney.text += " + 고스트 생물 필요. \n";
                        if (gm.killCount == 2)
                        {
                            currentMoney.text += "용서에는 비용이 들지 않죠.";
                        }
                    }
                    
                }
            }
        }
        if (isOpen)
        {
            sandTimer_go.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 140);
        }
        if (gm.killCount >= 5)
        {
            sandTimer_go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            turnback_OK_Bttn.SetActive(false);
            turnback_exp.SetActive(false);
        }
    }
}
