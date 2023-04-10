using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGuideShowUp : MonoBehaviour
{
    public bool isTouchDown;
    public float pressTimer;
    public float toggleTime;
    public GameObject toggleGuide;

    private void Update()
    {
        if (isTouchDown)
        {
            pressTimer += Time.deltaTime;

            if (pressTimer >= toggleTime)
            {
                toggleGuide.SetActive(true);
                isTouchDown = false;

                if (pressTimer >= 2 * toggleTime)
                {
                    pressTimer = 0; toggleGuide.SetActive(false);
                }
            }
        }
    }

    public void PressStart()
    {
        isTouchDown = true;
    }
    public void PointerUp()
    {
        isTouchDown = false;
        toggleGuide.SetActive(false);
        pressTimer = 0;
    }
   
}
