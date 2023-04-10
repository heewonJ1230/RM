using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityAdsBttn1 : MonoBehaviour
{
    
#if UNITY_IOS
    public const string id = "3541731";
#endif

    public string hchoplacementId = "Hcho_video";
    public Button hchoadButton;
    public Text hchoadBttnTxt;

    CartoonManager cm;
    void Start()
    {
        hchoadButton = GetComponent<Button>();
        if (hchoadButton)
        {
            hchoadButton.onClick.AddListener(hchoShowRewardedVideo);
        }

       Advertisement.Initialize(id,false);

        /*cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        if (cm.done10wha)
        {
            hchoCoolTimeZero();
        }*/ 
    }
  
    void Update()
    {
        if (hchoadButton)
        {
            hchoadButton.interactable = Advertisement.IsReady(hchoplacementId);
        }
        else if(hchoadButton.interactable==false)
        {
            hchoadBttnTxt.text = "광고 로딩 중...";
        }
        if (hchoadButton.interactable)
        {
            hchoadBttnTxt.text = "광고 보기";
        }

    }
   
    public void hchoShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = hchoHandleShowResult;

        Advertisement.Show(hchoplacementId, options);
    }
    private void hchoHandleShowResult(ShowResult result)
    {
       if(result == ShowResult.Finished)
        {
            hchoCoolTimeZero(); 
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
    private void hchoCoolTimeZero()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        cdc.marimoIsSick = false;
        cdc.loseActive = false;
        cdc.islose = false;
        cdc.isForgived = false; //--ㅇㅣ거지우면 마리모킬 끝나고 무한 병듬
    }
}
