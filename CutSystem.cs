using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSystem : MonoBehaviour
{
    [Header("컷만들기")]
    public GameObject cutPrefab;
    public float cutLifeTime;

    [Header("칼 그림")]
    public GameObject cutterPrefab;

    //    [Header("칼부림 초(최대 초 제한")]
    //public float swipeTime; 할수가 없네 이거 모르겠다.
  
    [Header("실패시 미스출력 아이ㅡ")]
    public GameObject miss;

    [Header("누르는 초")]
    public float touchTime=0;
    public float maxTouchTme;
    [Header("칼부림 최소 거ㄹ")]
    public float minCutDistance = 0;

    public bool isdragging= false;
    public bool iscutting = false; //이거 커터 한번만 소환하기위한 것.
    private Vector2 startCut;
    private Vector2 midSpawnCutter;
    private GameObject cutter; //껍데기가 따로 있어야함
    GameObject cutInstance;
    GameObject missInstance;

    MKmanager mkm;
    IMKSpeakBbbMaker imksbb;
    KillUISystem kiUI;


    private void Start()
    {
         mkm = GameObject.Find("marimoInMarimoKill").GetComponent<MKmanager>();
        imksbb = GameObject.Find("IMKSpeakBbbMaker").GetComponent<IMKSpeakBbbMaker>();
        kiUI = GameObject.Find("KillUISystem").GetComponent<KillUISystem>();
    }

    private void Update()
    {
        mkm = GameObject.Find("marimoInMarimoKill").GetComponent<MKmanager>();
        mkm.isBeCutted = false;
        if (Input.GetMouseButtonDown(0))
        {
            isdragging = true;
            startCut = Camera.main.ScreenToWorldPoint(Input.mousePosition);//1.스타트컷위치 선정하고.
        }
        else if(Input.GetMouseButtonUp(0) && isdragging)
        {
            SpawnCut();
            isdragging = false;
            Destroy(cutter);
        }
        else if (!isdragging)
        {
            iscutting = false;
        }
        else if (isdragging)
        {
            SpawnCutter();
            touchTime += Time.smoothDeltaTime;
            DrawCutter();
        }
    }

    private void SpawnCutter()
    {
        if (isdragging)
        {
            if (!iscutting)
            {
                midSpawnCutter = Camera.main.ScreenToWorldPoint(Input.mousePosition);//2. 미드컷위치를 찾고-그 커터칼 생길 위치 찾는거.

                if (Vector2.Distance(startCut, midSpawnCutter) >= 0.5f * minCutDistance)
                {
                    cutter = Instantiate(cutterPrefab, startCut, Quaternion.identity);
                   
                    iscutting = true; //한번만 커터칼생기게 해주고
                }
            }
        }
        TimeOver();
    }
    private void DrawCutter()
    {
        if (cutter != null)
        {
            Vector3 cutterPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cutterPosition.z = 100.0f;
            cutter.transform.position = cutterPosition;
            TimeOver();
        }
    }
    private void SpawnCut()
    {
        //스와이프 엔드가 어딘지 정의.
        Vector2 cutEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        if (Vector2.Distance(startCut,cutEnd) > minCutDistance)
        {
            //컷 렌더링
            cutInstance = Instantiate(cutPrefab, startCut, Quaternion.identity);
            cutInstance.GetComponent<LineRenderer>().SetPosition(0, startCut);
            cutInstance.GetComponent<LineRenderer>().SetPosition(1, cutEnd);

            //컬라이더 포인터를 적용
            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = Vector2.zero;
            colliderPoints[1] = cutEnd - startCut;
            cutInstance.GetComponent<EdgeCollider2D>().points = colliderPoints;

            //-만들어진 선 삭ㅈ
            Destroy(cutInstance, cutLifeTime);
        }
        else if(Vector2.Distance(startCut, cutEnd)> 0.5f * minCutDistance && Vector2.Distance(startCut, cutEnd) <= minCutDistance)
        {
            MissShowup();
            ++mkm.failCutCount;
        }
        TimeOver();
    }
    private void TimeOver()
    {
        if (touchTime >= maxTouchTme)
        {
            isdragging = false;
            touchTime = 0;
            Destroy(cutter);

            MissShowup();
            ++mkm.failCutCount;
        }
    }

    public void MissShowup()
    {
        if(null!= GameObject.Find("ForgiveBttn")
            )
        {
            IMK_FXController imkFx = GameObject.Find("IMK_FXController").GetComponent<IMK_FXController>();
            IMK_ForgiveBttn imkbttn = GameObject.Find("ForgiveBttn").GetComponent<IMK_ForgiveBttn>();
            Vector2 cutEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GameObject.Find("hitPoint(Clone)") != null)
            {

            }
            else
            {
                if (!imkbttn.isForgiveBttnClicked)
                {
                    imkFx.MissFX();
                    missInstance = Instantiate(miss, cutEnd, Quaternion.identity);
                    Destroy(missInstance, 2 * cutLifeTime);
                    if (kiUI.hpFill.fillAmount > 4 / 5f && kiUI.hpFill.fillAmount <= 1)
                    {
                        mkm.mSprite.sprite = mkm.meanImgs[0]; //확인 필수
                        int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[1].speakingTexts_IMK.Length);
                        imksbb.SpeakTxt(1, maxTxt);
                        imksbb.CreateBubbleIMK();
                        //Debug.Log(IMKSpeakBbbMaker.newSpeakTxt);
                    }
                }

            }
        }
        
    }

}
