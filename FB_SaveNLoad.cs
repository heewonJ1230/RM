using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class FB_SaveNLoad : MonoBehaviour
{
    DatabaseReference reference;
   // public InputField Email;
    public Text showLoadedText;
    public Text hangname;
    public Text accountEmailCheck;

    Firebase.Auth.FirebaseUser user;

   
    string stageProgress = null;
    public string uid;

    [Header("테스트 아이디 모음")]
    //public string[] testID; 
    public string[] testhangNames;
    public int[] testCounts; 
    //--알림창 --

    GameManager gm;
    CartoonManager cm;
    CommonDataController cdc;
    SuchoManager sm;


    
    //수초위치 저장
    List<string> sucho_vals_x = new List<string>();
    List<string> sucho_vals_y = new List<string>();
    string combi_val_Xs, combi_val_Ys;

    List<string> sucho_luds_x = new List<string>();
    List<string> sucho_luds_y = new List<string>();
    string combi_lud_Xs, combi_lud_Ys;



    public void getFirebaseUser()
    {
       // Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        user = FB_Login_Email.auth.CurrentUser;
        uid = null;
        if (user != null)
        {
            if (user.IsEmailVerified)
            {
                uid = user.UserId;
            }
            else
            {
              ;
            }
           
        }
        else {
            ;
        }
        
    }
    private void Awake()
    {
        
    }
    
    void Start()
    { 
        reference = FirebaseDatabase.DefaultInstance.RootReference;
      
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        sm = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();
    }


    public void FBSaveData()
    {
        getFirebaseUser();
        reference.Child("Users").Child(uid).Child("memberNum").SetValueAsync(uid);
        reference.Child("Users").Child(uid).Child("marimoSick").SetValueAsync(cdc.marimoIsSick.ToString());
        reference.Child("Users").Child(uid).Child("money").SetValueAsync(gm.money.ToString());
        reference.Child("Users").Child(uid).Child("moneyIncreaseAmount").SetValueAsync(gm.moneyIncreaseAmount.ToString());
        reference.Child("Users").Child(uid).Child("moneyIncreaseLevel").SetValueAsync(gm.moneyIncreaseLevel.ToString());
        reference.Child("Users").Child(uid).Child("moneyIncreasePrice").SetValueAsync(gm.moneyIncreasePrice.ToString());

        reference.Child("Users").Child(uid).Child("currentFloor").SetValueAsync(gm.currentFloor.ToString());
        //--- 테스트데이터
        /*for(int i = 0; i<100; i++)
        {
            FakeSave(i);
        }*/
      
        //---- 화났뽁
        reference.Child("Users").Child(uid).Child("eattenallPoint").SetValueAsync(gm.eattenallPoint.ToString());
        reference.Child("Users").Child(uid).Child("eattenCreatureCount").SetValueAsync(gm.eattenCreatureCount.ToString());

        //----만화 화 체크
        for(int i = 0; i <= cm.doneCartoon.Length; ++i)
        {
            if (cm.doneCartoon[i])
            {   
            }
            else
            {
                stageProgress = i.ToString();
            }
            
        }
        /*
        if (!cm.done2wha) stageProgress = "1";
        else if (!cm.done3wha) stageProgress = "2";
        else if (!cm.done4wha) stageProgress = "3";
        else if (!cm.done5wha) stageProgress = "4";
        else if (!cm.done6wha) stageProgress = "5";
        else if (!cm.done7wha) stageProgress = "6";
        else if (!cm.done8wha) stageProgress = "7";
        else if (!cm.done9wha) stageProgress = "8";
        else if (!cm.done10wha) stageProgress = "9";
        else if (!cm.done11wha) stageProgress = "10";
        else
            stageProgress = "11";*/
        reference.Child("Users").Child(uid).Child("stageProgress").SetValueAsync(stageProgress);
        Debug.Log("저장된 스테이지프로그래스는  " + stageProgress);
        reference.Child("Users").Child(uid).Child("tankName").SetValueAsync(gm.hangNameSaveTxt);


        //--- 카드 체크----
        //---일반 
        reference.Child("Users").Child(uid).Child("creatureCard").Child("normalShrimp").SetValueAsync(gm.normalShrimp.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("EggShrimp").SetValueAsync(gm.EggShrimp.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("angryDP").SetValueAsync(gm.angryDP.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("CHS").SetValueAsync(gm.CHS.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("AS").SetValueAsync(gm.AS.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("NoluvS").SetValueAsync(gm.nLS.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("ParentS").SetValueAsync(gm.parentS.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("SaskS").SetValueAsync(gm.saskS.ToString());

        reference.Child("Users").Child(uid).Child("creatureCard").Child("normalDP").SetValueAsync(gm.normalDP.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("SDP").SetValueAsync(gm.SDP.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("betta").SetValueAsync(gm.betta.ToString());

        //--고스트
        reference.Child("Users").Child(uid).Child("creatureCard").Child("ghost_normalShrimp").SetValueAsync(gm.ghost_normalShrimp.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("ghost_horseShrimp").SetValueAsync(gm.ghost_horseShrimp.ToString());
        reference.Child("Users").Child(uid).Child("creatureCard").Child("ghost_noRe_DP").SetValueAsync(gm.ghost_noRe_DP.ToString());

        SaveNoOfShrimps();

        //--마리모킬관련
        reference.Child("Users").Child(uid).Child("isForgived").SetValueAsync(gm.isForgived.ToString());
        reference.Child("Users").Child(uid).Child("isKilled").SetValueAsync(gm.isKilled.ToString());
        reference.Child("Users").Child(uid).Child("killCount").SetValueAsync(gm.killCount.ToString());

        //--gm외 다른 파일 보내는거
        reference.Child("Users").Child(uid).Child("otherPrice").Child("sh_autoMoneyIncreaseAmount").SetValueAsync(ShrimpsWork.sh_autoMoneyIncreaseAmount.ToString());
        reference.Child("Users").Child(uid).Child("otherPrice").Child("sh_autoIncreasePrice").SetValueAsync(ShrimpsWork.sh_autoIncreasePrice.ToString());
        reference.Child("Users").Child(uid).Child("otherPrice").Child("dp_autoMoneyIncreaseAmount").SetValueAsync(DPWork.dp_autoMoneyIncreaseAmount.ToString());
        reference.Child("Users").Child(uid).Child("otherPrice").Child("dp_autoIncreasePrice").SetValueAsync(DPWork.dp_autoIncreasePrice.ToString());
        reference.Child("Users").Child(uid).Child("otherPrice").Child("betta_autoMoneyIncreaseAmount").SetValueAsync(BettaWork.betta_autoMoneyIncreaseAmount.ToString());
        reference.Child("Users").Child(uid).Child("otherPrice").Child("betta_autoIncreasePrice").SetValueAsync(BettaWork.betta_autoIncreasePrice.ToString());

        //--수초
        reference.Child("Users").Child(uid).Child("suchoData").Child("sucho_autoMoneyIncreaseAmount").SetValueAsync(SuchoWork.sucho_autoMoneyIncreaseAmount.ToString());
        reference.Child("Users").Child(uid).Child("suchoData").Child("sucho_autoIncreasePrice_Vall").SetValueAsync(SuchoWork.sucho_autoIncreasePrice_Vall.ToString());
        reference.Child("Users").Child(uid).Child("suchoData").Child("sucho_autoIncreasePrice_Lud").SetValueAsync(SuchoWork.sucho_autoIncreasePrice_Lud.ToString());
        reference.Child("Users").Child(uid).Child("suchoData").Child("valCount").SetValueAsync(sm.valCount.ToString());
        reference.Child("Users").Child(uid).Child("suchoData").Child("ludCount").SetValueAsync(sm.ludCount.ToString());
        //reference.Child("Users").Child(uid).Child("suchoData").Child("suchoPositions_Vals").SetValueAsync(sm.suchoPositions_Vals.ToString());
        //reference.Child("Users").Child(uid).Child("suchoData").Child("suchoPositions_Luds").SetValueAsync(sm.suchoPositions_Luds.ToString());
        SavePositions_Sucho();

      //  Debug.Log("마리모식?"+ cdc.marimoIsSick.ToString() + "저장됨? 응 됨");
       // Debug.Log("현재 로그인된 계정은 " + user.Email);
    }

    void SavePositions_Sucho()
    {
        if (sm.ludCount > 0)
        {
            for (int i = 0; i < sm.suchoPositions_Luds.Count; ++i)
            {
                sucho_luds_x.Add(sm.suchoPositions_Luds[i].x.ToString());
                sucho_luds_y.Add(sm.suchoPositions_Luds[i].y.ToString());
            }
            combi_lud_Xs = string.Join(",", sucho_luds_x);
            combi_lud_Ys = string.Join(",", sucho_luds_y);

            reference.Child("Users").Child(uid).Child("suchoData").Child("combi_lud_Xs").SetValueAsync(combi_lud_Xs);
            reference.Child("Users").Child(uid).Child("suchoData").Child("combi_lud_Ys").SetValueAsync(combi_lud_Ys);
        }
        else {; }
        if(sm.valCount >0)
        {
            for (int i = 0; i < sm.suchoPositions_Vals.Count; ++i)
            {
                sucho_vals_x.Add(sm.suchoPositions_Vals[i].x.ToString());
                sucho_vals_y.Add(sm.suchoPositions_Vals[i].y.ToString());
            }
            combi_val_Xs = string.Join(",", sucho_vals_x);
            combi_val_Ys = string.Join(",", sucho_vals_y);

            reference.Child("Users").Child(uid).Child("suchoData").Child("combi_val_Xs").SetValueAsync(combi_val_Xs);
            reference.Child("Users").Child(uid).Child("suchoData").Child("combi_val_Ys").SetValueAsync(combi_val_Ys);

        }
        else {; }

    }

    void SaveNoOfShrimps()
    {
        //-- 일반
        reference.Child("Users").Child(uid).Child("allofCreature_Count").SetValueAsync(gm.creatureCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("all_sh_Count").SetValueAsync(gm.shrimpsCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("CHSCount").SetValueAsync(gm.CHSCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("ASCount").SetValueAsync(gm.ASCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("NLSCount").SetValueAsync(gm.noLSCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("ParentSCount").SetValueAsync(gm.parentSCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("SaskSCount").SetValueAsync(gm.saskScount.ToString());

        reference.Child("Users").Child(uid).Child("noOfcreature").Child("normal_dps_Count").SetValueAsync(gm.dpsCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("SDPCount").SetValueAsync(gm.SDPCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfcreature").Child("bettaCount").SetValueAsync(gm.bettaCount.ToString());

        //---고스트류
        reference.Child("Users").Child(uid).Child("noOfghost").Child("allofGhost").SetValueAsync(gm.ghost_CreatureCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfghost").Child("ghost_nrl_sh").SetValueAsync(gm.ghost_ShrimpsCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfghost").Child("ghost_horse_sh").SetValueAsync(gm.horse_ShrimpsCount.ToString());
        reference.Child("Users").Child(uid).Child("noOfghost").Child("ghost_noRDP").SetValueAsync(gm.noRe_DPsCount.ToString());
     
    }
   /* void FakeSave(int _testNo)
    {
        string testid = "Test" + _testNo.ToString();
        reference.Child("Users").Child(testid).Child("memberNum").SetValueAsync(testid.ToString());
        reference.Child("Users").Child(testid).Child("allofCreature_Count").SetValueAsync(testCounts[_testNo].ToString());
        reference.Child("Users").Child(testid).Child("tankName").SetValueAsync(testhangNames[_testNo].ToString());
    }
   */

    //---------------------------- 로드 관련 ------------------------------------------

    void LoadNoOfShrimps(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;

        }
        //-- 일반
        int.TryParse(e.Snapshot.Child(uid).Child("allofCreature_Count").GetValue(true).ToString(), out gm.creatureCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("all_sh_Count").GetValue(true).ToString(), out gm.shrimpsCount); 
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("CHSCount").GetValue(true).ToString(), out gm.CHSCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("ASCount").GetValue(true).ToString(), out gm.ASCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("NLSCount").GetValue(true).ToString(), out gm.noLSCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("ParentSCount").GetValue(true).ToString(), out gm.parentSCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("SaskSCount").GetValue(true).ToString(), out gm.saskScount);

        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("normal_dps_Count").GetValue(true).ToString(), out gm.dpsCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("SDPCount").GetValue(true).ToString(), out gm.SDPCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfcreature").Child("bettaCount").GetValue(true).ToString(), out gm.bettaCount);

        //--고스트류
        int.TryParse(e.Snapshot.Child(uid).Child("noOfghost").Child("allofGhost").GetValue(true).ToString(), out gm.ghost_CreatureCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfghost").Child("ghost_nrl_sh").GetValue(true).ToString(), out gm.ghost_ShrimpsCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfghost").Child("ghost_horse_sh").GetValue(true).ToString(), out gm.horse_ShrimpsCount);
        int.TryParse(e.Snapshot.Child(uid).Child("noOfghost").Child("ghost_noRDP").GetValue(true).ToString(), out gm.noRe_DPsCount);

        gm.FillCreature();
        gm.UpdateAll();
        gm.CreateFloor();
       // Debug.Log("생물 수 로드");
    }
    void LoadSucho(object sender, ValueChangedEventArgs e)//---수초 데이터 로드밑 합치기--.
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        long.TryParse(e.Snapshot.Child(uid).Child("suchoData").Child("sucho_autoMoneyIncreaseAmount").GetValue(true).ToString(), out SuchoWork.sucho_autoMoneyIncreaseAmount);
        long.TryParse(e.Snapshot.Child(uid).Child("suchoData").Child("sucho_autoIncreasePrice_Vall").GetValue(true).ToString(), out SuchoWork.sucho_autoIncreasePrice_Vall);
        long.TryParse(e.Snapshot.Child(uid).Child("suchoData").Child("sucho_autoIncreasePrice_Lud").GetValue(true).ToString(), out SuchoWork.sucho_autoIncreasePrice_Lud);

        int.TryParse(e.Snapshot.Child(uid).Child("suchoData").Child("valCount").GetValue(true).ToString(), out sm.valCount);
        int.TryParse(e.Snapshot.Child(uid).Child("suchoData").Child("ludCount").GetValue(true).ToString(), out sm.ludCount);

        if (sm.valCount != 0)
        {
            combi_val_Xs = e.Snapshot.Child(uid).Child("suchoData").Child("combi_val_Xs").GetValue(true).ToString(); 
            combi_val_Ys = e.Snapshot.Child(uid).Child("suchoData").Child("combi_val_Ys").GetValue(true).ToString();

            string[] val_Xs = combi_val_Xs.Split(new char[] { ',' });
            string[] val_Ys = combi_val_Ys.Split(new char[] { ',' });


            for (int i = 0; i< val_Xs.Length; ++i)
            {
                Vector3 loadVal = new Vector3(float.Parse(val_Xs[i]), float.Parse(val_Ys[i]), 0.0f);
                sm.suchoPositions_Vals.Add(loadVal);
            }

        }
        else {; }

        if (sm.ludCount != 0)
        {
            combi_lud_Xs = e.Snapshot.Child(uid).Child("suchoData").Child("combi_lud_Xs").GetValue(true).ToString();
            combi_lud_Ys = e.Snapshot.Child(uid).Child("suchoData").Child("combi_lud_Ys").GetValue(true).ToString();

            string[] lud_Xs = combi_lud_Xs.Split(new char[] { ',' });
            string[] lud_Ys = combi_lud_Ys.Split(new char[] { ',' });

            for (int i = 0; i < lud_Xs.Length; ++i)
            {
                Vector3 loadLud = new Vector3(float.Parse(lud_Xs[i]), float.Parse(lud_Ys[i]), 0.0f);
                sm.suchoPositions_Luds.Add(loadLud);
            }

        }
        else {; }

        sm.FillSucho();
        sm.UpdateSuchoAll();

        //Debug.Log("수초 로드 완료");

    }

    public void FBLoadData()
    {
        getFirebaseUser();
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += LoadSucho;
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += LoadNoOfShrimps;
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += Script_ValueChanged;
    }
    private void Script_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if(e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;

        }
       // showLoadedText.text = e.Snapshot.Child(uid).Child("Email").GetValue(true).ToString();

        uid = e.Snapshot.Child(uid).Child("memberNum").GetValue(true).ToString(); //--이건 안가져와도 되나?!

        cdc.marimoIsSick = e.Snapshot.Child(uid).Child("marimoSick").GetValue(true).ToString() == "True";

        int.TryParse(e.Snapshot.Child(uid).Child("currentFloor").GetValue(true).ToString(), out gm.currentFloor);

        long.TryParse(e.Snapshot.Child(uid).Child("money").GetValue(true).ToString(), out gm.money);
        long.TryParse(e.Snapshot.Child(uid).Child("moneyIncreaseAmount").GetValue(true).ToString(), out gm.moneyIncreaseAmount);
        long.TryParse(e.Snapshot.Child(uid).Child("moneyIncreaseLevel").GetValue(true).ToString(), out gm.moneyIncreaseLevel);
        long.TryParse(e.Snapshot.Child(uid).Child("moneyIncreasePrice").GetValue(true).ToString(), out gm.moneyIncreasePrice);

        //---화났뽁
        int.TryParse(e.Snapshot.Child(uid).Child("eattenallPoint").GetValue(true).ToString(), out gm.eattenallPoint);
        long.TryParse(e.Snapshot.Child(uid).Child("eattenCreatureCount").GetValue(true).ToString(), out gm.eattenCreatureCount);

        //---만화 화체크
        stageProgress = e.Snapshot.Child(uid).Child("stageProgress").GetValue(true).ToString();


        //대가리 터지는 만화 오픈
        for(int i = 0; i<= int.Parse(stageProgress); ++i)
        {
            cm.doneCartoon[i] = true;
        }
        for(int i = int.Parse(stageProgress)+1; cm.doneCartoon.Length  - i >= 0; ++i)
        {
            cm.doneCartoon[i] = false;

        }


        /*
        if (stageProgress == "11")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = cm.done7wha = cm.done8wha = cm.done9wha = cm.done10wha = cm.done11wha = true;
        }
        else if (stageProgress == "10")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = cm.done7wha = cm.done8wha = cm.done9wha = cm.done10wha = true;
            cm.done11wha = false;
        } else if (stageProgress == "9")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = cm.done7wha = cm.done8wha = cm.done9wha = true;
            cm.done11wha = cm.done10wha = false;

        } else if (stageProgress == "8")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = cm.done7wha = cm.done8wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = false;
        } else if (stageProgress =="7") {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = cm.done7wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha =cm.done8wha = false;
        }else if (stageProgress =="6")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = cm.done6wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = false;
        }
        else if (stageProgress == "5")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = cm.done5wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = cm.done6wha = false;
        }
        else if (stageProgress == "4")
        {
            cm.done2wha = cm.done3wha = cm.done4wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = cm.done6wha = cm.done5wha = false;
        }
        else if (stageProgress == "3")
        {
            cm.done2wha = cm.done3wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = cm.done6wha = cm.done5wha = cm.done4wha = false;
        }
        else if (stageProgress == "2")
        {
            cm.done2wha = true;
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = cm.done6wha = cm.done5wha = cm.done4wha = cm.done3wha = false;
        }
        else if (stageProgress == "1")
        {
            cm.done11wha = cm.done10wha = cm.done9wha = cm.done8wha = cm.done7wha = cm.done6wha = cm.done5wha = cm.done4wha = cm.done3wha =cm.done2wha= false;
        }
        else {; }*/

        gm.hangNameSaveTxt = e.Snapshot.Child(uid).Child("tankName").GetValue(true).ToString();
        hangname.text = gm.hangNameSaveTxt;

        //--- 카드 체크----
        //---일반
        gm.normalShrimp = e.Snapshot.Child(uid).Child("creatureCard").Child("normalShrimp").GetValue(true).ToString() == "True";
        gm.normalDP = e.Snapshot.Child(uid).Child("creatureCard").Child("normalDP").GetValue(true).ToString() == "True";
        gm.angryDP = e.Snapshot.Child(uid).Child("creatureCard").Child("angryDP").GetValue(true).ToString() == "True";
        gm.CHS = e.Snapshot.Child(uid).Child("creatureCard").Child("CHS").GetValue(true).ToString() == "True";
        gm.AS = e.Snapshot.Child(uid).Child("creatureCard").Child("AS").GetValue(true).ToString() == "True";
        gm.SDP = e.Snapshot.Child(uid).Child("creatureCard").Child("SDP").GetValue(true).ToString() == "True";
        gm.EggShrimp = e.Snapshot.Child(uid).Child("creatureCard").Child("EggShrimp").GetValue(true).ToString() == "True";
        gm.betta = e.Snapshot.Child(uid).Child("creatureCard").Child("betta").GetValue(true).ToString() == "True";

        //--고스트
        gm.ghost_normalShrimp = e.Snapshot.Child(uid).Child("creatureCard").Child("ghost_normalShrimp").GetValue(true).ToString() == "True";
        gm.ghost_horseShrimp = e.Snapshot.Child(uid).Child("creatureCard").Child("ghost_horseShrimp").GetValue(true).ToString() == "True";
        gm.ghost_noRe_DP = e.Snapshot.Child(uid).Child("creatureCard").Child("ghost_noRe_DP").GetValue(true).ToString() == "True";

        //--   마리모킬관
        gm.isForgived =cdc.isForgived = e.Snapshot.Child(uid).Child("isForgived").GetValue(true).ToString() == "True";
        gm.isKilled = cdc.isKilled = e.Snapshot.Child(uid).Child("isKilled").GetValue(true).ToString() == "True";


        //--gm외 다른 파일로
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("sh_autoMoneyIncreaseAmount").GetValue(true).ToString(), out ShrimpsWork.sh_autoMoneyIncreaseAmount);
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("sh_autoIncreasePrice").GetValue(true).ToString(), out ShrimpsWork.sh_autoIncreasePrice);
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("dp_autoMoneyIncreaseAmount").GetValue(true).ToString(), out DPWork.dp_autoMoneyIncreaseAmount);
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("dp_autoIncreasePrice").GetValue(true).ToString(), out DPWork.dp_autoIncreasePrice);
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("betta_autoMoneyIncreaseAmount").GetValue(true).ToString(), out BettaWork.betta_autoMoneyIncreaseAmount);
        long.TryParse(e.Snapshot.Child(uid).Child("otherPrice").Child("betta_autoIncreasePrice").GetValue(true).ToString(), out BettaWork.betta_autoIncreasePrice);


        //Debug.Log(gm.isForgived);
        int.TryParse(e.Snapshot.Child(uid).Child("killCount").GetValue(true).ToString(), out gm.killCount);
        Debug.Log("그밖의 것 로드");
    }
}
