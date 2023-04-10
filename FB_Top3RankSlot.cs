using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB_Top3RankSlot : MonoBehaviour //=--- 이파일은 인벤토리 슬롯이라구함
{
    public Image num_icon;


    public Text top3_hangname;
    public Text top3_creatureCount;
    public GameObject top3_memeCheck;
    
    //-----케이디 보고 만드는 중 이


    public void Top3AddRank(FB_Rank fB_Rank)
    {
        top3_hangname.text = fB_Rank.rank_hangName;
        top3_creatureCount.text = string.Format("{0:0,0}",fB_Rank.rank_cretureCount)
         + " 마리";
    }

}