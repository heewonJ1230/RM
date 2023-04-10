using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MKmanager : MonoBehaviour//애니메이션&대사& 손으로 힛하는 거 일부 들어가 있음
{
    [Header("카운트 용")]
    public int cutCount;
    public int failCutCount;
    public int sdgCount;
    public int clickFBCount;
    public bool isKilled;
    public bool isForgived;

    [Header("때리는거 만들기")]
    public GameObject hitPrefab;
    public float hitLifeTime;
    
    [Header("칼질 충돌 체크용")]
    public bool isBeCutted;
    public bool isDamaged;

    [Header("마리모 idle 움직이는 시간&거리")]
    public float mMoveTime;
    public float mMoveDistance;

    [Header("ㄱㅣ본이미지 바꿈- 2개")]
    public Sprite[] idleImgs;

    [Header("띄꺼운거")]
    public Sprite[] meanImgs;

    [Header("마리모 싸대기 맞았을뜬")]
    public float mSDGMoveTime;
    public float mSDGMoveDistance;
    [Header("맞아서 아픈거")]
    public Sprite[] hurtImgs;

    [Header("죽은거 ")]
    public Sprite[] dieImgs;

    public int ranMirror;
    
    Vector2 mPosition;
    public Vector2 mPositionOrigin;
    Vector3 mScale;
    GameObject hitFinger;
    Vector3 hitPosition;
    public SpriteRenderer mSprite;


    SaveData saveData;
    KillUISystem kus;
    CutSystem cs;
    IMKSpeakBbbMaker imksbb;
    IMK_FXController imkFx;
    [SerializeField] private GameObject thisMarimo;

    [Header("마리모 피 4단계에서 가만히 있으면 비는걸루 ")]
    public float noInputTimer; //마리모 피 4/5깍였을때 가만히 있으면 빌음 ..
    public float maxNoInputTime;

    void Start()
    {
        isKilled = false;
        isForgived = false;
        kus = GameObject.Find("KillUISystem").GetComponent<KillUISystem>();
        cs = GameObject.Find("CutSystem").GetComponent<CutSystem>();
        imksbb = GameObject.Find("IMKSpeakBbbMaker").GetComponent<IMKSpeakBbbMaker>();
        imkFx = GameObject.Find("IMK_FXController").GetComponent<IMK_FXController>();
        // hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mPositionOrigin;
        mPosition = transform.position;
        mScale = transform.localScale;
        mSprite = transform.GetComponent<SpriteRenderer>();
        //kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리게

        InvokeRepeating("IdleMove", 2 * mMoveTime, 2 * mMoveTime);
        InvokeRepeating("IdleSpriteChanger", 4 * mMoveTime, 4 * mMoveTime);
        //InvokeRepeating("RanImgDir", 2 * mMoveTime, 2 * mMoveTime);
        //InvokeRepeating("IdleSpriteChanger", 2 * mMoveTime, 2 * mMoveTime);
        //isBeCutted = true;

    }

    void Update()
    {
       // Debug.Log("상태확인" + kus.forgiveBttnb.interactable);
        NoInput();
        HitToCut();
        //Debug.Log(mTimeCount);
        if (kus.hpFill.fillAmount <= 0)
        {
            MDeath();
        }

    }

    //---아이들 애님 --.
    void IdleMove()
    {
        IdlemoveRight();
        Invoke("IdleMoveLeft", mMoveTime);
        Invoke("IdleMoveReset", 2 * mMoveTime);
    }
    void IdlemoveRight()
    {
        mPosition.y = mPositionOrigin.y;
        mPosition.x += mMoveDistance * Time.deltaTime;
        transform.position = mPosition;
        if (kus.hpFill.fillAmount < 1 && kus.hpFill.fillAmount >= 4 / 5f)
        {
            if (sdgCount <= 7)
            {
                RanImgDir();
                //InvokeRepeating("RanImgDir", 2 * mMoveTime, 2 * mMoveTime);
            }
            else
            {
                RanImgDir();
                //Invoke("IdleSpriteChanger", 3 * mMoveTime);
                IdleSpriteChanger();
            }
        }
        else
        {
            RanImgDir();
            IdleSpriteChanger();
        }
            
        //Sprite randomIdle = idleImgs[Random.Range(0, idleImgs.Length)];
        //transform.GetComponent<SpriteRenderer>().sprite = randomIdle;
    }
    void IdleMoveLeft()
    {
        mPosition.y = mPositionOrigin.y;
        mPosition.x -= mMoveDistance * Time.deltaTime;
        transform.position = mPosition;
        if (kus.hpFill.fillAmount < 1 && kus.hpFill.fillAmount >= 4 / 5f)
        {
            if (sdgCount <= 7)
            {
                RanImgDir();
                //CancelInvoke("RanIngDir");
                Invoke("IdleSpriteChanger", mMoveTime);
            }
            else
            {
                RanImgDir();
                Invoke("IdleSpriteChanger", mMoveTime);
            }
        }
        else
        {
            RanImgDir();
            IdleSpriteChanger();
        }
            
    }
    //--- 움직인 애니메이션 리셋--
    public void IdleMoveReset()
    {
        transform.localScale = mScale;
        transform.position = mPositionOrigin;
    }
    //--Idle Sprite정하기.
    void IdleSpriteChanger()
    {
        if (kus.hpFill.fillAmount <= 1 && kus.hpFill.fillAmount > 4 / 5f)//체력 1단계
        {
            Sprite randomIdle = idleImgs[Random.Range(0, idleImgs.Length)];
            mSprite.sprite = randomIdle;
        }
        else if (kus.hpFill.fillAmount <= 4 / 5f && kus.hpFill.fillAmount > 3 / 5f)//체력 2단계
        {
            mSprite.sprite = hurtImgs[0];
        }
        else if (kus.hpFill.fillAmount <= 3 / 5f && kus.hpFill.fillAmount > 2 / 5f)//체력 3단계
        {
            mSprite.sprite = hurtImgs[2];
        }
        else if (kus.hpFill.fillAmount <= 2 / 5f && kus.hpFill.fillAmount > 1 / 5f)//체력 4단계
        {
            mSprite.sprite = hurtImgs[4];
        }
        else if (kus.hpFill.fillAmount <= 1 / 5f)//체력 5단
        {
            mSprite.sprite = hurtImgs[Random.Range(6,7)];
        }
    }

    //--이미지 방향 랜덤 정하기
    void RanImgDir()
    {
        ranMirror = Random.Range(0,2);
        if (ranMirror <= 0)
            mScale.x = 0.5f;
        else
            mScale.x = - 0.5f;
        transform.localScale = mScale;
    }

    //때리기!!
    //--- 따귀 때렸을ㄸ ㅐ애님//

    void SSaDaeGiMove_Up()
    {
        RanImgDir();
        mPosition.x = mPositionOrigin.x;
        mPosition.y -= mSDGMoveDistance * Time.deltaTime;
        transform.position = mPosition;
    }
    void SSaDaegiMove_Down()
    {
        RanImgDir();
        mPosition.x = mPositionOrigin.x;
        mPosition.y += mSDGMoveDistance * Time.deltaTime;
        transform.position = mPosition;
    }
    void OnSsaDaeGiAnimMarimo()
    {
        if (thisMarimo.activeSelf)
        {
            CancelInvoke("IdleMove");
            SDGSprite();
            SSaDaeGiMove_Up();
            Invoke("SSaDaegiMove_Down", mSDGMoveTime);
            Invoke("IdleMoveReset", 2 * mSDGMoveTime);
            InvokeRepeating("IdleMove", 5 * mSDGMoveTime, 2 * mMoveTime);
        }
    }
    
    //---따귀 때렸을 때 Sprite
    void SDGSprite()
    {
        if (kus.hpFill.fillAmount <= 1 && kus.hpFill.fillAmount > 4 / 5f)//체력 1단계
        {
            if(sdgCount <= 7)
            {
                int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[0].speakingTexts_IMK.Length);
                mSprite.sprite = meanImgs[Random.Range(0,meanImgs.Length)];
                imksbb.SpeakTxt(0, maxTxt);
                imksbb.CreateBubbleIMK();
            }
            else
            {
                mSprite.sprite = hurtImgs[1];
                int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[2].speakingTexts_IMK.Length);
                imksbb.SpeakTxt(2, maxTxt);
                imksbb.CreateBubbleIMK();
            }
        }
        else if (kus.hpFill.fillAmount <= 4 / 5f && kus.hpFill.fillAmount > 3 / 5f)//체력 2단계
        {
            mSprite.sprite = hurtImgs[1];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[4].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(4, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount < 3 / 5f && kus.hpFill.fillAmount >= 2 / 5f)//체력 3단계
        {
            mSprite.sprite = hurtImgs[3];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[5].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(5, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount < 2 / 5f && kus.hpFill.fillAmount > 1 / 5f)//체력 4단계
        {
            mSprite.sprite = hurtImgs[5];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[6].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(6, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount <= 1 / 5f)//체력 5단
        {
            mSprite.sprite = hurtImgs[Random.Range(6,7)];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[8].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(8, maxTxt);
            imksbb.CreateBubbleIMK();
        }
    }
    //----온--클릭
    private void OnMouseDown()
    {
        if (!isDamaged)
        {
            kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리게

            imkFx.HitFX();
            CancelInvoke("RanIngDir");
            CancelInvoke("IdleMove");
            CancelInvoke("IdleSpriteChanger");
            Invoke("SpawnHitFinger", 0.15f);
            //  Debug.Log("온마우스다운이거슨 이거되네 ; 허p");
            OnSsaDaeGiAnimMarimo();
            if (kus.hpFill.fillAmount <= 1 && kus.hpFill.fillAmount > 4 / 5f)//체력 1단계
            {
                Invoke("OffDamaged", 2 * mSDGMoveTime);
                Invoke("InColorAfterDamaged", 1.8f * mSDGMoveTime);

            }
            else
            {
                Invoke("OffDamaged", 3 * mSDGMoveTime);
                Invoke("InColorAfterDamaged", 2.8f * mSDGMoveTime);

            }
            kus.HitHp();
            ++sdgCount;
        }
    }
    void SpawnHitFinger()
    {
        if (GameObject.Find("CutterPosition(Clone)") != null)
        {

        }
        else
        {
            //Debug.Log("손-한번씩 잘");
            OnDamaged();
            hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitPosition.z = 100.0f;
            hitFinger = Instantiate(hitPrefab, hitPosition, Quaternion.identity);
        
            Destroy(hitFinger, hitLifeTime);
        }
    }

    //--- 칼질.
    //------데미지 오분의 일 들갔을때 움직임.

    void Damage20Move_Up()
    {
        RanImgDir();
        mPosition.x = mPositionOrigin.x;
        mPosition.y -= 2 * mSDGMoveDistance * Time.deltaTime;
        transform.position = mPosition;

    }
    void Damage20Move_Down()
    {
        RanImgDir();
        mPosition.x = mPositionOrigin.x;
        mPosition.y += 2 * mSDGMoveDistance * Time.deltaTime;
        transform.position = mPosition;
    }

    public void Damage20_Move()
    {
        if (kus.hpFill.fillAmount <= 1 / 5f)
        {
            CancelInvoke("IdleMove");
            Invoke("IdleSpriteChanger", 0.5f);
        }
        else
        {
            CancelInvoke("IdleMove");
            CancelInvoke("IdleSpriteChanger");
            CutSprite();
            Damage20Move_Up();
            Invoke("Damage20Move_Down", 0.5f *mSDGMoveTime);
            Invoke("IdleMoveReset", 1 * mSDGMoveTime);
            Invoke("Damage20Move_Up", 1.5f * mSDGMoveTime);
            Invoke("Damage20Move_Down", 2 * mSDGMoveTime);
            InvokeRepeating("IdleMove", 5 * mSDGMoveTime, 2 * mMoveTime);
        }
    }
    //--칼질할때 Sprite.
    void CutSprite()
    {
        kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리게

        if (kus.hpFill.fillAmount <= 1 && kus.hpFill.fillAmount > 4 / 5f)//체력 1단계
        {
            mSprite.sprite = hurtImgs[1];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[3].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(3, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount <= 4 / 5f && kus.hpFill.fillAmount > 3 / 5f)//체력 2단계
        {
            mSprite.sprite = hurtImgs[3];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[4].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(4, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount <= 3 / 5f && kus.hpFill.fillAmount > 2 / 5f)//체력 3단계
        {
            mSprite.sprite = hurtImgs[5];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[5].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(5, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount <= 2 / 5f && kus.hpFill.fillAmount > 1 / 5f)//체력 4단계
        {
            mSprite.sprite = hurtImgs[6];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[6].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(6, maxTxt);
            imksbb.CreateBubbleIMK();
        }
        else if (kus.hpFill.fillAmount <= 1 / 5f)//체력 5단
        {
            mSprite.sprite = dieImgs[Random.Range(0,1)];
            int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[8].speakingTexts_IMK.Length);
            imksbb.SpeakTxt(8, maxTxt);
            imksbb.CreateBubbleIMK();
            CancelInvoke("IdleMove");
        }
    }
    //---제대로 칼질 맞췄나?!

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cutter")
        {
            if (!isBeCutted)
            {
                CutSprite();
                imkFx.CutFX();
                kus.CutHp();
                cutCount++;
                Damage20_Move();
                isBeCutted = true;
                OnDamaged();
                Invoke("OffDamaged", 4.2f * mSDGMoveTime);
                Invoke("InColorAfterDamaged", 4.0f * mSDGMoveTime);
                //Debug.Log("칼- 한번ㅈ씩 잘 됨");
                //Debug.Log("들어감"); //--ㅇㅣㄸㅏㄴㄱㅓ ㅇㅓㅂㅅㅇㅐㄹㄱㅓㅅ
            }
        }
        else if(collision.tag =="Safezone")
        {
            isBeCutted = false;
            //Debug.Log("나깜?");
            failCutCount++;
            cs.MissShowup(); Debug.Log("이잉3");
            Debug.Log(collision.tag);
        }
    }

    //----무적상태
    void OnDamaged()//맞으면 무적상태로 변함
    {
        gameObject.layer = 9;
        kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리게
        if (kus.hpFill.fillAmount <= 0f)
        {
            ;
        }
        else
        {
            //스프라이트 렌더러 이미지 투명하게
            mSprite.color = new Color(1, 1, 1, 0.4f);
        }
        //다마지드 체크용: 온클릭이 작동해버려서 추가함..
        isDamaged = true;
    }

    void OffDamaged()//무적상태 해제
    {
        gameObject.layer = 8;
        //mSprite.color = new Color(1, 1, 1, 1);
        isDamaged = false;
    }
    void InColorAfterDamaged()
    {
        mSprite.color = new Color(1, 1, 1, 1);
    }

    //--때리기 20번시 칼로 한번이됨.
    void HitToCut()
    {
        if (sdgCount == 20)
        {
            ++cutCount;
            sdgCount = 0;
        }
    }

    //----죽음 상태
    void MDeath()
    {
        CancelInvoke("IdleMove");
        CancelInvoke("RanImgDir");
        mSprite.sprite = dieImgs[Random.Range(0,1)];//다시 확인 필ㄴ
        gameObject.layer = 9;
        isKilled = true;
        isDamaged = true;

    }

    //--반응 없을때 체력 4단계 에서 잘못했다고 빔 //그 또처음 시작할때 노반응이면
    void NoInput()
    {
        if (kus.hpFill.fillAmount > 4/5f && sdgCount < 7 )//풀피로 가만히 있을때 도발
        {
            noInputTimer += Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
            {
                noInputTimer = 0;
            }
            if (noInputTimer >= 2 * maxNoInputTime)
            {
                CancelInvoke("IdleSpriteChanger");
                Sprite meanIdle = meanImgs[Random.Range(0, meanImgs.Length)];
                mSprite.sprite = meanIdle;
                noInputTimer = 0;
                kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리kus.forgiveBttnb.interactable = false;//말할동안 포기브 안눌리게
                
                int rna = Random.Range(4, 6);
                imksbb.SpeakTxt(0, rna);
                imksbb.CreateBubbleIMK();
                //InvokeRepeating("IdleSpriteChanger", 4 * mMoveTime, 4 * mMoveTime);
            }
        }
        else if (kus.hpFill.fillAmount <= 2 / 5f && kus.hpFill.fillAmount >= 1 / 5f)//몇대 맞았을때 빌어
        {
            noInputTimer += Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
            {
                noInputTimer = 0;
            }

            if(noInputTimer >= maxNoInputTime)
            {
                noInputTimer = 0;
                kus.forgiveBttnb.interactable = false;
                int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[7].speakingTexts_IMK.Length);
                imksbb.SpeakTxt(7, maxTxt);
                imksbb.CreateBubbleIMK();
            }
        }
        else if (kus.hpFill.fillAmount <= 1 / 5f && kus.hpFill.fillAmount > 0.5f)
        {
            noInputTimer += Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
            {
                noInputTimer = 0;
            }

            if (noInputTimer >= maxNoInputTime)
            {
                noInputTimer = 0;
                kus.forgiveBttnb.interactable = false;
                int maxTxt = Random.RandomRange(0, imksbb.iMKspeak.iMKSpeakTags[8].speakingTexts_IMK.Length);
                imksbb.SpeakTxt(8, maxTxt);
                imksbb.CreateBubbleIMK();
            }
        }
        else if (kus.hpFill.fillAmount < 0.1f)
        {
            noInputTimer = 1;
        }

    }
    //---포기브 눌렀을때반응
    public void ForgiveAnim()
    {
        ++clickFBCount;
        kus.forgiveBttnb.interactable = false;
        Debug.Log("포기브 눌린겨");
        CancelInvoke("IdleSpriteChanger");
        
        if (kus.hpFill.fillAmount <= 1 && kus.hpFill.fillAmount > 4 / 5f)
        {
            mSprite.sprite = meanImgs[Random.Range(0,meanImgs.Length)];//다시 확인필요
            if (clickFBCount == 1)
            {
                imksbb.SpeakTxt(9, 0);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 2)
            {
                imksbb.SpeakTxt(9, 1);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 3)
            {
                imksbb.SpeakTxt(9, 2);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 4)
            {
                mSprite.sprite = idleImgs[3];
                imksbb.SpeakTxt(9, 3);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount >= 5)
            {
                mSprite.sprite = idleImgs[2];
                imksbb.SpeakTxt(12, 0);
                imksbb.CreateBubbleIMK();
                LastClickFB();
            }
        }
        else if(kus.hpFill.fillAmount <= 4/5f && kus.hpFill.fillAmount > 3 / 5f)
        {
            mSprite.sprite = meanImgs[Random.Range(0, meanImgs.Length)];
            if (clickFBCount == 1)
            {
                imksbb.SpeakTxt(10, 0);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 2)
            {
                imksbb.SpeakTxt(10, 1);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 3)
            {
                imksbb.SpeakTxt(10, 2);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount >= 4)
            {
                imksbb.SpeakTxt(12, 0);
                imksbb.CreateBubbleIMK();
                LastClickFB();
            }
        }
        else if (kus.hpFill.fillAmount <= 3 / 5f && kus.hpFill.fillAmount > 2 / 5f)
        {
            if (clickFBCount == 1)
            {
                imksbb.SpeakTxt(11, 0);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount == 2)
            {
                imksbb.SpeakTxt(11, 1);
                imksbb.CreateBubbleIMK();
            }
            else if (clickFBCount >= 3)
            {
                imksbb.SpeakTxt(12, 0);
                imksbb.CreateBubbleIMK();
                LastClickFB();
            }

        }
        else if (kus.hpFill.fillAmount <= 2 / 5f )
        {
            imksbb.SpeakTxt(12, 0);
            imksbb.CreateBubbleIMK();
            LastClickFB();
        }
        Invoke("IdleSpriteChanger", 5 * mMoveTime);
    }
    void LastClickFB()
    {
        isForgived = true;
        kus.forgiveBttn.SetActive(false);
        //--ㅅㅔㅇㅣㅂㅡ ㅎㅐㅇㅑㅎㅐ --//
/*
        SaveData saveData = new SaveData();
        saveData.cutCount = cutCount;
        saveData.hpFill = kus.hpFill.fillAmount;
        saveData.clickFBCount = clickFBCount;

        saveData.isKilled = isKilled;
        saveData.isForgived = isForgived;
        if (isKilled)
        {
            ++saveData.killCount; 
        }

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);

        Debug.Log(saveData.isKilled);
*/
        Invoke("Go3ep", 3 * mMoveTime);

    }
    void Go3ep()
    {
        LoadingManager.LoadScene("3ep1");
        Debug.Log("왜안감");
    }
}


