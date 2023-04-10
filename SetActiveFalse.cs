using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFalse : MonoBehaviour
{
    bool isSetActive;
    public float offSec;
    void Update()
    {
        if (gameObject.activeSelf && !isSetActive)
        {
            isSetActive = true;
            Invoke("SetActiveOff", offSec);
        }
    }

    void SetActiveOff()
    {
        gameObject.SetActive(false);
        isSetActive = false;
    }
}
