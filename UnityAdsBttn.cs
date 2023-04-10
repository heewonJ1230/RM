using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityAdsBttn : MonoBehaviour
{
    
#if UNITY_IOS
    public const string id = "3541731";
#endif

    public string placementId = "rewardedVideo";
    public Button adButton;
    public Text adBttnTxt;
   
    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowRewardedVideo);
        }

       Advertisement.Initialize(id,false);

    }
  
    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Advertisement.IsReady(placementId);
        }
        else if(adButton.interactable==false)
        {
            adBttnTxt.text = "광고 로딩 중...";
        }
        if (adButton.interactable)
        {
            adBttnTxt.text = "광고 보기";
        }

    }
   
    public void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(placementId, options);
    }
    private void HandleShowResult(ShowResult result)
    {
       if(result == ShowResult.Finished)
        {
            CoolTimeZero(); 
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
    private void CoolTimeZero()
    {
        SaiyanMarimo sm = GameObject.Find("Icon_SaiyanMC").GetComponent<SaiyanMarimo>();

        sm.isAdsDone = true;
    }
}
