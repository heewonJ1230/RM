using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpsWork : MonoBehaviour //새우 자동일
{
    [SerializeField] private GameObject shrimp;
    //--패트롤 ai도 여기에.
    private float waitTime;
    public float waitTimeMin, waitTimeMax;
    public bool shrimpTurn;
    public bool isShrimpWithEggs;
    public bool isGhost;
    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float ranSpeedMin;
    public float ranSpeedMax;
    public int YeoBun;
    private float minY;
    private float maxY;
    //----애니메이션 교차 작동
    private Animator anim;

    public static long sh_autoMoneyIncreaseAmount = 1;
    public static long sh_autoIncreasePrice = 101;

    //--ㅇㅐㅇ그리뽁
    bool islastone;
    void Start()
    {
        islastone = false;
        maxY = -0.1f;
        anim = GetComponent<Animator>();

        waitTime = Random.Range(waitTimeMin, waitTimeMax); 
        StartCoroutine(Work());
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        MoveAnim();
        if (isShrimpWithEggs)
        {
            Invoke("ShrimpsWithEggs", 31.0f);
            Invoke("DeathofShrimpwithEggs",31.0f);
        }
    }
    void DeathofShrimpwithEggs()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Instantiate(gm.prefabShrimp, transform.position, Quaternion.identity); 
        Destroy(this.gameObject);
    }
    void Update()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        minY = (gm.fixedY - gm.currentFloor*gm.spaceFloor) + YeoBun;
        MoveAnim();
        Move();
        if (gm.shrimpsCount == 1 && gm.angryDPCount >= 1)
        {
            if (!islastone)
            {
                islastone = true;
                /*                if (!gm.isKilled)
                                {
                                    if (null != GameObject.FindWithTag("AngryPuffer"))
                                    {
                                        AngryDPEttnAllSrimp();
                                    }
                                }*/
            }

        }

        if (islastone)
        {
            AngryDPEttnAllSrimp();
        }

        if (!isGhost && gm.angryDPCount>0 && gm.shrimpsCount <= 0)
        {
            gm.shrimpsCount = 0;
            Destroy(this.gameObject, 0.3f);
        }
    }
    //---------위에 시스템 구분
    void AngryDPEttnAllSrimp()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
       
        sh_autoIncreasePrice = 101 - gm.eattenallPoint;

        if(gm.shrimpsCount == 1)
        {
            gm.shrimpsCount = 0;
            gm.CHSCount = 0;
            gm.ASCount = 0;
            gm.creatureCount -= 1;


            Destroy(this.gameObject, 0.3f);
        }

    }

   
    void ShrimpsWithEggs()
    {
        int ranEggCount = Random.Range(5, 20);
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 1; i < ranEggCount; ++i)
        {
            float angle = (360.0f / ranEggCount);
            float newX = (Mathf.Sin(i * angle * Mathf.Deg2Rad + (360.0f / ranEggCount))) * 1.2f;
            float newY = (Mathf.Cos(i * angle * Mathf.Deg2Rad + (360.0f / ranEggCount))) * 1.2f;
            newX = newX + transform.position.x;
            newY = newY + transform.position.y;
            
            GameObject egg = Instantiate(gm.prefabShrimp, new Vector2(newX, newY), Quaternion.identity);
        }
        gm.shrimpsCount += ranEggCount;
        gm.creatureCount += ranEggCount;
       
    }

    //--마우스로 내려칠때
    void OnMouseDown()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

        if (shrimp.activeSelf)
        {
            anim.SetBool("MoveCheck", true);
            MoveSpot();
            fXController_ep1.InvokeWaterDrops_Start();
        }
    }
    //--새우 움직임에 관한 것 --.
    int movespotCount;
    float moveY;
    float newMinY;
    float newMaxY;

    void MoveSpot()
    {
        movespotCount += 1;
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        MoveSpotAgain();
        moveSpots.position = new Vector2(Random.Range(minX, maxX), moveY);
        if (movespotCount == gm.currentFloor)
        {
            movespotCount = 1;
        }
    }
    void MoveSpotAgain()
    {
        if(moveSpots.position.y - 9 >= minY) 
        {
            newMinY = moveSpots.position.y - 9;
        }
        else { newMinY = minY; }
        if(moveSpots.position.y + 9 <= maxY)
        {
            newMaxY = moveSpots.position.y + 9;
        }
        else { newMaxY = maxY; }
        moveY = Random.Range(newMinY,newMaxY);
    }
    void Move()
    {
        //방향 구하기
        Vector3 dir = moveSpots.position - transform.position;
        float shrimpsSpeed = Random.Range(ranSpeedMin, ranSpeedMax);
  
        //타겟 방향으로 다가감
        transform.position += dir * shrimpsSpeed * Time.deltaTime; //이거는 거리가 멀면 가속함
        //타겟 방향으로 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (moveSpots.transform.position.x < transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = - Mathf.Abs(scale.x); 
            transform.localScale = scale;
 
        }
        else if(moveSpots.transform.position.x >= transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = - Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
 
    void MoveAnim()
    {
        if (Vector2.Distance(transform.position, moveSpots.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                MoveSpot(); 
                waitTime = Random.Range(waitTimeMin, waitTimeMax);
                anim.SetBool("MoveCheck", true); 
            }
            else
            {
                waitTime -= Time.deltaTime;
                anim.SetBool("MoveCheck", false);
            }
        }
    }   
    IEnumerator Work()
    {
        while (true)
        {
            CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (!cdc.marimoIsSick && !gm.isKilled)
            {
                if (isGhost)
                {
                    gm.money += 13 * sh_autoMoneyIncreaseAmount;
                }
                else
                {
                    gm.money += sh_autoMoneyIncreaseAmount;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
