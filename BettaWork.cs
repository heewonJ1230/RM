using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettaWork : MonoBehaviour //ㅂ자동 일 
{
    public int eatCount;
    public float eatRate;
    public float ranEatRateMin, ranEatRateMax;
    public bool isGhost;
    public bool isAngry;

    [SerializeField] private GameObject betta;
    
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
    
    public static long betta_autoMoneyIncreaseAmount = 19;
    public static long betta_autoIncreasePrice = 11311; //가격 필히  11311
    GameManager gm;


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
   //앵그리뽁 필요없음 삭제

    //--마우스로 내려칠때
    void OnMouseDown()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
       
        if (betta.activeSelf)
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
                    gm.money += 13 * betta_autoMoneyIncreaseAmount;
                }
                else
                {
                    gm.money += betta_autoMoneyIncreaseAmount;
                }
            }

            yield return new WaitForSeconds(1);
        }

    }

    // 새우 먹어보리자!!
    void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.tag == "Bettas")
        {
            if (Random.Range(1, 100) <= 30)
            {
                collision.gameObject.layer = 9;
                gm.bettaCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if((betta_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 11) > 0
                ) { betta_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 11; }
                else
                {
                    betta_autoMoneyIncreaseAmount = 19;
                }
                
                Destroy(collision.gameObject);
            }
            else
            {
                ;
            }
           
        }
        else if(Random.Range(1, 100) <= eatRate)
        {
            if (collision.tag == "Shrimps")
            {
                Destroy(collision.gameObject, 0.01f);
                gm.shrimpsCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if (ShrimpsWork.sh_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 1 > gm.moneyIncreaseLevel * gm.shrimpsCount * 211)
                { ShrimpsWork.sh_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 1;}

               
                CreateGhostShrimps();
               
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

                CreateGhostShrimps();
            }
            else if (collision.tag == "DPs")
            {
                Destroy(collision.gameObject, 0.01f);

                gm.dpsCount -= 1;
                gm.creatureCount -= 1;
                eatCount += 1;
                if (DPWork.dp_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 2 > gm.moneyIncreaseLevel * gm.dpsCount * 3331)
                { DPWork.dp_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 2; }

                CreateGhostDPs();
            }
            else if (collision.tag == "SupriseDP")
            {
                Destroy(collision.gameObject, 0.01f);

                gm.dpsCount -= 1;
                gm.creatureCount -= 1;
                gm.SDPCount -= 1;
                eatCount += 1;
                if (DPWork.dp_autoMoneyIncreaseAmount - gm.moneyIncreaseLevel * 2 > gm.moneyIncreaseLevel * gm.dpsCount * 3331)
                { DPWork.dp_autoMoneyIncreaseAmount -= gm.moneyIncreaseLevel * 2; }

               
                CreateGhostDPs();
            }
            else
            {
                ; //Debug.Log("유령들아님?대체 유령왜없어지누?"); 
            }
        }
        else {
            ;//Debug.Log("유령들아님?대체 유령왜없어지누?") ;
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

