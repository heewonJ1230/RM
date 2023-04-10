using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds_Banner : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3541731";
#elif UNITY_ANDROID
    private string gameId = "3541730";
#elif UNITY_EDOTOR
    private string gameId = "1111111";
#endif

    private string placementName = "Banner3";
    public bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId,testMode);
        StartCoroutine(ShowBannerWhenInitialized());
        
    }

    public void HideBanner()

    {
        Debug.Log("ㅎㅏ이드?");
        StopCoroutine(ShowBannerWhenInitialized());
        Advertisement.Banner.Hide();
        Destroy(gameObject);

    }

        
    IEnumerator ShowBannerWhenInitialized()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.05f);
        }
        Advertisement.Banner.Show(placementName);
    }
}
