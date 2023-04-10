using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HchoPlayMAnager : MonoBehaviour
{
    public static long gottenMoney = 0;
    public Text gotmoneyTxt;
    public Transform moveSpots;
    public Button startHcho;
    public GameObject startbttn;
    public GameObject successPanel;
    public GameObject failPanel;
    private Animator anim;
    public Text timeouttxt;
    public GameObject safezoneGo;
    //private int score = 5;
    //rivate float scoreTime;
    private float nowTime;
    private float maxTime = 5.0f;
    private float makeTime = 2f;
    public float minY;
    public float maxY;
    public float maxtime;
    private float safezoneSpeed;
    private float waitTime = 0.3f;
    private float time;
    private bool isStarted = false;
    public bool successChechOnce = false;
    public bool loseChechOnce = false;
    //GameManager gm;
    public static bool isWin;
    bool isInSafezone = false;
    bool bPause;
    public Rigidbody mrd;

    private void Awake()
    {
        isStarted = false;
        isWin = false;
        //Debug.LogError("어웨이크작동 ");
        mrd = gameObject.GetComponent<Rigidbody>();
        mrd.useGravity = false;
    }
    void Start()
    {
        isStarted = false;
        Debug.Log("스타트 작동 ");
        //        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        isInSafezone = false;
        loseChechOnce = false;
        //timeouttxt.text = "ㅎ초 타임 : " + "한 번에 대박 물력 벌기";
        // time = 7.0f;
        // timeouttxt.text = "ㅎ초 타임 : " + string.Format("{0:N2}", time) + " 초";
        //  Debug.Log("머니 인크리스트어마운"+ gm.moneyIncreaseAmount);
        //  Debug.Log("머니 인크리스트어마운" + gm.money);
        //Time.timeScale = 0;
        //nowTime = maxTime;
        //scoreTime = Time.time + 2;
        anim = GetComponent<Animator>();
        anim.SetTrigger("marimoClick");
       // Move();
        loseChechOnce = false;
        successChechOnce = false;
    }
    

    public void ClickStart()
    {
        FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();
        fxc.WaterDrops_Start();
        isStarted = true;
        //ㅎ초 게임 초기화
        isWin = false;
        loseChechOnce = false;
        successChechOnce = false;

        time = maxtime;
        Time.timeScale = 1;
        safezoneSpeed = Random.Range(0.5f, 1);//0.5f, 4

        MoveSafezone();
    }

    void Update()
    {
       
        if (isStarted)
        {
            time -= Time.deltaTime;
            timeouttxt.text = "ㅎ초 타임 : " + string.Format("{0:N2}", time) + " 초";
            mrd.useGravity = true;
            Move();
            if (Time.timeScale > 0)
            {
                startHcho.interactable = false;
                startbttn.SetActive(false);
            }
            /*else if(Time.timeScale <= 0)
            {
                startbttn.SetActive(true);
                startHcho.interactable = true;
            }*/
            if (isInSafezone)
            {
                anim.SetTrigger("inSafezone");
            }
            if (time <= 0.05f)
            {
                Time.timeScale = 0.5f;
                if (time <= 0.01f)
                {
                    isStarted = false;
                    timeouttxt.text = "ㅎ초 타임 : 종료";
                    if (isInSafezone)
                    {
                        if (!successChechOnce)
                        {
                            // Debug.Log("성공!!");
                            successChechOnce = true;
                            anim.SetTrigger("inSafezone");
                            HchoSuccess();
                        }
                    }
                    else
                    {
                        if (!loseChechOnce)
                        {
                            loseChechOnce = true;
                            Debug.Log("실패!");
                            HchoLose();
                            gameObject.GetComponent<Animator>().Play("Sick");
                        }

                    }
                }

            }
            else
            {
                FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();

                if (Input.GetMouseButtonDown(0))
                {
                    fxc.Tab();
                    anim.SetTrigger("marimoClick");
                    mrd.velocity = new Vector3(0, 0, 0);
                    mrd.AddForce(0, 300, 0);
                }
                /* if (Time.time - scoreTime > 2)
                 {
                     scoreTime = Time.time;
                     score--;
                     //timeouttxt.text = score.ToString();
                 }*/
            }
        }
    }

   
    void HchoSuccess()
    {
        if (successChechOnce)
        {
            int rand = Random.Range(1, 100);
           // Debug.Log("썩쎄스");
            successPanel.SetActive(true);
            safezoneGo.SetActive(false);
            if (rand <= 10)
            {
                int rand2 = Random.Range(1, 100);
                if (rand2 <= 2)
                {
                    int randMoney = Random.Range(10000, 524287);
                   
                    gottenMoney = randMoney * CommonDataController.hchomoneyIncreaseAmount;
                    Debug.Log("초초대박  " + randMoney);
                }
                else if (rand2 > 2)
                {
                    int randMoney = Random.Range(997, 10000);
                    gottenMoney = randMoney * CommonDataController.hchomoneyIncreaseAmount;
                    //CommonDataController.hchomoney += gottenMoney;
                    Debug.Log("초대박  " + randMoney);
                }
                else
                {
                    Debug.Log("뭐이상함;");
                }
            }
            else if (rand > 10)
            {
                int randMoney = Random.Range(5, 101);
                gottenMoney = randMoney * CommonDataController.hchomoneyIncreaseAmount;
                //CommonDataController.hchomoney += gottenMoney;
                Debug.Log("대박  " + randMoney);
              
            }
            else
            {
                //Debug.Log("뭐이상함;");
            }
//            Debug.Log( "+" + gottenMoney);
            
            gotmoneyTxt.text = "+" + gottenMoney.ToString("###,###") + " 물력!!";
            CommonDataController.hchomoney += gottenMoney;
            Debug.Log("획득 금액 " + CommonDataController.hchomoney);
            FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();
            fxc.SavedSccssFX();
            CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
            isWin = true;
            Debug.Log("이겼니" + isWin);

           
        }
    }
    void HchoLose()
    {
        if (loseChechOnce)
        {
            FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();
            fxc.CloseSFX();
            failPanel.SetActive(true);
            safezoneGo.SetActive(false);
            isWin = false;
        }
    }
    private void MoveSafezone()
    {
        moveSpots.position = new Vector2(0, Random.Range(minY, maxY));
    }
    private void Move()
    {
        safezoneGo.transform.position = Vector2.MoveTowards(safezoneGo.transform.position, moveSpots.position, safezoneSpeed * Time.deltaTime);
        if(Vector2.Distance(safezoneGo.transform.position, moveSpots.position) < 0.1f)
        {
            if(waitTime <= 0)
            {
                waitTime = 0.3f;
                safezoneSpeed = Random.Range(0.5f, 3);
                //Debug.Log("스피드" + safezoneSpeed);
                MoveSafezone();   
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
           
        }
    }
    public void OnClickGo3Scene()
    {
        FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();
        fxc.CloseDirY();
        isWin = false;//이

        isStarted = false;
        Time.timeScale = 1;
        LoadingManager.LoadScene("3ep1");
    }
    public void OnClickLose()
    {
        FXController fxc = GameObject.Find("FXManager_Hcho").GetComponent<FXController>();
        fxc.CloseDirY();

        isStarted = false;
        Time.timeScale = 1;
        LoadingManager.LoadScene("3ep1");
     }
    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale += 0.3f;
        gameObject.GetComponent<Animator>().Play("Sick");
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Safezone")
        {
            isInSafezone = true;
            anim.SetTrigger("inSafezone");
            /* if(isInSafezone == false)
             {
                 isInSafezone = true;
                 Debug.Log("성공");
                 anim.SetBool("inSafezone", true);
             }
             else
             {
                 isInSafezone = false;
                 //gameObject.GetComponent<Animator>().Play("Marimo_Work");
                 anim.SetBool("inSafezone", false);
             }*/
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Safezone")
        {
            isInSafezone = false;
        }
    }
}
