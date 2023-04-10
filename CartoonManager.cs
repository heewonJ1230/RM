using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonManager : MonoBehaviour
{
    //public static int curIndex;
    public static int curIndex;//<- 이거 초기화 안되게 스테틱해야하나봄

    public bool[] doneCartoon;
    public int lastCartoon;
    public int[] cartoonPageCount;
    PageTurning pt;
    private void Awake()
    {
        //PassPageCount();
    }

    private void Start()
    {
       //PassPageCount();

        doneCartoon[0] = true;
       //Debug.Log("지금" + curIndex);
        if (null != GameObject.Find("Panel_Cartoon"))
        {
            if (curIndex == 9)
            {
                ;
            }
            else
            {
                curIndex += 1;
            }
        }
       if (null != GameObject.Find("MarimoInMK"))
        {
            //don10wha = true;
            doneCartoon[9] = true;
            Debug.Log("지금10화봤다고 없뎃?");
        }
        lastCartoon = curIndex;
        for(int i=0; i<=lastCartoon; i++)
        {
            doneCartoon[i] = true;
        }
    }

    void PassPageCount()
    {
        /*  if(null!= GameObject.Find("Panel_Webtoon"))
          {
              pt = GameObject.Find("Panel_Webtoon").GetComponent<PageTurning>();

              if (curIndex != 0)
              {
                  pt.lookinghwa = curIndex;
                  pt.lookinghwaPageCount = cartoonPageCount[curIndex];

                  Debug.Log("카툰메니저에서 읽은 pt 페이지 현재 화인덱스 " + pt.curIndex +" 화"+ pt.curPageIndex + " 쪽");
                  //위에꺼 안됨.
              }
              Debug.Log("카툰메니저에서 읽은 pt 페이지 현재 화인덱스 " + pt.curIndex + " 화" + pt.curPageIndex + " 쪽");
              //이거 안되고 페이지터닝에서 다함
          }*/
    }
}
