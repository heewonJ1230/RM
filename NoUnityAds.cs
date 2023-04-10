
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class NoUnityAds : MonoBehaviour
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
        Advertisement.Banner.Hide();
    }
    private void Update()
    {
        Advertisement.Banner.Hide();
    }

}