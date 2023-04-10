using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CartoonHwaSlot : MonoBehaviour
{/*
   // public int hwaNum;
  //  public int pageCount;
   // public string thumbnailPath;
    public bool isClicked;
    public int bttnId;
    [Header("전달용")]
    public int hwaNum;
    public int pageCount;
    public string ct_title;
  
    //public Text titleText;
    //public Image thumbnail;

    public Button thisBttn;

    private int thisbttnID;
    CommonDataController cdc;
    CartoonParser ct_parser;
    CartoonReplayData crd;

    private void Awake()
    {
        cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        ct_parser = GameObject.Find("CartoonNHcho").GetComponent<CartoonParser>();
        crd = GameObject.Find("CartoonReplayData").GetComponent<CartoonReplayData>();
        thisBttn = gameObject.GetComponent<Button>();
        thisbttnID = bttnId;
        isClicked = false;
    }
   private void Start()
    {
        //개별 버튼에서는 내가 아는 걸로 안되나봄
        
        for(int i = 0; i<=crd.crds.Length; ++i)
        {
            if (i == bttnId && crd.crds[i].isOpen_ct)
            {
                thisBttn.interactable = true;
                Debug.Log("클릭가능?  " + bttnId + "   ");

            }
            else
            {
                thisBttn.interactable = false;
                Debug.Log("클릭 불가?  " + bttnId + "   ");

            }
        }
       
        //--알수없구먼 오늘은 그만할뤠.
    }

    private void Update()
    {
        if (isClicked)
        {
            thisBttn.interactable = false; //--다른거 중복 클릭 안되게... 맞나이게?...
        }
    }*/
   
}
