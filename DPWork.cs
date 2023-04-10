using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPWork : MonoBehaviour //인뽁 자동 일 
{
    public int eatCount;
    public float eatRate;
    public float ranEatRateMin, ranEatRateMax;
    public bool isGhost;
    public bool isAngry;

    [SerializeField] private GameObject dwarfPuffer;
    
    private float waitTime;
    public float waitTimeMin, waitTimeMax;

    //public Transform mother_empty;
    public Transform moveSpots;
    public float minX;
    public float maxX;
    public float ranSpeedMin;
    public float ranSpeedMax;
    public int YeoBun;
    private float minY;
    private float maxY;
    
    public static long dp_autoMoneyIncreaseAmount = 3;
    public static long dp_autoIncreasePrice = 977; //가격 필히 밸런싱977
    GameManager gm;

    bool isAdEattenall = false;

    //----애니메이션 교차 작동
    private Animator anim;

 
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        maxY = 0.3f;
        anim = GetComponent<Animator>();
        Move();
        waitTime = Random.Range(waitTimeMin, waitTimeMax);
        StartCoroutine(Work());
        MoveAnim();
        eatRate = Random.Range(ranEatRateMin, ranEatRateMax);

    }

    void Update()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        Move();
        minY = (gm.fixedY - gm.currentFloor * gm.spaceFloor) + YeoBun;
        MoveAnim();
        if (gm.angryDPCount >= 1 && (gm.dpsCount - gm.SDPCount - gm.angryDPCount) <= 0)
        {
            if (!isAdEattenall)
            {
                isAdEattenall = true;
                Invoke("AngryDPEattenAll", 31.0f);//아래꺼때문에 추가 후에 삭제해야해
               /* if (!gm.isKilled)
                {
                    Invoke("AngryDPEattenAll", 31.0f);

                }*/
            }
        }
        else
        {
            ;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (null != GameObject.FindGameObjectWithTag("AngryPuffer"))
        {

            GameObject[] adp = GameObject.FindGameObjectsWithTag("AngryPuffer");
            if (adp.Length >= 1)
            {
                for (int i = 0; i < adp.Length; i++)
                {
                    Destroy(adp[i]);
                }
            }
            gm.angryDPCount = 0;
        }
    }
    void AngryDPEattenAll()
    {
        dp_autoIncreasePrice = 977 - gm.eattenallPoint;
        ShrimpsWork.sh_autoIncreasePrice -= gm.eattenallPoint;

        if (null!= GameObject.FindGameObjectWithTag("DPs"))
        {
            GameObject[] dps = GameObject.FindGameObjectsWithTag("DPs");
            if (dps.Length >= 1)
            {
                for (int i = 0; i < dps.Length; i++)
                {
                    Destroy(dps[i]);
                }
            }
        }
        if(null!= GameObject.FindGameObjectWithTag("SupriseDP"))
        {
            GameObject[] sdps = GameObject.FindGameObjectsWithTag("SupriseDP");
            if (sdps.Length >= 1)
            {
                for (int i = 0; i < sdps.Length; i++)
                {
                    Destroy(sdps[i]);
                }
            }
        }
        if(null != GameObject.FindGameObjectWithTag("AngryPuffer"))
        {
            gm.creatureCount = 0;
            gm.dpsCount = 0;
            gm.SDPCount = 0;
            gm.angryDPCount = 0;
            gm.AngryDiscount();
            ++gm.eattenallPoint;

            GameObject[] adp = GameObject.FindGameObjectsWithTag("AngryPuffer");
            if (adp.Length >= 1)
            {
                for (int i = 0; i < adp.Length; i++)
                {
                    Destroy(adp[i]);
                } 
            }
           // Debug.Log("앵그리복어 안지워지는거??");
        }
     
    }
    //--마우스로 내려칠때
    void OnMouseDown()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
       
        if (dwarfPuffer.activeSelf)
        {
            anim.SetBool("MoveCheck", true);
            fXController_ep1.InvokeWaterDrops_Start();
            MoveSpot();
        }
    }

    void MoveSpot()
    {
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    void Move()
    {
        //방향 구하기
        Vector3 dir = moveSpots.position - transform.position;
        float dpSpeed = Random.Range(ranSpeedMin, ranSpeedMax);

        //타겟 방향으로 다가감
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, dpSpeed * Time.deltaTime);
        //mother_empty.position = transform.position;
        //타겟 방향으로 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //위아래 뒤집기하씨... 되긴하는데.. 뭔가 찝찝
        if (moveSpots.transform.position.x < transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = - Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (moveSpots.transform.position.x >= transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    void ResetFish() //각도는 평평하게 돌아왔음 좋겠는데 반전된건 두고 싶어
    {
        transform.rotation = Quaternion.identity;
    }
    void MoveAnim()
    {
        ResetFish();
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
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
            if (!cdc.marimoIsSick && !gm.isKilled)
            {
                if (isGhost)
                {
                    gm.money += 13 * dp_autoMoneyIncreaseAmount;
                }
                else
                {
                    gm.money += dp_autoMoneyIncreaseAmount;
                }
            }

            yield return new WaitForSeconds(1);
        }

    }

    // 새우 먹어보리자!!
    void OnTriggerEnter2D(Collider2D collision)
     {
        if(Random.Range(1, 100) <= eatRate)
        {
            if (collision.tag == "Shrimps")
            {
                Destroy(collision.gameObject, 0.01f);
                gm.shrimpsCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if (ShrimpsWork.sh_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 1 > gm.moneyIncreaseLevel * gm.shrimpsCount * 211)
                { ShrimpsWork.sh_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 1;}

                if (eatCount == 5 && gm.angryDPCount < 3)
                {
                    gm.CreateAngryPuffers();
                    eatCount -= 5;
                    gm.creatureCount += 1;
                    gm.angryDPCount += 1;
                    gm.dpsCount += 1;
                    gm.angryDP = true;
                }
                else if(eatCount == 5 && gm.angryDPCount >=3)
                {
                    eatCount -= 5;
                }
                CreateGhostShrimps();
                if (isAngry)
                {
                    ++gm.eattenCreatureCount;
                }
            }
            else if (collision.tag == "CHS")
            {
                Destroy(collision.gameObject, 0.01f);
                gm.shrimpsCount -= 1;
                gm.CHSCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if (ShrimpsWork.sh_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 1 > gm.moneyIncreaseLevel * gm.shrimpsCount * 211)
                { ShrimpsWork.sh_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 1; }

                if (eatCount == 5 && gm.angryDPCount < 3)
                {
                    gm.CreateAngryPuffers();
                    eatCount -= 5;
                    gm.creatureCount += 1;
                    gm.angryDPCount += 1;
                    gm.dpsCount += 1;
                    gm.angryDP = true;
                }
                else if (eatCount == 5 && gm.angryDPCount >= 3)
                {
                    eatCount -= 5;
                }
                if (isAngry)
                {
                    ++gm.eattenCreatureCount;
                }
                CreateGhostShrimps();
            }
            else if (collision.tag == "AstronautShrimp")
            {
                Destroy(collision.gameObject, 0.01f);
                gm.shrimpsCount -= 1;
                gm.ASCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if (ShrimpsWork.sh_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 1 > gm.moneyIncreaseLevel * gm.shrimpsCount * 211)
                { ShrimpsWork.sh_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 1; }

                if (eatCount == 5 && gm.angryDPCount < 3)
                {
                    gm.CreateAngryPuffers();
                    eatCount -= 5;
                    gm.creatureCount += 1;
                    gm.angryDPCount += 1;
                    gm.dpsCount += 1;
                    gm.angryDP = true;
                }
                else if (eatCount == 5 && gm.angryDPCount >= 3)
                {
                    eatCount -= 5;
                }
                if (isAngry)
                {
                    ++gm.eattenCreatureCount;
                }
                CreateGhostShrimps();
            }

            if (gm.shrimpsCount <= gm.dpsCount / 5)
            {
                if (collision.tag == "DPs")
                {
                    Destroy(collision.gameObject, 0.01f);

                    gm.dpsCount -= 1;
                    gm.creatureCount -= 1;
                    eatCount += 1;
                    if (dp_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 2 > gm.moneyIncreaseLevel * gm.dpsCount * 3331)
                    { dp_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 2; }

                    if (eatCount == 5 && gm.angryDPCount < 3)
                    {
                        gm.CreateAngryPuffers();
                        eatCount -= 5;
                        gm.creatureCount += 1;
                        gm.angryDPCount += 1;
                        gm.dpsCount += 1;
                        gm.angryDP = true;
                    }
                    else if (eatCount == 5 && gm.angryDPCount >= 3)
                    {
                        eatCount -= 5;
                    }
                    if (isAngry)
                    {
                        ++gm.eattenCreatureCount;
                    }
                    CreateGhostDPs();
                }
                if (collision.tag == "SupriseDP")
                {
                    Destroy(collision.gameObject, 0.01f);

                    gm.dpsCount -= 1;
                    gm.creatureCount -= 1;
                    gm.SDPCount -= 1;
                    eatCount += 1;
                    if (dp_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 2 > gm.moneyIncreaseLevel * gm.dpsCount * 3331)
                    { dp_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 2; }

                    if (eatCount == 5 && gm.angryDPCount < 3)
                    {
                        gm.CreateAngryPuffers();
                        eatCount -= 5;
                        gm.creatureCount += 1;
                        gm.angryDPCount += 1;
                        gm.dpsCount += 1;
                        gm.angryDP = true;
                    }
                    else if (eatCount == 5 && gm.angryDPCount >= 3)
                    {
                        eatCount -= 5;
                    }
                    if (isAngry)
                    {
                        ++gm.eattenCreatureCount;
                    }
                    CreateGhostDPs();
                }
            }
        }
        else
        {
            //Debug.Log("ㅂㅜ딪혔지만 살려주지");
        }

    }

    void CreateGhostShrimps()//유령새우 만들어!!
    {
        int randomGhost = Random.Range(1, 100);
        if (randomGhost <= 5 && gm.moneyIncreaseLevel >= 17)
        {
            SellGCreature sgc = GameObject.Find("SellManager").GetComponent<SellGCreature>();

            int ranspGhost = Random.Range(1, 100);
            if (ranspGhost <= 80)
            {
                gm.CreateG_Shrimps();
                gm.ghost_CreatureCount += 1;
                gm.ghost_ShrimpsCount += 1;
                gm.ghost_normalShrimp = true;
                Debug.Log("유령이새우탄생!!"+ gm.ghost_ShrimpsCount);
            }
            else if(ranspGhost > 80)
            {
                gm.CreateH_Shrimps();
                gm.ghost_CreatureCount += 1;
                gm.horse_ShrimpsCount += 1;
                gm.ghost_horseShrimp = true;
                Debug.Log("말새우탄생!"+ gm.horse_ShrimpsCount);
            }
            sgc.UpdatePanelCell_Gshrimps();
            sgc.UpdatePanelCell_Hshrimps();
            
        }
    }

    void CreateGhostDPs()//유령 인뽁들!
    {
        int randonGhost = Random.Range(1, 100);
        if(randonGhost <= 5 && gm.moneyIncreaseLevel >= 17)
        {
            SellGCreature sgc = GameObject.Find("SellManager").GetComponent<SellGCreature>();

            gm.CreateNoRe_DPs();
            gm.ghost_CreatureCount += 1;
            gm.noRe_DPsCount += 1;
            gm.ghost_noRe_DP = true;
            Debug.Log("오는 중인복 탄생!" + gm.noRe_DPsCount);
            sgc.UpdatePanelCell_NRDP();
        }
    }
}

