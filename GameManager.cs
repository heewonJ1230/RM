using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("만렙달성")]
    public GameObject maxLev;
    public int intmaxLev;
  
    private bool bPause;

    public System.DateTime startTime;
    public System.DateTime quitTime;

    [Header("시스템")]
    public int currentCoolTime;
    public bool isCoolTime;
    public Text nameText;
    public Text nameInput;
    public Button nameConfirmBttn;
    public Text namePlaceHolder;
    public string hangNameSaveTxt;
    public GameObject nameInputPanel;
    public GameObject creaturesGroup;
    public GameObject angryPufferGroup;

    public int width; 
    public float space; 
    public float fixedY = -11.91f;

    public float spaceFloor; 
    public int floorCapacity;
    public int currentFloor; 

    public long money;
    [Tooltip("클릭당 단가 업그레이드 양")]
    public long moneyIncreaseAmount;
    [Tooltip("클릭당 단가 업그레이드 레벨")]
    public long moneyIncreaseLevel; 
    [Tooltip("업그레이드 가격")]
    public long moneyIncreasePrice;

    [Header("화났뽁관련")]
    public GameObject angryDiscount;
    public Text angryDiscountPoint;
    public int eattenallPoint;
    public long eattenCreatureCount;

    [Header("카드 체크용")]
    public bool normalShrimp;
    public bool CHS;
    public bool AS;
    public bool nLS;
    public bool parentS;
    public bool saskS;

    public bool normalDP;
    public bool angryDP;
    public bool SDP;
    public bool EggShrimp;
    public bool betta;

    public bool ghost_normalShrimp;
    public bool ghost_horseShrimp;
    public bool ghost_noRe_DP;

    //public static long moneyMax = 1000000000; 
    [Header("게임에 반영되는 수")]
    public int creatureCount; //생물 수
    public int shrimpsCount;//새우 수
    public int CHSCount;
    public int ASCount;
    public int noLSCount; //인없새우
    public int parentSCount; //육아
    public int saskScount; //사스카툰

    public int dpsCount;//뽁어수
    public int angryDPCount;//앵그리뽁
    public int SDPCount;
    
    public int bettaCount;

    public int ghost_CreatureCount; //고스트류
    public int ghost_ShrimpsCount;
    public int horse_ShrimpsCount;
    public int noRe_DPsCount;


    public Text shrimptextBuy;//새우 구매 카드 표시
    public Text dptextBuy; //복어 구매 카드 표시
    public Text bettatextBuy;//베타 카운트
    public Text textMoney;
    public Text textCreatureCount;
    public Text textPrice; //표시할 텍스트

    public Text textGhostCreatureCount;

    public GameObject prefabMoney;
    public GameObject prefabsiayanMoney;
    public GameObject prefabShrimp; //새우 프리팹
    public GameObject prefCHS;//방역영웅이새우
    public GameObject prefabAS;//우주비행사새
    public GameObject prefabNLS; //인업새우.
    public GameObject prefabParentS; //육아새우.
    public GameObject prefabSaskS; //사스크.


    public GameObject prefabDP; //복어 프리팹
    public GameObject prefabSDP;//깜짝복어
    public GameObject prefabADP;//앵그리뽁
    public GameObject prefSWEggs;//알뱄새우
    public GameObject prefBetta; //일반 베타

    public GameObject prefabG_Shrimps;//고스트새우
    public GameObject prefabH_Shrimps;//말이되새
    public GameObject prefabNR_DPs;//ㅇㅣ유없이 오는중인복


    public GameObject prefabFloor1, prefabFloor2; //바닥 프리팹
    public Button buttonPrice; //단가 업그레이드버튼
    public Button buttonBuyShrimps;//ㅅ쌔우구매.
    public Button buttonBuyDPs; //뽁어 구매
    public Button buttonBuyBettas;//베타구매

    [Header("마리모 킬 관련")]
    public bool isKilled;
    public bool isForgived;
    public int cutCount;
    public int killCount;
    
    SaveData saveData;
    FB_ImageLoader fb_il;
    CartoonManager cm;

    //--------- 기존 함수.

    public void Save()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        SuchoManager suchom = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
        SellGCreature sgc = GameObject.Find("SellManager").GetComponent<SellGCreature>();
        SaveData saveData = new SaveData();

        quitTime = System.DateTime.Now;
        saveData.quitTime = quitTime;
        saveData.money = money;
        saveData.moneyIncreaseAmount = moneyIncreaseAmount;
        saveData.moneyIncreaseLevel = moneyIncreaseLevel;
        saveData.moneyIncreasePrice = moneyIncreasePrice;

        saveData.eattenallPoint = eattenallPoint;
        saveData.eattenCreatureCount = eattenCreatureCount;

        saveData.normalShrimp = normalShrimp;
        saveData.EggShrimp = EggShrimp;
        saveData.CHS = CHS;
        saveData.AS = AS;
        saveData.nLS = nLS;
        saveData.parentS = parentS;
        saveData.saskS = saskS;

        saveData.normalDP = normalDP;
        saveData.angryDP = angryDP;
        saveData.SDP = SDP;
      
        saveData.betta = betta;

        saveData.ghost_normalShrimp = ghost_normalShrimp;
        saveData.ghost_horseShrimp = ghost_horseShrimp;
        saveData.ghost_noRe_DP = ghost_noRe_DP;

        saveData.creatureCount = creatureCount;
        saveData.shrimpsCount = shrimpsCount;
        saveData.ASCount = ASCount;
        saveData.CHScount = CHSCount;
        saveData.nLSCount = noLSCount;
        saveData.parentSCount = parentSCount;
        saveData.saskSCount = saskScount;
       
        saveData.dpsCount = dpsCount;
        saveData.SDPCount = SDPCount;
        saveData.bettaCount = bettaCount;

        saveData.ghost_CreatureCount = ghost_CreatureCount;
        saveData.ghost_ShrimpsCount = ghost_ShrimpsCount;
        saveData.horse_ShrimpsCount = horse_ShrimpsCount;
        saveData.noRe_DPsCount = noRe_DPsCount;

        saveData.autoMoneyIncreaseAmountShrimps = ShrimpsWork.sh_autoMoneyIncreaseAmount;
        saveData.autoIncreasePriceShrimps = ShrimpsWork.sh_autoIncreasePrice;
        saveData.autoMoneyIncreaseAmountDPs = DPWork.dp_autoMoneyIncreaseAmount;
        saveData.autoIncreasePriceDPs = DPWork.dp_autoIncreasePrice;
        saveData.autoMoneyIncreaseAmountBettas = BettaWork.betta_autoMoneyIncreaseAmount;
        saveData.autoIncreasePriceBettas = BettaWork.betta_autoIncreasePrice;

        saveData.sucho_autoMoneyIncreaseAmount = SuchoWork.sucho_autoMoneyIncreaseAmount;
        saveData.sucho_autoIncreasePrice_Vall = SuchoWork.sucho_autoIncreasePrice_Vall;
        saveData.sucho_autoIncreasePrice_Lud = SuchoWork.sucho_autoIncreasePrice_Lud;
        saveData.suchoPositions_Vals = suchom.suchoPositions_Vals;
        saveData.suchoPositions_Luds = suchom.suchoPositions_Luds;
        saveData.valCount = suchom.valCount;
        saveData.ludCount = suchom.ludCount;

        saveData.currentFloor = currentFloor;
        hangNameSaveTxt = nameText.text;
        saveData.hangName = hangNameSaveTxt;
        saveData.saiyanMrimoCoolTimeAd = currentCoolTime = sm.currentCoolTime;
        saveData.isAdCoolTime = sm.isCoolTime;

        saveData.curIndex = CartoonManager.curIndex;
        saveData.doneCartoon = new List<bool>();

        for(int i = 0; i < cm.doneCartoon.Length; ++i)
        {
            saveData.doneCartoon.Add(cm.doneCartoon[i]);
            //Debug.Log("저장 제대로 됐니? " + i + "  " + saveData.doneCartoon[i]);
            //에드로 해야하는거 아님?
        }
      

        /*saveData.done3wha = cm.done3wha;
        saveData.done4wha = cm.done4wha;
        saveData.done5wha = cm.done5wha;
        saveData.done6wha = cm.done6wha;
        saveData.done7wha = cm.done7wha;
        saveData.done8wha = cm.done8wha;
        saveData.done9wha = cm.done9wha;
        saveData.done10wha = cm.done10wha;
        saveData.done11wha = cm.done11wha;*/

        saveData.isMarimoSick = cdc.marimoIsSick;
        //saveData.isLose = cdc.islose;
        //saveData.loseActive = cdc.loseActive;
        //saveData.isLose = CommonDataController.hcho_static_lose;
        saveData.hchoLoseCoolTime = cdc.currentHchoCoolTime;

        saveData.sellP_Gshrimps = SellGCreature.sellP_Gshrimps;
        saveData.sold_GshrimpsCount = sgc.sold_GshrimpsCount;
        saveData.sellP_Hshrimps = SellGCreature.sellP_Hshrimps;
        saveData.sold_HshrimpsCount = sgc.sold_HshrimpsCount;
        saveData.sellP_NRDP = SellGCreature.sellP_NRDP;
        saveData.sold_NRDPCount = sgc.sold_NRDPCount;

        saveData.sellGhostCount = sgc.sellGhostCount;

        //마리모 킬 관련
      killCount = CommonDataController.killCount;
      saveData.killCount = killCount;
   //   Debug.Log("세이브시 킬카운트 " + killCount + "cdc킬카운트" + CommonDataController.killCount);
      saveData.isKilled = isKilled;
      saveData.isForgived = isForgived;
      killCount = saveData.killCount;
        cutCount = CommonDataController.cutCount;
        saveData.cutCount = cutCount;
//        Debug.Log("세이브시 컷카운ㅌ " + cutCount + "cdc컷카운트" + CommonDataController.cutCount);


        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
       // Debug.Log("ㅁㅓㄴㄷㅔ ㅇㅓㄷㅣㄱㅏㅁ ? gmㅋㅣㄹ드" + isKilled);
    }

    public void Load()
    {
        Time.timeScale = 1;
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        SuchoManager suchom = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
        SellGCreature sgc = GameObject.Find("SellManager").GetComponent<SellGCreature>();

        SaveData saveData = new SaveData();
        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);
        startTime = System.DateTime.Now;
    
        quitTime = saveData.quitTime;
        money = saveData.money;
        moneyIncreaseAmount = saveData.moneyIncreaseAmount;
        moneyIncreaseLevel = saveData.moneyIncreaseLevel;
        moneyIncreasePrice = saveData.moneyIncreasePrice;

        eattenallPoint = saveData.eattenallPoint;
        eattenCreatureCount = saveData.eattenCreatureCount;

        normalShrimp = saveData.normalShrimp;
        CHS = saveData.CHS;
        EggShrimp = saveData.EggShrimp;
        AS = saveData.AS;
        nLS = saveData.nLS;
        parentS = saveData.parentS;
        saskS = saveData.saskS;

        normalDP = saveData.normalDP;
        angryDP = saveData.angryDP;
        SDP = saveData.SDP;

        betta = saveData.betta;

        ghost_normalShrimp = saveData.ghost_normalShrimp;
        ghost_horseShrimp = saveData.ghost_horseShrimp;
        ghost_noRe_DP = saveData.ghost_noRe_DP;

        creatureCount = saveData.creatureCount;

        shrimpsCount = saveData.shrimpsCount;
        ASCount = saveData.ASCount;
        CHSCount = saveData.CHScount;
        noLSCount = saveData.nLSCount;
        parentSCount = saveData.parentSCount;
        saskScount = saveData.saskSCount;

        dpsCount = saveData.dpsCount;
        SDPCount = saveData.SDPCount;

        bettaCount = saveData.bettaCount;

        ghost_CreatureCount = saveData.ghost_CreatureCount;
        ghost_ShrimpsCount = saveData.ghost_ShrimpsCount;
        horse_ShrimpsCount = saveData.horse_ShrimpsCount;
        noRe_DPsCount = saveData.noRe_DPsCount;

        ShrimpsWork.sh_autoMoneyIncreaseAmount = saveData.autoMoneyIncreaseAmountShrimps;
        ShrimpsWork.sh_autoIncreasePrice = saveData.autoIncreasePriceShrimps;
        DPWork.dp_autoMoneyIncreaseAmount = saveData.autoMoneyIncreaseAmountDPs;
        DPWork.dp_autoIncreasePrice = saveData.autoIncreasePriceDPs;
        BettaWork.betta_autoMoneyIncreaseAmount = saveData.autoMoneyIncreaseAmountBettas;
        BettaWork.betta_autoIncreasePrice = saveData.autoIncreasePriceBettas;

        SuchoWork.sucho_autoMoneyIncreaseAmount = saveData.sucho_autoMoneyIncreaseAmount;
        SuchoWork.sucho_autoIncreasePrice_Vall = saveData.sucho_autoIncreasePrice_Vall;
        SuchoWork.sucho_autoIncreasePrice_Lud = saveData.sucho_autoIncreasePrice_Lud;
        suchom.suchoPositions_Vals = saveData.suchoPositions_Vals;
        suchom.suchoPositions_Luds = saveData.suchoPositions_Luds;
        suchom.valCount = saveData.valCount;
        suchom.ludCount = saveData.ludCount;

        currentFloor = saveData.currentFloor;
        hangNameSaveTxt = saveData.hangName;
        nameText.text = hangNameSaveTxt;
        sm.isSaiyanTime = false;
        sm.isCoolTime = saveData.isAdCoolTime;
        sm.currentCoolTime = currentCoolTime = saveData.saiyanMrimoCoolTimeAd ;

        CartoonManager.curIndex = saveData.curIndex;
        for (int i = 0; i < saveData.doneCartoon.Count; ++i)
        {
            
            cm.doneCartoon[i] = saveData.doneCartoon[i];
            //Debug.Log("불러오기 제대로 됐니? " + i + "  " + cm.doneCartoon[i]);
            //에드로 해야하는거 아님?
        }
        //
        //saveData.doneCartoon = cm.doneCartoon;
        /*        cm.done2wha = saveData.done2wha;
                cm.done3wha = saveData.done3wha;
                cm.done4wha = saveData.done4wha;
                cm.done5wha = saveData.done5wha;
                cm.done6wha = saveData.done6wha;
                cm.done7wha = saveData.done7wha;
                cm.done8wha = saveData.done8wha;
                cm.done9wha = saveData.done9wha;
                cm.done10wha = saveData.done10wha;
                cm.done11wha = saveData.done11wha;*/

        if (cdc.marimoIsSick)
        {
            ;
        }
        else
        {
            cdc.marimoIsSick = saveData.isMarimoSick;
        }
        //cdc.islose = saveData.isLose;
        //cdc.loseActive = saveData.loseActive;
        // CommonDataController.hcho_static_lose = saveData.isLose;
        cdc.currentHchoCoolTime = saveData.hchoLoseCoolTime;

        SellGCreature.sellP_Gshrimps = saveData.sellP_Gshrimps;
        sgc.sold_GshrimpsCount = saveData.sold_GshrimpsCount;
        SellGCreature.sellP_Hshrimps = saveData.sellP_Hshrimps;
        sgc.sold_HshrimpsCount = saveData.sold_HshrimpsCount;
        SellGCreature.sellP_NRDP = saveData.sellP_NRDP;
        sgc.sold_NRDPCount = saveData.sold_NRDPCount;

        sgc.sellGhostCount = saveData.sellGhostCount;

        isKilled = saveData.isKilled;
        isForgived = saveData.isForgived;
        if (!cdc.isKilled)
        {
            killCount = saveData.killCount;
            CommonDataController.killCount = killCount;
        }
        else
        {
            killCount = CommonDataController.killCount;
        }
        if (cm.doneCartoon[9] && !isForgived && !isKilled)
        {
            cm.doneCartoon[9] = false;
        }
        else if (isKilled || isForgived)
        {
            cm.doneCartoon[9] = true;
            saveData.doneCartoon[9] = true;
        }
       // Debug.Log("킬카운트" + CommonDataController.killCount);
       // Debug.Log("gm킬카운트" + killCount);
        killCount = CommonDataController.killCount;

        if (CommonDataController.cutCount == 0)
        {
            //Debug.Log("10화 아직 안대데");
            cutCount = saveData.cutCount;
            CommonDataController.cutCount += cutCount;
        }
        else if (CommonDataController.cutCount >= 1)
        {
           // Debug.Log("10화 댔는데" + "랴 1보다 크ㄷ");
            cutCount = CommonDataController.cutCount;
            // cutCount = CommonDataController.cutCount;
        }

        cutCount = CommonDataController.cutCount;
       // Debug.Log("컷카운트" + CommonDataController.cutCount);
       // Debug.Log("gm컷카운트" + cutCount);
        //cdc.isForgived = saveData.isForgived; //이거넣으면 광고 본거 소용이가 없어/

        TimeCounting();
        CheckCreature();
        FillCreature();
    }
    void Awake()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        fb_il = GameObject.Find("FB_ImageLoader").GetComponent<FB_ImageLoader>();
        Camera.main.GetComponent<CameraDrag>().limitMinY = -2;
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
          Load();
        }
        else
        {
            nameInputPanel.SetActive(true);
        }
        if (dpsCount <= 0)
        {
            dpsCount = 0; 
            if (creatureCount <= 0)
            {
                creatureCount = dpsCount + shrimpsCount +bettaCount;
            }
        }
        if (shrimpsCount <= 0)
        {
            shrimpsCount = 0;
            if (creatureCount <= 0)
            {
                creatureCount = dpsCount + shrimpsCount + bettaCount;
            }
        }
        if (creatureCount <= 0)
        {
            creatureCount = 0;
        }
        if (bettaCount <= 0)
        {
            bettaCount = 0;
            if (creatureCount <= 0)
            {
                creatureCount = dpsCount + shrimpsCount + bettaCount;
            }
        }
        else if(creatureCount != shrimpsCount + dpsCount + bettaCount)
        {
            creatureCount = shrimpsCount + dpsCount + bettaCount;
        }

//        Debug.Log("머니 인크리스트어마운" + moneyIncreaseAmount);
//        Debug.Log("머니 " + money);

    }
    private void Start()
    {   
        GoSpecialCartoon();
        SaveAfter10wha();
        AngryDiscount();
    }
    void Update()
    {
        NameBttnActiveCheck();
        ShowInfo();
        MoneyIncrease();
        ButtonActiveCheck();
        ButtonBuyActiveCheck();
        ButtonBuyActiveCheckDP();
        ButtonBuyActiveCheckBetta();
        UpdatePanelText();
        UpdatePanelBuy();
        UpdatePanelBuyDP();
        UpdatePanelBuyBetta();
        MaxLev();
        CreateFloor();
       // AngryDiscount();
        SaveAfter10wha();
        CheckCreature(); //--이거 왜 빠져있었지?

        if (Application.platform == RuntimePlatform.Android)   // 플렛폼 정보 .
        {
            if (Input.GetKey(KeyCode.Escape)) // 키 눌린 코드 신호를 받아오는것.
            {
                Application.Quit();
            }
        }

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            bPause = true;
            StopAllCoroutines();
            Save();
        }
        else
        {
            if (bPause)
            {
                bPause = false;
                Load();
            }
        }
    }
    void AppQuit()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    //-----------------------------------------------------------------------------------------
    //---만렙 달성
   public void MaxLev()
    {
        if(moneyIncreaseLevel == intmaxLev)
        {
            maxLev.SetActive(true);
        }
        else
        {
            maxLev.SetActive(false);
        }
    } 

    //----특정 만화 호출
   public void GoSpecialCartoon()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        if (cm.doneCartoon[3] && !cm.doneCartoon[4])
        {

            //Debug.LogError("깐다 5화 -20초 뒤에! ");

            // Invoke("InvokeCartoon", 13.0f);
            /*if (cm.curIndex == 0)
            {
                cm.curIndex += 3;
            }
            else if (cm.curIndex == 1)
            {
                cm.curIndex += 2;
            }
            else if (cm.curIndex == 2)
            {
                cm.curIndex += 1;
            }*/
            CartoonManager.curIndex = 3;
            fb_il.ShrimpGo(3, 7);
            Save();
        }
        else if (cm.doneCartoon[8] && !cm.doneCartoon[9] && !isKilled && !isForgived)
        {

            // Invoke("InvokeCartoon", 11.0f);

            /*if (cm.curIndex == 7)
            {
                cm.curIndex += 1;
            }
            if (cm.curIndex == 6)
            {
                cm.curIndex += 2;
            }
            if (cm.curIndex == 5)
            {
                cm.curIndex += 3;
            }
            else if (cm.curIndex == 4)
            {
                cm.curIndex += 4;
            }
            else if (cm.curIndex == 3)
            {
                cm.curIndex += 5;
            }
            else if (cm.curIndex == 2)
            {
                cm.curIndex += 6;
            }
            else if (cm.curIndex == 1)
            {
                cm.curIndex += 7;
            }
            else if (cm.curIndex == 0)
            {
                cm.curIndex += 8;
            }*/
            CartoonManager.curIndex = 8;
            fb_il.ShrimpGo(8, 8);


        }
        if (cdc.isForgived || cdc.isKilled)
        {
            CancelInvoke("InvokeCartoon");
            Debug.Log("캔슬인보크 되는겨?");
            cm.doneCartoon[9] = true;
        }
    }
    //저장하는 함수 혹시몰라서
    void SaveAfter10wha()
    {
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        if (isForgived)
        {
            cm.doneCartoon[9] = true;
        }
    }
    //생물 수 체크
    void CheckCreature()
    {
        if(shrimpsCount-(CHSCount + ASCount + noLSCount + parentSCount + saskScount) >= 1)
        {
            normalShrimp = true;
        }
        if(dpsCount-SDPCount >= 1)
        {
            normalDP = true;
        }
        if (angryDPCount >= 1)
        {
            angryDP = true;
        }
        if (CHSCount >= 1)
        {
            CHS = true;
        }
        if(ASCount >= 1)
        {
            AS = true;
        }
        if (noLSCount >= 1)
            nLS = true;
        if (parentSCount >= 1)
            parentS = true;
        if (saskScount >= 1)
            saskS = true;
        if (SDPCount >= 1)
        {
            SDP = true;
        }
        if (bettaCount >= 1)
        {
            betta = true;
        }

        if (ghost_ShrimpsCount >= 1)
        {
            ghost_normalShrimp = true;
        }
        if (horse_ShrimpsCount >= 1)
        {
            ghost_horseShrimp = true;
        }
        if (noRe_DPsCount >= 1)
        {
            ghost_noRe_DP = true;
        }
    }
    //생물 수 불러오기
    public void FillCreature()
    {
        GameObject[] shrimps = GameObject.FindGameObjectsWithTag("Shrimps");
        if (shrimpsCount - (CHSCount + ASCount + noLSCount + parentSCount + saskScount) != shrimps.Length)
        {
            StartCoroutine(SpawnShrimps());
        }

        GameObject[] dps = GameObject.FindGameObjectsWithTag("DPs");
        if (dpsCount - SDPCount != dps.Length)
        {
            StartCoroutine(SpawnDPs());
        }
        GameObject[] chss = GameObject.FindGameObjectsWithTag("CHS");
        if (CHSCount != chss.Length)
        {
            StartCoroutine(SpawnCHSs());
        }
        GameObject[] ass = GameObject.FindGameObjectsWithTag("AstronautShrimp");
        if (ASCount != ass.Length)
        {
            StartCoroutine(SpawnASs());
        }
        GameObject[] sdp = GameObject.FindGameObjectsWithTag("SupriseDP");
        if(SDPCount != sdp.Length)
        {
            StartCoroutine(SpawnSDPs());
        }
        GameObject[] bettas = GameObject.FindGameObjectsWithTag("Bettas");
        if(bettaCount != bettas.Length)
        {
            StartCoroutine(SpawnBettas());
        }

        GameObject[] g_shrimps = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");
        if(ghost_ShrimpsCount != g_shrimps.Length)
        {
            StartCoroutine(SpawnG_Shrimps());
        }
        GameObject[] h_shrimps = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");
        if(horse_ShrimpsCount != h_shrimps.Length)
        {
            StartCoroutine(SpawnH_Shrimps());
        }
        GameObject[] noRe_DPs = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");
        if (noRe_DPsCount != noRe_DPs.Length)
        {
            StartCoroutine(SpawnNoRe_DPs());
        }
        GameObject[] nolovs = GameObject.FindGameObjectsWithTag("NoLuvShrimp");
        if (noLSCount != nolovs.Length)
        {
            StartCoroutine(SpawnNoLuvS());
        }
        GameObject[] parenss = GameObject.FindGameObjectsWithTag("ParentShrimp");
        if (parentSCount != parenss.Length)
        {
            StartCoroutine(SpawnParentS());
        }
        GameObject[] saskss = GameObject.FindGameObjectsWithTag("SaskShrimp");
        if (saskScount != saskss.Length)
        {
            StartCoroutine(SpawnSaskS());
        }
    }
    IEnumerator SpawnShrimps()
    {
        GameObject[] shrimps = GameObject.FindGameObjectsWithTag("Shrimps");

        float randTime = (float)Random.Range(0.001f, 0.1f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = shrimps.Length; i < shrimpsCount - (CHSCount+ASCount); i++)
        {
            float spotX = marimoSpot.x + (i % width) * space;
            float spotY = marimoSpot.y - (i / width) * space;
                
            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabShrimp, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
   
    IEnumerator SpawnDPs()
    {
        GameObject[] dps = GameObject.FindGameObjectsWithTag("DPs");

        float randTime = (float)Random.Range(0.01f, 0.05f);
        float rand = (float)Random.Range( 0.2f, 0.7f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = dps.Length; i < dpsCount - SDPCount; i++)
        {
            float spotX = marimoSpot.x + (i % (width - rand)) * space * (rand + rand);
            float spotY = marimoSpot.y - (i / (width - rand)) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabDP, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnBettas()
    {
        GameObject[] bettas = GameObject.FindGameObjectsWithTag("Bettas");

        float randTime = (float)Random.Range(0.01f, 0.05f);
        float rand = (float)Random.Range(1.0f, 1.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.5f);
        for (int i = bettas.Length; i < bettaCount; i++)
        {
            float spotX = marimoSpot.x + (i % (0.25f * width - rand)) * space * (rand + rand);
            float spotY = marimoSpot.y - (i / (0.25f * width)) * space/3 * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefBetta, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }

    //---스페셜들
    IEnumerator SpawnNoLuvS()
    {
        GameObject[] nolovs = GameObject.FindGameObjectsWithTag("NoLuvShrimp");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(1.5f, 2.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = nolovs.Length; i < noLSCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabNLS, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnParentS()
    {
        GameObject[] parenss = GameObject.FindGameObjectsWithTag("ParentShrimp");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(1.5f, 2.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = parenss.Length; i < parentSCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabParentS, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnSaskS()
    {
        GameObject[] saskss = GameObject.FindGameObjectsWithTag("SaskShrimp");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(1.5f, 2.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = saskss.Length; i < saskScount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabSaskS, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnCHSs()
    {
        GameObject[] chss = GameObject.FindGameObjectsWithTag("CHS");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(1.5f, 2.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = chss.Length; i < CHSCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefCHS, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnASs()
    {
        GameObject[] ass = GameObject.FindGameObjectsWithTag("AstronautShrimp");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(1.5f, 2.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = ass.Length; i < ASCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabAS, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnSDPs()
    {
        GameObject[] sdp = GameObject.FindGameObjectsWithTag("SupriseDP");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(0.2f, 0.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = sdp.Length; i < SDPCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabSDP, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }


    IEnumerator SpawnG_Shrimps()
    {
        GameObject[] g_shrimps = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = g_shrimps.Length; i < ghost_ShrimpsCount; i++)
        {
            float spotX = (marimoSpot.x + (i % width) * space) + 0.4f;
            float spotY = (marimoSpot.y - (i / width) * space) + 0.55f;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabG_Shrimps, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnH_Shrimps()
    {
        GameObject[] h_shrimps = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.2f);
        for (int i = h_shrimps.Length; i < horse_ShrimpsCount; i++)
        {
            float spotX = (marimoSpot.x + (i % width) * space) + 0.1f;
            float spotY = (marimoSpot.y - (i / width) * space) + 0.1f;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabH_Shrimps, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }
    IEnumerator SpawnNoRe_DPs()
    {
        GameObject[] noReDPs = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");

        float randTime = (float)Random.Range(0.01f, 0.1f);
        float rand = (float)Random.Range(0.2f, 0.5f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        for (int i = noReDPs.Length; i < noRe_DPsCount; i++)
        {
            float spotX = marimoSpot.x + (i % width) * space * rand;
            float spotY = marimoSpot.y - (i / width) * space * rand;

            yield return new WaitForSeconds(randTime);
            GameObject obj = Instantiate(prefabNR_DPs, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
    }

    //바닥 만들기 함수 
    public void CreateFloor()
    {
        Vector2 bgPosition = GameObject.Find("BG").transform.position;
        float spotX = bgPosition.x;
        int nextFloor = ((creatureCount + 4) / floorCapacity) - 1;
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Add_Floor");
        if (currentFloor != floors.Length)
        {
            for (int i = floors.Length + 1; i <= currentFloor; i++)
            {
                if (i % 2 == 1)
                {
                    float spotY = fixedY - ((i - 1) * spaceFloor);
                    GameObject newbg = Instantiate(prefabFloor1, new Vector2(spotX, spotY), Quaternion.identity);
                    Camera.main.GetComponent<CameraDrag>().limitMinY = fixedY - ((i - 1) * spaceFloor) + 4;
                }
                else if (i % 2 == 0)
                {
                    float spotY = fixedY - ((i - 1) * spaceFloor);
                    GameObject newbg = Instantiate(prefabFloor2, new Vector2(spotX, spotY), Quaternion.identity);
                    Camera.main.GetComponent<CameraDrag>().limitMinY = fixedY - ((i - 1) * spaceFloor) + 4;
                    break;
                }
            }
        }
        if (nextFloor >= currentFloor)
        {
            currentFloor += 1;
        }
    }

    //소지금 증가 
    void MoneyIncrease()
    {
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        
        if (Input.GetMouseButtonDown(0))
        {
            CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


              if (cdc.marimoIsSick || isKilled)
               {fXController_ep1.EndSFX();
                   
                }
              else if(cm.doneCartoon[8] && !cm.doneCartoon[9])
                {
                    fXController_ep1.EndSFX();
                   
                }
               else
               {
                    fXController_ep1.Tab();
                    if (sm.isSaiyanTime)
                    {
                        Instantiate(prefabsiayanMoney, new Vector3(mousePosition.x, mousePosition.y, mousePosition.z), Quaternion.identity);
                        money += 3 * moneyIncreaseAmount;
                    }
                    else { money += moneyIncreaseAmount;
                        Instantiate(prefabMoney, new Vector3(mousePosition.x, mousePosition.y, mousePosition.z), Quaternion.identity);
                    }

                    if (money >= 11 && !cm.doneCartoon[1])//--만화 화 조절 돈 10원 벌었을 때 2화
                    {
                        //cm.LoadCartoon();
                        cm.doneCartoon[1] = true;
                        fb_il.ShrimpGo(1, 7);

                        Save();
                    }
                    if (moneyIncreaseLevel > 13 && cm.doneCartoon[2] && !cm.doneCartoon[3])//--만화4화
                    {
                       
                        for (int i= 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }
                        CartoonManager.curIndex++;
                        //cm.LoadCartoon();
                        fb_il.ShrimpGo(3, 7);
                        Debug.Log("13렙 많와 " + "애는 그 더하기 두CartoonManager에함  " + CartoonManager.curIndex);

                        cm.doneCartoon[3] = true;
                        Save();
                    }
                    if (moneyIncreaseLevel >= 47 && !cm.doneCartoon[5] && cm.doneCartoon[4])//--만화 화 6화
                    {
                        CartoonManager.curIndex++;

                        fb_il.ShrimpGo(4, 7);

                        for (int i = 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }
                        Debug.Log("47렙 많와 " + "애는 그 더하기 앞에함  " + CartoonManager.curIndex);

                        /*if (CartoonManager.curIndex == 3)
                        {
                            CartoonManager.curIndex += 1;
                        }
                        if (CartoonManager.curIndex == 2)
                        {
                            CartoonManager.curIndex += 2;
                        }
                        else if (CartoonManager.curIndex == 1)
                        {
                            CartoonManager.curIndex += 3;
                        }
                        else if (CartoonManager.curIndex == 0)
                        {
                            CartoonManager.curIndex += 4;
                        }*/
                        //cm.LoadCartoon();
                        cm.doneCartoon[5] = true;
                        Save();
                       
                    }
                    else if (moneyIncreaseLevel >= 61  && !cm.doneCartoon[6] && cm.doneCartoon[5])//--만화 화 7화
                    {//- 카툰매니저 여기서 부터 고쳐야함!! cur index 말이CartoonManager
                        CartoonManager.curIndex = 5;
                        fb_il.ShrimpGo(5, 7);

                        for (int i = 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }/*
                        if (CartoonManager.curIndex == 4)
                        {
                            CartoonManager.curIndex += 1;
                        }
                        if (CartoonManager.curIndex == 3)
                        {
                            CartoonManager.curIndex += 2;
                        }
                        else if (CartoonManager.curIndex == 2)
                        {
                            CartoonManager.curIndex += 3;
                        }
                        else if (CartoonManager.curIndex == 1)
                        {
                            CartoonManager.curIndex += 4;
                        }
                        else if (CartoonManager.curIndex == 0)
                        {
                            CartoonManager.curIndex += 5;
                        }*/
                        //cm.LoadCartoon();
                        cm.doneCartoon[6] = true;
                        Save();
                        
                    }
                    else if( moneyIncreaseLevel >= 71 && !cm.doneCartoon[7] && cm.doneCartoon[6])
                    {
                        CartoonManager.curIndex = 6;
                        fb_il.ShrimpGo(6, 7);
                        for (int i = 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }
                       // cm.LoadCartoon();
                        cm.doneCartoon[7] = true;
                        Save();
                        
                    }
                    else if(moneyIncreaseLevel >= 89 && !cm.doneCartoon[8] && cm.doneCartoon[7])//--9화
                    {
                        CartoonManager.curIndex = 7;
                        fb_il.ShrimpGo(7, 7);
                        for (int i = 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }
                       
                      //  cm.LoadCartoon();
                        cm.doneCartoon[8] = true; 
                        Save();
                        
                    }
                    else if (moneyIncreaseLevel >= 97 && !cm.doneCartoon[10] && cm.doneCartoon[9])//--11화
                    {
                        CartoonManager.curIndex = 9;
                        fb_il.ShrimpGo(8, 7);
                        for (int i = 0; i <= CartoonManager.curIndex; ++i)
                        {
                            cm.doneCartoon[i] = true;
                        }
                       
                        Debug.Log("11화가야하는데?!");
                      //  cm.LoadCartoon();
                        cm.doneCartoon[10] = true;
                        Save();

                    }
                    Debug.Log("cm.curIndex" + CartoonManager.curIndex + "  이거 바뀌나봐 ㅠㅊ");
                }

            }
        }
    }
    //쇼인포

    public void UpdateAll()
    {
        ShowInfo();
        UpdatePanelText();
        UpdatePanelBuy();
        UpdatePanelBuyDP();
        UpdatePanelBuyBetta();
    }
    public void ShowInfo()
    {
        if (money == 0)
            textMoney.text = "0 물력";
        else
            textMoney.text = money.ToString("###,###") + " 물력";
        if (creatureCount == 0)
            textCreatureCount.text = "0 마리";
        else
            textCreatureCount.text = creatureCount.ToString("###,###") + " 마리";
        if (ghost_CreatureCount == 0)
            textGhostCreatureCount.text = "0 마리";
        else
            textGhostCreatureCount.text = ghost_CreatureCount.ToString("###,###") + " 마리";
    }

    //패널 텍스트 업데이트
    void UpdatePanelText()
    {
        textPrice.text = "생산물력 Lv." + moneyIncreaseLevel + " \n\n";
        textPrice.text += "손길 당 생산물력: \n";
        textPrice.text += moneyIncreaseAmount.ToString("###,###") + " 물력 \n\n";
        textPrice.text += "업그레이드 비용: \n";
        textPrice.text += moneyIncreasePrice.ToString("###,###") + " 물력";
    }
    void UpdatePanelBuy()
    {
        shrimptextBuy.text = "새우 수: " + shrimpsCount + " \n\n";
        shrimptextBuy.text += "새우 당 생산물력:"+ "  ";
        shrimptextBuy.text += ShrimpsWork.sh_autoMoneyIncreaseAmount.ToString("###,###") + " 물력 \n";
        shrimptextBuy.text += "번식 비용: " + "  ";
        shrimptextBuy.text += ShrimpsWork.sh_autoIncreasePrice.ToString("###,###") + " 물력";
    }
    void UpdatePanelBuyDP()
    {
        dptextBuy.text = "인디언 복어 수: " + dpsCount + " \n\n";
        dptextBuy.text += "인뽁은 다른 생물을 공격한다. 구매에 주의해." + "\n\n";
        dptextBuy.text += "인복 당 생산물력: "+"  ";
        dptextBuy.text += DPWork.dp_autoMoneyIncreaseAmount.ToString("###,###") + " 물력 \n";
        dptextBuy.text += "번식 비용: " +  "  ";
        dptextBuy.text += DPWork.dp_autoIncreasePrice.ToString("###,###") + " 물력";
    }
    void UpdatePanelBuyBetta()
    {
        bettatextBuy.text = "베타 수: " + bettaCount + " \n\n";
        bettatextBuy.text += "베타는 다른 생물에게 배타적이다. 한 마리만 사 " + "\n\n";
        bettatextBuy.text += "베타 당 생산물력: " + "  ";
        bettatextBuy.text += BettaWork.betta_autoMoneyIncreaseAmount.ToString("###,###") + " 물력 \n";
        bettatextBuy.text += "구매 비용: " + "  ";
        bettatextBuy.text += BettaWork.betta_autoIncreasePrice.ToString("###,###") + " 물력";
    }

    //----버튼 작동----
    public void OnClickNameBttn() //--이름입력
    {
        nameText.text = nameInput.text;
        hangNameSaveTxt = nameText.text;
        Save();
        nameInputPanel.SetActive(false);
    }
    public void UpgradePrice()
    {
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

        if (money >= moneyIncreasePrice)
        {
            money -= moneyIncreasePrice;
            moneyIncreaseLevel += 1;
            moneyIncreaseAmount += moneyIncreaseLevel * 5;
            moneyIncreasePrice += moneyIncreaseLevel * 2029; //적정 2029
            if (moneyIncreaseLevel > 13 && cm.doneCartoon[2] && !cm.doneCartoon[3]) //--4
            {
                CartoonManager.curIndex = 2;
                fb_il.ShrimpGo(2, 7);
                //cm.LoadCartoon();
                cm.doneCartoon[3] = true;
                Save();
            }
        }
    }
    public void BuyShrimps()
    {
        if (money >= ShrimpsWork.sh_autoIncreasePrice)
        {
            money -= ShrimpsWork.sh_autoIncreasePrice;
            creatureCount += 1;
            shrimpsCount += 1;
            ShrimpsWork.sh_autoMoneyIncreaseAmount += moneyIncreaseLevel * 1;
            ShrimpsWork.sh_autoIncreasePrice += moneyIncreaseLevel * shrimpsCount * 211 - eattenallPoint * eattenCreatureCount; //적정 211

            CreateShrimps();

            CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
            if (shrimpsCount > 2 && !cm.doneCartoon[2]) //--만화3화
            {
                CartoonManager.curIndex = 1;
                fb_il.ShrimpGo(1, 7);
                for (int i = 0; i <= CartoonManager.curIndex; ++i)
                {
                    cm.doneCartoon[i] = true;
                }
               // cm.LoadCartoon();
                cm.doneCartoon[2] = true;
                Save();
            }
            if (moneyIncreaseLevel > 13 && cm.doneCartoon[2] && !cm.doneCartoon[3])//--만화4화
            {
                CartoonManager.curIndex = 2;
                fb_il.ShrimpGo(2, 7);
                for (int i = 0; i <= CartoonManager.curIndex; ++i)
                {
                    cm.doneCartoon[i] = true;
                }
               // cm.LoadCartoon();
                cm.doneCartoon[3] = true;
               Save();
            }
        }

    }
    public void BuyDPs()
    {
        if (money >= DPWork.dp_autoIncreasePrice)
        {
            money -= DPWork.dp_autoIncreasePrice;
            creatureCount += 1;
            dpsCount += 1;
            
            DPWork.dp_autoMoneyIncreaseAmount += moneyIncreaseLevel * 2;
            DPWork.dp_autoIncreasePrice += moneyIncreaseLevel * dpsCount * 3331 - eattenallPoint * eattenCreatureCount; //적정 3331
            CreateDPs();

           CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
            if (dpsCount > 1 && !cm.doneCartoon[2]) //--만화3화
            {
                CartoonManager.curIndex = 1;
                fb_il.ShrimpGo(1, 7);
                for (int i = 0; i <= CartoonManager.curIndex; ++i)
                {
                    cm.doneCartoon[i] = true;
                }
            //    cm.LoadCartoon();
                cm.doneCartoon[2] = true;
                Save();
            }
            if (moneyIncreaseLevel > 13 && cm.doneCartoon[2] && !cm.doneCartoon[3]) //--4화
            {
                CartoonManager.curIndex = 2;
                fb_il.ShrimpGo(2, 7);
                for (int i = 0; i <= CartoonManager.curIndex; ++i)
                {
                    cm.doneCartoon[i] = true;
                }
                //cm.LoadCartoon();
                cm.doneCartoon[3] = true;
               Save();
            }
        }
    }
    public void BuyBettas()
    {
        if (money >= BettaWork.betta_autoIncreasePrice)
        {
            money -= BettaWork.betta_autoIncreasePrice;
            creatureCount += 1;
            bettaCount += 1;

            BettaWork.betta_autoMoneyIncreaseAmount += moneyIncreaseLevel * 11; //11 넘 좋은가?
            BettaWork.betta_autoIncreasePrice += moneyIncreaseLevel * bettaCount * 33533 - eattenallPoint * eattenCreatureCount; //적정 33533
            CreateBettas();
        }
    }


    //생물 구매.
    void CreateShrimps()
    {
        int randShrimps = Random.Range(1,100);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        float spotX = marimoSpot.x + (creatureCount % width) * space;
        float spotY = marimoSpot.y - (creatureCount / width) * space;
        if (randShrimps <= 13)
        {
            int randSpShrimps = Random.Range(1, 100);
            if(randSpShrimps <= 95)
            {
                if(randSpShrimps > 90)
                {
                    GameObject obj = Instantiate(prefabNLS, new Vector2(spotX, spotY), Quaternion.identity);
                    obj.transform.parent = creaturesGroup.transform;
                    //Debug.Log("인기없새우 ㅠㅠ  탄생!!");
                    noLSCount += 1;
                    nLS = true;
                }
                else if(randSpShrimps > 70 && randSpShrimps <= 90)
                {
                    GameObject obj = Instantiate(prefabParentS, new Vector2(spotX, spotY), Quaternion.identity);
                    obj.transform.parent = creaturesGroup.transform;
                    //Debug.Log("육아하새우우우웅  탄생!!");
                    parentSCount += 1;
                    parentS = true;
                }
                else if(randSpShrimps > 50 && randSpShrimps <= 70)
                {
                    GameObject obj = Instantiate(prefabSaskS, new Vector2(spotX, spotY), Quaternion.identity);
                    obj.transform.parent = creaturesGroup.transform;
                    //Debug.Log("사스카툰  탄생!!");
                    saskScount += 1;
                    saskS = true;
                }
                else if(randSpShrimps > 25 && randSpShrimps <=50)
                {
                    GameObject obj = Instantiate(prefCHS, new Vector2(spotX, spotY), Quaternion.identity);
                    obj.transform.parent = creaturesGroup.transform;
                    //Debug.Log("방역영웅이새우 탄생!!");
                    CHSCount += 1;
                    CHS = true;
                }
                else if (randSpShrimps <= 25)
                {
                    GameObject obj = Instantiate(prefabAS, new Vector2(spotX, spotY), Quaternion.identity);
                    obj.transform.parent = creaturesGroup.transform;
                    //Debug.Log("우주비행사새우 탄생!");
                    ASCount += 1;
                    AS = true;
                }
            }
            else if(randSpShrimps > 95)
            {
                GameObject obj = Instantiate(prefSWEggs, new Vector2(spotX, spotY), Quaternion.identity);
                obj.transform.parent = creaturesGroup.transform;
                //Debug.Log("알뱄새우 탄생!!");
                EggShrimp = true;
            }
        }
        else if(randShrimps > 13)
        {
            normalShrimp = true;
            GameObject obj = Instantiate(prefabShrimp, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }
        else
        {
            ;
        }
       
    }
    void CreateDPs()
    {
        int randDPs = Random.Range(1, 100);
        float randX = Random.Range(-2.2f, 2.2f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        float spotX = randX + (creatureCount % width) * space;
        float spotY = marimoSpot.y - (creatureCount / width) * space;
        if(randDPs <= 20)
        {
            SDP = true;
            GameObject obj = Instantiate(prefabSDP, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
            //Debug.Log("깜짝삐에로인뽁 탄생!");
            SDPCount += 1;
        }
        else if(randDPs > 20)
        {
            normalDP = true;
            GameObject obj = Instantiate(prefabDP, new Vector2(spotX, spotY), Quaternion.identity);
            obj.transform.parent = creaturesGroup.transform;
        }

    }
    void CreateBettas()
    {
        float randX = Random.Range(-2.2f, 2.2f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.2f);
        float spotX = randX + (creatureCount % (width/2)) * space;
        float spotY = marimoSpot.y - (creatureCount / (0.25f * width)) * space/10;
        betta = true;
        GameObject obj = Instantiate(prefBetta, new Vector2(spotX, spotY), Quaternion.identity);
        obj.transform.parent = creaturesGroup.transform;

    }
    public void CreateAngryPuffers()
    {
        float randX = Random.Range(-2.0f, 0.0f);
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        float spotX = randX + (creatureCount % width) * space;
        float spotY = marimoSpot.y;
        
        GameObject obj = Instantiate(prefabADP, new Vector2(spotX, spotY), Quaternion.identity);
        obj.transform.parent = creaturesGroup.transform;
        AngryDiscount();
    }

    public void CreateG_Shrimps()
    {
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        float spotX = marimoSpot.x + (creatureCount % width) * space;
        float spotY = marimoSpot.y - (creatureCount / width) * space;

        GameObject obj = Instantiate(prefabG_Shrimps, new Vector2(spotX, spotY), Quaternion.identity);
        obj.transform.parent = creaturesGroup.transform;
    }
    public void CreateH_Shrimps()
    {
        Vector2 marimoSpot = new Vector2(-1.7f, -1.0f);
        float spotX = marimoSpot.x + (creatureCount % width) * space;
        float spotY = marimoSpot.y - (creatureCount / width) * space;

        GameObject obj = Instantiate(prefabH_Shrimps, new Vector2(spotX, spotY), Quaternion.identity);
        obj.transform.parent = creaturesGroup.transform;

    }
    public void CreateNoRe_DPs()
    {
        Vector2 marimoSpot = new Vector2(-2.0f, 0.0f);
        float spotX = marimoSpot.x + (creatureCount % width) * space;
        float spotY = marimoSpot.y - (creatureCount / width) * space;

        GameObject obj =  Instantiate(prefabNR_DPs, new Vector2(spotX, spotY), Quaternion.identity);
        obj.transform.parent = creaturesGroup.transform;

    }

    //--- 버튼 쳌
    void NameBttnActiveCheck() //--어항 이름 버튼 체크!.
    {
        if (nameInput.text == "")
        {
            if (hangNameSaveTxt == "이 곳을 눌러 어항에 이름을 붙여주세요.")
            {
                namePlaceHolder.text = "예시) 환생마리모 다운로드 해주신 여러분 쪼항 <3";
            }
            else if(hangNameSaveTxt != "")
            {
                namePlaceHolder.text = "현재 내 어항 이름 :" + hangNameSaveTxt;
            }
            else
            {
                namePlaceHolder.text = "예시) 환생마리모 다운로드 해주신 여러분 쪼항 <3";
                hangNameSaveTxt = "이 곳을 눌러 어항에 이름을 붙여주세요.";
                nameText.text = hangNameSaveTxt;
            }
            
            nameConfirmBttn.interactable = false;
        }
        else if(nameInput.text !="")
        {
            nameConfirmBttn.interactable = true;
        }
        else
        {
            nameConfirmBttn.interactable = false;
        }
    }
    void ButtonActiveCheck()
    {
        if (money >= moneyIncreasePrice && moneyIncreaseLevel < intmaxLev)
        {
            buttonPrice.interactable = true;
        }
        else
        {
            buttonPrice.interactable = false;
        }
    }
    void ButtonBuyActiveCheck()
    {
        if (money >= ShrimpsWork.sh_autoIncreasePrice)
        {
            buttonBuyShrimps.interactable = true;
        }
        else
        {
            buttonBuyShrimps.interactable = false;
        }
    }
    void ButtonBuyActiveCheckDP()
    {
        if (money >= DPWork.dp_autoIncreasePrice)
        {
            buttonBuyDPs.interactable = true;
        }
        else
        {
            buttonBuyDPs.interactable = false;
        }
    }
    void ButtonBuyActiveCheckBetta()
    {
        if (money >= BettaWork.betta_autoIncreasePrice && moneyIncreaseLevel >= intmaxLev)
        {
            buttonBuyBettas.interactable = true;
        }
        else
        {
            buttonBuyBettas.interactable = false;
        }
    }

    /*void InvokeCartoon()
    {
        CartoonManager cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        if (!cm.doneCartoon[4])
        {
            cm.doneCartoon[4] = true;
        }
        else if (!cm.doneCartoon[9])
        {
            cm.doneCartoon[9] = true;
            Debug.Log("인보크카툰 게임메니저에서");
        }
       // OnlyMoneyManager.hchomoney += 1;
        Save();
        Debug.Log("이거 저장 안대나?");
        LoadingManager.LoadScene("2Cartoon");
    }*/
    void TimeCounting()
    {
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();

        System.TimeSpan timeDiff = startTime - quitTime;

        double diffHoures = timeDiff.TotalHours;

        if(diffHoures > 1)
        {
            sm.currentCoolTime = 0;
            sm.isCoolTime = false;
        }
        else
        {
            double diffTotalMin = timeDiff.TotalMinutes;
            int intdiffTotalMin = (int)System.Math.Round(diffTotalMin);
            if(sm.currentCoolTime - intdiffTotalMin > 0)
            {
                sm.currentCoolTime = sm.currentCoolTime - intdiffTotalMin;
                sm.isCoolTime = isCoolTime = true;
            }
            else if(diffTotalMin <= 0)
            {
                sm.currentCoolTime = currentCoolTime;
                sm.isCoolTime = isCoolTime = true;
            }
            else if (diffTotalMin > 61)
            {
                sm.currentCoolTime = 0;
                sm.isCoolTime = false;
            }
            else
            {
                sm.currentCoolTime = 0;
                sm.isCoolTime = false;
            }
        }
    }
    public void AngryDiscount()
    {
        if(eattenallPoint <= 0)
        {
            angryDiscount.SetActive(false);
        }
        else
        {
            angryDiscount.SetActive(true);
            angryDiscountPoint.text = (eattenallPoint * eattenCreatureCount).ToString("###,###");
        }
    }
}
