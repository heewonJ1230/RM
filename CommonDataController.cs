using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CommonDataController : MonoBehaviour
{
    //--스토리툰 다시보기 관련.//
    [Header("스토리툰 다시보기 관련")]
    public int hwaNum;
    public int pageCount;//-이거 페이지.



    //--Hcho버튼 컨트롤

    [Header("홍초관련")]
    public float delaySec;
    public GameObject prefab_BigMoney;
    public int hcho_Count;
    public int currentHchoCoolTime = 5;
    public static Button hchoAdBttn;
    public static Text hchoADBttnText;
    //여기 아래는 데잍터..?칸ㅌ,럴
    public static long hchomoney = 0;
    public static long hchomoneyIncreaseAmount = 0;

    public bool loseActive;
    public bool islose; //--얘가 실질적인 스위치로 얘 관리가 중
    public bool marimoIsSick = false;

    static bool isWin;
    public bool isWinOnce;

    [Header("ㅁㅏ리모 킬관련")]
    public static int cutCount;
    public bool isKilled;
    public bool isForgived;
    public static int killCount;
    public bool isTurnBack;

    bool deathonece = false;

    private void OnApplicationQuit()
    {
         CartoonManager cm = GetComponent<CartoonManager>();
         if (cm.doneCartoon[8] && !isKilled && !isForgived)
         {
             cm.doneCartoon[9] = false;
         }
    }
    private void Awake()
    {
        var obj = FindObjectsOfType<CommonDataController>();
        CartoonManager cm = GetComponent<CartoonManager>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            hchomoney = 0;
        }
        else
        {
            Destroy(gameObject);
        }
        if (null != GameObject.Find("GameManager"))
        {
            HchoManager hm = GameObject.Find("HchoManager").GetComponent<HchoManager>();
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
           //--마리모 킬 관련
            if (isKilled)
            {
                gm.isKilled = isKilled;
                gm.killCount = killCount;
                Debug.Log("얼마나죽었니스타트" + killCount);

                //Debug.Log("gm.isKilled");
                marimoIsSick = true;
            }
            if (isForgived)
            {
                gm.isForgived = isForgived;
                gm.cutCount = cutCount;
            }
            deathonece = false;
            hchoAdBttn = GameObject.Find("Image_HchoADPanelBttn").GetComponent<Button>();
            hchoADBttnText = GameObject.Find("Text_HchoAdBttntext").GetComponent<Text>();

        }
        isTurnBack = false;

        if (!cm.doneCartoon[3])
        {
            marimoIsSick = false;
        }
    }
    private void Start()
    {
        if (null != GameObject.Find("GameManager"))
        {
            HchoManager hm = GameObject.Find("HchoManager").GetComponent<HchoManager>();
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            CartoonManager cm = GetComponent<CartoonManager>();
            if (cm.doneCartoon[3] && !cm.doneCartoon[4])
            {
                marimoIsSick = true;
            }
            else if (cm.doneCartoon[4])
            {
                if (null != GameObject.Find("GameManager"))
                {

                  //--마리모 킬 관련

                    if (isKilled)
                    {
                        gm.isKilled = isKilled;
                        gm.killCount = killCount;
                        marimoIsSick = true;
                        cm.doneCartoon[9] = true; 
                        isForgived = false;
                        gm.isForgived = isForgived;

                    }
                    else if(isForgived)
                    {
                        gm.isForgived = isForgived;
                        cm.doneCartoon[9] = true;
                        gm.cutCount = cutCount;
                        if (cutCount >= 1)
                        {
                            marimoIsSick = true; 
                        }
                        isKilled = false;
                        gm.isKilled = false;

                    }
                  
                    hchomoneyIncreaseAmount = gm.moneyIncreaseAmount;

                }
            }
        }
           
    }

    void Update()
    {
        CartoonManager cm = GetComponent<CartoonManager>();
        if (cm.doneCartoon[3] && !cm.doneCartoon[4])
        {
            marimoIsSick = true;
            hchoADBttnText.text = "";
            hchoAdBttn.interactable = false;
        }
        else if (cm.doneCartoon[4])
        {
            if (null != GameObject.Find("NewMarimo_0"))
            {


                if (HchoPlayMAnager.isWin)
                {
                    isWin = true;
                    //Debug.Log("이겼니" + isWin);
                } //else { isWin = false; }
                isWinOnce = false;


                //                    Debug.Log("이즈윈원스는  " + isWinOnce);

                HchoPlayMAnager hpm = GameObject.Find("NewMarimo_0").GetComponent<HchoPlayMAnager>();
                if (hpm.loseChechOnce)
                {
                    islose = true;
                    marimoIsSick = true;

                    //Debug.Log("실패시 이즈 루즈 값은?" + islose);

                }
                else if (hpm.successChechOnce)
                {
                    islose = false;

                }
            }

            if (null != GameObject.Find("GameManager"))
            {
                deathonece = false;
                HchoManager hm = GameObject.Find("HchoManager").GetComponent<HchoManager>();
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>(); gm = GameObject.Find("GameManager").GetComponent<GameManager>();

                // Debug.Log("이긴거 들어오니?이겼니" + isWin);
                ButtnCheckhchoADButtn();
             
                hchomoneyIncreaseAmount = gm.moneyIncreaseAmount;

                if (islose && !loseActive)
                {
                    hchomoney = 0;
                    marimoIsSick = true;
                    hm.isHchoLose = true;
                    currentHchoCoolTime = hm.loseCoolTime_Min;
                    hm.currentHchoCoolTime = hm.loseCoolTime_Min;
                }
                if (marimoIsSick)
                {
                    islose = true;
                }
                else if (isWin && hchomoney != 0)
                {
                    //Debug.Log("이거 작동 안하니?");
                    marimoIsSick = false;
                    if (!isWinOnce)
                    {
                        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

                        fXController_ep1.HchoSuccessFX();
                        isWinOnce = true;
                        Vector3 midpoint =
                        new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 1);
                        GameObject bigM = Instantiate(prefab_BigMoney, midpoint, Quaternion.identity);//만들기 되었고

                        //이동시키
                        Invoke("GethchoMoneyFX", delaySec - 0.05f);
                        Invoke("GetMoney", delaySec);
                    }
                }

                //--마리모 킬 관련
                gm.cutCount = cutCount;
//                Debug.Log("cutcount" + cutCount);
                if (isKilled)
                {
                    gm.isKilled = isKilled;
                    gm.killCount = killCount;
                    marimoIsSick = true;
                    cm.doneCartoon[9] = true;
                    isForgived = false;
                    gm.isForgived = isForgived;

                }
                if (!isKilled && isForgived)
                {
                    gm.isForgived = isForgived;
                    cm.doneCartoon[9] = true;
                    gm.cutCount = cutCount;
                    if (cutCount >= 1)
                    {
                        marimoIsSick = true;
                    }
                    isKilled = false;
                    gm.isKilled = false;

                }
            }
            if (isTurnBack)
            {
                cm.doneCartoon[9] = false;
                isTurnBack = false;
                Debug.Log("턴백실행되서 10화체크풀려야데");
            }

        }


        //--마리모 킬&포기브 관련
        if (null != GameObject.Find("marimoInMarimoKill"))
        {
            MKmanager mkm = GameObject.Find("marimoInMarimoKill").GetComponent<MKmanager>();
            cutCount = mkm.cutCount;
            Debug.Log("cutcount" + cutCount);
            if (mkm.isKilled)
            {
                isKilled = mkm.isKilled;

                Debug.Log("죽은거 cdc에서 인식하는지" + isKilled);
                if (!deathonece)
                {
                    ++killCount;
                    deathonece = true;
                    Debug.Log("얼마나죽었니" + killCount);
                }
            }
            else if (!mkm.isKilled && mkm.isForgived)
            {
                isForgived = mkm.isForgived;
                Debug.Log("용서받은거 인지" + isForgived);

            }

        }
    }
    void GethchoMoneyFX()
    {
        if (null != GameObject.Find("GameManager"))
        {
            FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

            fXController_ep1.HchoAferFx();
        }
    }

    void GetMoney()//ㅎ초 성공ㅅ 보상 딜레이 줄거양
    {
        if (null != GameObject.Find("GameManager"))
        {
            FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

            fXController_ep1.HchoAferFx();
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            Debug.Log("가져와야 하는데 돈? " + "원래 있던 돈" + gm.money + "+" + hchomoney);
            isWin = false;
            gm.money += hchomoney;
            Debug.Log("글서 반영된 머니가" + gm.money);
        }
    }

    public void OnclickHchoReset()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        hchomoney = 0;
        hchomoneyIncreaseAmount = gm.moneyIncreaseAmount;
        islose = false;
        isWin = false;
        //Debug.Log("버튼 ㅎ초 게임으로 고고 초기화되었니/?돈 얼마니?" + hchomoney);
        //Debug.Log("버튼 ㅎ초 게임으로 고고 초기화되었니/?인크리스어마운트?" + hchomoneyIncreaseAmount);
    }



    void ButtnCheckhchoADButtn()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        CartoonManager cm = GetComponent<CartoonManager>();

        if (gm.isKilled)
        {
            hchoADBttnText.text = "";
            hchoAdBttn.interactable = false;
        }
        else
        {
            if (cm.doneCartoon[4])
            {
                if (marimoIsSick)
                {
                    hchoADBttnText.text = "팻말 눌러서 즉시 치료";
                    hchoAdBttn.interactable = true;
                }
                else
                {
                    hchoADBttnText.text = "";
                    hchoAdBttn.interactable = false;
                }
            }
            else
            {
                 hchoADBttnText.text = "";
                 hchoAdBttn.interactable = false;
             
            }
        }
       

    }

}