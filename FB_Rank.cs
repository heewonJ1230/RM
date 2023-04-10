using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FB_Rank 
{
    public string no;
    public string rankUID; 
    public string rank_hangName;
    public string rank_cretureCount;
    public CreatureType rank_creatureType; //ㅇㅣ넘값도 바로 못쓰네 만들고 선언하고 써야하는구나
    public enum CreatureType
    {
        Shrimp,
        DP,
        Betta,
        Ghost
    }

    public FB_Rank(string _no,string _rankUID, string _rank_hangName, string _rank_cretureCount/*, CreatureType _rank_creatureType*/)
    {
        no = _no;
        rankUID = _rankUID;
        rank_hangName = _rank_hangName;
        rank_cretureCount = _rank_cretureCount;
       // rank_creatureType = _rank_creatureType;
    }
}
