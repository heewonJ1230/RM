using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplasFXManager : MonoBehaviour
{
    void Start()
    {
        FXController fXController_splash = GameObject.Find("Image").GetComponent<FXController>();
        fXController_splash.InvokeWaterDrops_Start();
        fXController_splash.InvokeWaterDrops();
    }

}
