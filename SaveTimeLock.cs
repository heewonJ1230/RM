using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTimeLock : MonoBehaviour
{
    public Button saveBttn;
    public Text saveBttn_text;
    public int setOrizenSize;
    public int setTimerSize;
    public int saveLockTime;
    private int saveLockTime_Sec;
    private int savelockTimer;
    private bool isPressedSB;


    // Start is called before the first frame update
    void Start()
    {
        saveBttn.interactable = true;
        isPressedSB = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressedSB)
        {
            saveBttn.interactable = false;
        }
        else
        {
            saveBttn.interactable = true;
        }
    }
    public void StartTimer_SaveBttn()
    {
        if (!isPressedSB)
        {
            isPressedSB = true;
            saveBttn.interactable = false;
            StartCoroutine("SaveTimerStart");
        }
    }


    IEnumerator SaveTimerStart()
    {
        savelockTimer = saveLockTime;
        saveBttn_text.text = savelockTimer + " 분" + saveLockTime_Sec + " 초";
        while (savelockTimer > 0)
        {
            saveBttn_text.fontSize = setTimerSize;
            yield return new WaitForSeconds(1.0f);
            saveLockTime_Sec -= 1;
            if(saveLockTime_Sec <= 0)
            {
                savelockTimer -= 1;
                saveLockTime_Sec += 60;
            }
            saveBttn_text.text = savelockTimer + " 분  " + saveLockTime_Sec + " 초";
        }
        isPressedSB = false;
        saveBttn_text.fontSize = setOrizenSize;
        saveBttn_text.text = "서버에 저장";
        yield break;
    }
}
