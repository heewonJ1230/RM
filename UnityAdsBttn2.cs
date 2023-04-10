using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityAdsBttn2 : MonoBehaviour
{
    
#if UNITY_IOS
    public const string id = "3541731";
#endif


    public string timetrunBack = "timeTrunBack";
    public Button sandTimer;
    public Text timerText;

    GameManager gm;
    SuchoManager sm;
    TimeManager tm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sm = GameObject.Find("SuchoManager").GetComponent<SuchoManager>();

        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();

        sandTimer = GetComponent<Button>();
        if (sandTimer)
        {
            sandTimer.onClick.AddListener(timeShowRewardedVideo);
        }

       Advertisement.Initialize(id,false);

    }
  
    void Update()
    {
        if (sandTimer)
        {
            sandTimer.interactable = Advertisement.IsReady(timetrunBack);
        }
        else if(sandTimer.interactable==false)
        {
            timerText.text = "광고 로딩 중...";
        }
        if (sandTimer.interactable)
        {//여기다가 조건써야ㅔㅆ네 ; 헐퀴
            if (CommonDataController.killCount == 1)
            {
                timerText.text = "시간 돌리기";

            }
            else if(CommonDataController.killCount == 2)
            {
                if(gm.ghost_CreatureCount > 0)
                {
                    timerText.text = "시간 돌리기";
                }
                else
                {
                    timerText.text = "비용 부족";
                    sandTimer.interactable = false;
                }
            }
            else if (CommonDataController.killCount == 3)
            {
                if (gm.creatureCount > 0)
                {
                    timerText.text = "시간 돌리기";
                }
                else
                {
                    timerText.text = "비용 부족";
                    sandTimer.interactable = false;
                }
            }
            else if (CommonDataController.killCount == 4)
            {
                if (sm.valCount +sm.ludCount > 0)
                {
                    timerText.text = "시간 돌리기";
                }
                else
                {
                    timerText.text = "비용 부족";
                    sandTimer.interactable = false;
                }
            }

        }

    }
   
    public void timeShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = timeReward;

        Advertisement.Show(timetrunBack, options);
    }
    private void timeReward(ShowResult result)
    {
       if(result == ShowResult.Finished)
        {
            turnBackTime(); 
        }
       else if(result == ShowResult.Skipped)
        {
            ;
        }
        else if (result == ShowResult.Failed)
        {
            ;
        }
    }
    private void turnBackTime()
    {
       // Debug.Log("ㅅㅣ간돌리기?");이거 아니고
        tm.OnClickTurnBackMK();
    }
}
