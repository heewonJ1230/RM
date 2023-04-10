using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CartoonReplay 
{
    [Header("유저에게 안보이는 정보")]
    public int hwaNum;
    public int pageCount;

    [Header("유저에게 보이는 정보")]
    public string ct_title;
    //public string thumbnailPath;
    public bool isOpen_ct; 
    public Text titleText;
    public GameObject lockedCover_ct;
    public Image thumbnailImg;
    public Image thumbnailImg_locked;
    public Button thatBttn;
}
public class CartoonReplayData : MonoBehaviour
{
    [Header("넘기기 데이터")]
    public bool isClicked;
    public int clickedhwa;
    public int clicked_pageCount;
    public string clicked_title;

    // public Transform momtr; --수동으로 해야햄
    // public GameObject prefab_cartoon;
    public GameObject crBttn_go;
    public GameObject cr_LockedTxt;
    public CartoonReplay[] crds;
    public int curhawId;
    CartoonManager cm;
    FB_ImageLoader fb_il;

    private void Awake()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        fb_il= gameObject.GetComponent<FB_ImageLoader>();

        FillCTSlot();
        CheckCartoonPrg();
    }

    private void Start()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        CheckCartoonPrg();
        
        for (int i = 0; i < crds.Length; ++i)
        {
            if (crds[i].isOpen_ct)
            {
                crds[i].thatBttn.interactable = true;
                Debug.Log("클릭가능?  " + i + "   ");

            }
            else
            {
                crds[i].thatBttn.interactable = false;
                Debug.Log("클릭 불가?  " + i + "   ");

            }
        }
        if (cm.doneCartoon[7] && !cm.doneCartoon[9])
        {
            crBttn_go.SetActive(false);
            cr_LockedTxt.SetActive(true);
        }

    }
    private void Update()
    {
        //CheckCartoonPrg();
    }

    public void OnClick_Ct(int _clickedhwaID)
    {
        if (!isClicked)
        {
            isClicked = true;
            curhawId = _clickedhwaID;
            clickedhwa = crds[curhawId].hwaNum;
            clicked_pageCount = crds[curhawId].pageCount;
            clicked_title = crds[curhawId].ct_title;

            fb_il.load_title.text = clicked_title;
          
            Debug.Log("제대로 눌렸니? " + curhawId);
            fb_il.ShrimpGo(clickedhwa, clicked_pageCount);
        }
    }
    void CheckCartoonPrg()
    {
        if (null != GameObject.Find("GameManager"))
        {
            for (int i = 0; i < cm.doneCartoon.Length; ++i)
            {
                // Debug.Log(i + " 오픈첵을 위하여 ");
                if (cm.doneCartoon[i])
                {
                    crds[i].isOpen_ct = true;
                    crds[crds.Length - (i+1)].lockedCover_ct.SetActive(false);
                    crds[i].thatBttn.interactable = true;
  //                  Debug.Log("클릭가능?  " + i + "   ");
                }
                else
                {
                    // Debug.Log(i + "오픈 안된걸로 체크됨");
                    crds[i].isOpen_ct = false;
                    crds[crds.Length - (i+1)].lockedCover_ct.SetActive(true);
                    crds[i].thatBttn.interactable = false;
//                    Debug.Log("클릭 불가?  " + i + "   ");
                }

            }
        } //이거 추가함 안열려서; 소용없었지만 
        else if(null !=GameObject.Find("Panel_Webtoon"))
        {
            CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();

            fb_il.ShrimpGo(cdc.hwaNum, cdc.pageCount);
        }
    }
    
    public void FillCTSlot()
    {
        for (int i = crds.Length - 1; i >= 0; --i)
        {

            crds[crds.Length - (i+1)].titleText.text = crds[i].ct_title;
          /*  if (crds[crds.Length - (i + 1)].isOpen_ct)
            {
                Debug.Log(i+"  오픈이 아무것도 안되네?!");
                crds[i].lockedCover_ct.SetActive(false);
            }
            else
            {
                Debug.Log(i + "   모두닫힘이네?!");
                crds[i].lockedCover_ct.SetActive(true);
            }*/
            string thumbnailPath = "Cartoon/Thumbnail/" + i + "Thumbnail";
            SetThumbnailImage(Resources.Load<Sprite>(thumbnailPath));
           
            //Debug.Log(curhawId);

        }
    }
    /* 
      public CartoonReplayData(int _hawId, int _pageCount, string _ct_title, string _thumbnailPath)
    {
        //이렇게 하면 되나? 이게 아닌거 같어
        crds[_hawId].hwaId = _hawId;
        crds[_hawId].pageCount = _pageCount;
        crds[_hawId].ct_title = _ct_title;
        crds[_hawId].thumbnailPath = _thumbnailPath;

        curhawId = _hawId;
        Set_Ct_Title(_ct_title);
        SetThumbnailImage(Resources.Load<Sprite>(_thumbnailPath));
    }
    */

    //어후 시발 잠만
    //배열 어레이 리스트로 하는게 맞지 
    

    void SetThumbnailImage(Sprite sprite)
    {
        if (curhawId < crds.Length)
        {
            crds[curhawId].thumbnailImg.sprite
           = crds[curhawId].thumbnailImg_locked.sprite
           = sprite;
            ++curhawId;
        }
       
    }
    void InvokeCartoon()
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
       // Save();
        Debug.Log("이거 저장 안대나?");
        LoadingManager.LoadScene("2Cartoon");
    }

    //데이터 로드 하는애가 넘겨주면 되겠다. 몰라 ㅆ발 해보고 아니면 말아 괜차나
    //그거 본 내용 저장은 어디해? - 이미 저장되어 있자너.

}
