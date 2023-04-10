using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB_RankSlot : MonoBehaviour //=--- 이파일은 인벤토리 슬롯이라구함
{
    //public Image num_icon;
    // public Text top3_hangname;
    public Text no;
    public Text hangname;

    //public Text top3_creatureCount;
    public Text creatureCount;

    public string ranking_uid;

    //public GameObject top3_memeCheck;
   public GameObject memeCheck;

    //-----케이디 보고 만드는 중 이
    private void Start()
    {
        RemoveRank();
    }

    public void AddRank(FB_Rank _fB_Rank)
    {
        ranking_uid = _fB_Rank.rankUID;
        hangname.text = _fB_Rank.rank_hangName;
        creatureCount.text = string.Format("{0:0,0}",_fB_Rank.rank_cretureCount);
    }
    public void RemoveRank()
    {
        hangname.text = "가장 지루한 중학교는??";
        creatureCount.text = "...로딩중...";
    }
}