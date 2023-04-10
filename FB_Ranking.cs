using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class FB_Ranking : MonoBehaviour //케이디가  인벤토리라고 하는것
{
    DatabaseReference reference;

    [Header("top3")]
    public string[] top3_usid = new string[3];
    public string[] top3_hangname = new string[3];
    public string[] top3_count = new string[3];
    public Text[] top1_Text;
    public Text[] top2_Text;
    public Text[] top3_Text;

    [Header("top100")]
    public GameObject rankSlot;
    FB_RankSlot[] slots; //--랭크 슬롯들.

    private string[] rkin;
    private string[] rkin_count;
    private Dictionary<string, int> rkin_Dic;
    private long ranking_int;

    private bool textLoadBool = false;

    [Header("케이디")]

    List<FB_Rank> userRankList; //--플레이어에게 보이는 랭크. (플레이어가 소지한 아이템 리스트)약간 개념이 다른거 같은데?. 
    List<FB_Rank> rankTapList; // 선택된 탭에 따라 다르게 보여질 링크 -- 그 아직 안만들긴했지만 참고겸하려고 남겨.. 음 탭은 일단 하지마.

    //public Text Description_Text; //-- 부연설명
    //public string[] tabDescription; //-- 탭 부연설명들! 
    public Transform tf; //-- slots의  부모객체 ;.
    public GameObject rank_go; //랭크창 켜고 끄기;.

    //public GameObject[] selectedTabImages; //--그 나중에 넣을거 탭 구별

    private long memeRank; //-- 선택된 아이템 / 나로 보이는 것을 순서로 체크할건데 이게 int면 안될듯.
    //private int selectedTab; //--역시 나중에 넣을 거임. -선택된 템.

    bool isRankActived; //--랭크 오픈시- 케이디는 Activated로함 그냥 .
    bool tabActived; //-- 탭 관련 빼자아이.탭 활성화시 트루.
    bool memeActived; //-- 내가 순위에 있으면 표시될거./아이템 활성화시 트루.
   
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
   
    // public List<FB_Rank> rankList;

   

    /*  public void ShowItem()
      {
          inventoryTabList.Clear(); 
          RemoveSlot();//--일단 슬롯 초기화.
          selectedItem = 0; //--맨 앞에꺼 선택되어있게.

          switch (welectedTab)
          {
              case 0;
                  for(int i =0; i<inventoryItemList.Count; i++)//종류가 같을때만 인벤토리에 넣어주는 걸로 표시하는거구나/ 
                  {
                      if(Item.ItemType.Use == inventoryItemList[i].itemType)
                          inventoryTabList.Add(inventoryItemList[i]);
                  }
                    break;
               case 1;
                  for(int i =0; i<inventoryItemList.Count; i++)
                  {
                      if(Item.ItemType.Equip == inventoryItemList[i].itemType)
                          inventoryTabList.Add(inventoryItemList[i]);
                  }
                    break;
          }
      }*/

    //-뭐 키관련은 다 생략.


    //---- 무슨 사이트 보고 만든것
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;//나중에 어웨이크로 가야할 --Ok 
    }

    void Start()
    {
        top3_usid = new string[3];
        top3_hangname = new string[3];
        top3_count = new string[3];

        userRankList = new List<FB_Rank>();
        rankTapList = new List<FB_Rank>();
        slots = tf.GetComponentsInChildren<FB_RankSlot>();
        DataLoad();
        Debug.Log("스타트가 실행되는거야?");
    }

    void Update()
    {
     
    }
    void LateUpdate()
    {
        if (textLoadBool)
        {
           // Debug.Log("이거 실행되긴함?" + "텍스트로드");

            TextLoad();
        }
    }


    //---------------------------------------------------------------------------------------------------------------------------------------

    void ShowRank()
    {
        Aroow_click ac = GameObject.Find("Image_tedoriNArrow").GetComponent< Aroow_click > ();
        ac.GoTop();
        RemoveSlot();
     //   Debug.LogError(userRankList.Count);
        for (int i = 0; i < 97; i++)
        {
            rankTapList.Add(userRankList[i]);
           // Debug.LogError("쇼랭크첫번째 포문  : "+ i);
            if (userRankList[i] == null)
                break;
        }

        for (int i = 0; i < rankTapList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddRank(rankTapList[i]);
            slots[i].no.text = (i + 4).ToString();
       
            //Debug.LogError("쇼랭크됨?"+"rankTapList  " + rankTapList.Count );
        }

        //--미미인거 빤짝이주ㄱ
    }
    public void RankBttn() //--ㅇㅕ기다가 다하자! 
    {
        if(rankTapList != null)
        {
            rankTapList.Clear();
        }
        DataLoad();

        rank_go.SetActive(true);

        tabActived = true;
        memeActived = false;
    }
    public void RemoveSlot()
    {//--지우기 처음에 열때 초기화 해줘야함 --근데 케이디는 그 탭 구별이니까 기본 탭이 없어서 이렇게 한거 같은데!..

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveRank();
            slots[i].gameObject.SetActive(false);//이게 아니라 만들어줘야/
        }
    }
    void DataLoad()
    {
        //데이터 로드 > 여기부터 잘 모르겠음 ;;; 다 모르는 거자나 .
        reference.Child("Users").GetValueAsync().ContinueWith(task =>
        {
           //Debug.Log("레퍼런스 차일드 유저스는" + reference.Child("Users").ToString());
            if (task.IsFaulted)
            {
                Debug.Log("데이터 로드 실패?");
                //에러 데이터로드 실패 시 다시 데이터 로드
               DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int count = 0;
                ranking_int = snapshot.ChildrenCount;
                Debug.Log("스냅샷 칠드런카운트" + snapshot.ChildrenCount);
                rkin = new string[ranking_int];
                rkin_count = new string[ranking_int];
                rkin_Dic = new Dictionary<string, int>();


                foreach (DataSnapshot data in snapshot.Children)
                {
                    //받은 데이터들을 하나씩 잘라 string 배열에 저장
                    IDictionary rankInfo = (IDictionary)data.Value;//--원
                 
                    //Debug.Log("스냅샷 칠드런카운트 포이치 에서  " + snapshot.ChildrenCount);

                    if (rankInfo["tankName"] == null|| rankInfo["allofCreature_Count"] == null)
                    {
                        count++;
                    }
                    else {
                        rkin_Dic.Add(rankInfo["tankName"].ToString() + "|" + rankInfo["memberNum"].ToString(), Int32.Parse(rankInfo["allofCreature_Count"].ToString()));

                        count++; //-- 요게 원래 퍼온거에서 있던거고 위에 if는 내가 넣은거 ; 요걸로 하면 하나하고 끗나버리고 이프문 지우면 안끗나던ㄷ
                    }

                }
            textLoadBool = true;
            }
        });
    }
    void TextLoad()
    {
        //Debug.Log("이거 실행되긴함?" + strRank);
        textLoadBool = false;
        try
        {
            ;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("눌리퍼런스 이 ");
            return;
        }
        var quieryDic = rkin_Dic.OrderByDescending(x => x.Value);
        int rank_dic_no = 0;
       foreach(var arraid_dic in quieryDic)
        {
            if (rank_dic_no <= 2)
            {
                string[] top3block = arraid_dic.Key.Split(new char[] { '|' });
                top3_hangname[rank_dic_no] = top3block[0];
                top3_count[rank_dic_no] = string.Format("{0:0,0}", arraid_dic.Value) + " 마리";
            }else if(rank_dic_no>=3 && rank_dic_no< 100)
            {
                string[] top100block = arraid_dic.Key.Split(new char[] { '|' });
                userRankList.Add(new FB_Rank(rank_dic_no.ToString(),top100block[1], top100block[0],string.Format("{0:0,0}", arraid_dic.Value) + " 마리"));

            }
            else
            {
                break;
            }
            top1_Text[0].text = top3_hangname[0];
            top1_Text[1].text = top3_count[0];
            top2_Text[0].text = top3_hangname[1];
            top2_Text[1].text = top3_count[1];
            top3_Text[0].text = top3_hangname[2];
            top3_Text[1].text = top3_count[2];


            //Debug.Log("이거가 되는 거 가테!" + rank_dic_no +"위" +"어항"+ arraid_dic.Key +"    "+ arraid_dic.Value+" 마리");

            rank_dic_no++;
        }
        ShowRank();

    }
}
 

