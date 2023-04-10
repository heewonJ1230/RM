using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public DateTime quitTime;
    public long money;
    public long moneyIncreaseAmount;
    public long moneyIncreaseLevel;
    public long moneyIncreasePrice;

    //--앵그리뽁
    public int eattenallPoint;
    public long eattenCreatureCount;

    //--있었나 없었나 체ㅋ. 바보맥.

    public bool normalShrimp;
    public bool CHS;
    public bool AS;
    public bool EggShrimp;
    public bool nLS;
    public bool parentS;
    public bool saskS;

    public bool normalDP;
    public bool angryDP;
    public bool SDP;
    public bool betta;

    public bool ghost_normalShrimp;
    public bool ghost_horseShrimp;
    public bool ghost_noRe_DP;

    //--게임에 반영ㅋ.

    public int creatureCount;
    public int shrimpsCount;
    public int CHScount;
    public int ASCount;
    public int nLSCount;
    public int parentSCount;
    public int saskSCount;


    public int dpsCount;
    public int SDPCount;
    public int bettaCount;

    /*
    public int secretAngryDPCount;
    public int shtimpwithEggsCount;
    public int secretShrimps;
    public int secretDPS;
    public int secretCHScount;
    public int secretASCount;
    public int secretSDPCount;
    */

    public int ghost_CreatureCount;
    public int ghost_ShrimpsCount;
    public int horse_ShrimpsCount;
    public int noRe_DPsCount;

    public long autoMoneyIncreaseAmountShrimps;
    public long autoIncreasePriceShrimps;
    public long autoMoneyIncreaseAmountDPs;
    public long autoIncreasePriceDPs;
    public long autoMoneyIncreaseAmountBettas;
    public long autoIncreasePriceBettas;

    public long sucho_autoMoneyIncreaseAmount;
    public long sucho_autoIncreasePrice_Vall;
    public long sucho_autoIncreasePrice_Lud;
    public List<Vector3> suchoPositions_Vals;
    public List<Vector3> suchoPositions_Luds;
    public int valCount;
    public int ludCount;

    public int currentFloor;
    public string hangName;
    public int saiyanMrimoCoolTimeAd;
    public bool isAdCoolTime;

    public int curIndex;
    //public bool[] doneCartoon;
    public List<bool> doneCartoon;
    /*
    public bool done2wha;
    public bool done3wha;
    public bool done4wha;
    public bool done5wha;
    public bool done6wha;
    public bool done7wha;
    public bool done8wha;
    public bool done9wha;
    public bool done10wha;
    public bool done11wha;
    */
    public bool isLose;
    public bool loseActive;
    public bool isMarimoSick;
    public int hchoLoseCoolTime;

    public int sellGhostCount;
    public long sellP_Gshrimps;
    public int sold_GshrimpsCount;
    public long sellP_Hshrimps;
    public int sold_HshrimpsCount;
    public long sellP_NRDP;
    public int sold_NRDPCount;

    //---5KMarimo 마리모 킬 관련--
    public int cutCount;
    public float hpFill;
    public int clickFBCount;

    public int killCount; //과거로 돌아가기 횟수.
    public bool isKilled;
    public bool isForgived;

}