using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleMaker : MonoBehaviour //--얘가 사실 버블 자체
{
    [SerializeField] private GameObject imgbubble;
    public Text talkingText;
    public Text bubblePriceTxt;
    public GameObject goBubblePriceTxt;
    public Image BubbleImg;
    bool isclicked = false;
    void Awake()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();
        UpdateSpeakBubble();
        if (cdc.marimoIsSick)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 7.0f);
        }
    }

    void Update()
    {
        CommonDataController cdc = GameObject.Find("CartoonNHcho").GetComponent<CommonDataController>();

        //transform.rotation = Quaternion.identity;
        //Vector3 motherVec = transform.parent.localScale;
        //string mothername = transform.parent.tag;
       
        if (isclicked)
        {
            bubblePriceTxt.color = new Color(bubblePriceTxt.color.r, bubblePriceTxt.color.g, bubblePriceTxt.color.b, bubblePriceTxt.color.a - 0.03f);
            talkingText.color = new Color(talkingText.color.r, talkingText.color.g, talkingText.color.b, talkingText.color.a - 0.03f);
            BubbleImg.color = new Color(BubbleImg.color.r, BubbleImg.color.g, BubbleImg.color.b, BubbleImg.color.a - 0.03f);
            bubblePriceTxt.transform.position = Vector2.MoveTowards(bubblePriceTxt.transform.position, new Vector2(-2.0f,4.5f), Time.deltaTime * 1f);
        }
        else if (cdc.marimoIsSick)
        {
            Destroy(this.gameObject);
        }

        if(null!= gameObject.transform.parent)
        {
            this.gameObject.transform.position = this.gameObject.transform.parent.GetChild(0).transform.position;
        }
    }
    void UpdateSpeakBubble()
    {
        StartCoroutine(AppearbubbleTxt(0.05f));
    }
    public void OnClickBubble()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.money += 5 * gm.moneyIncreaseAmount;
        fXController_ep1.EndSFX();
        bubblePriceTxt.text = "+ "+ (5 * gm.moneyIncreaseAmount).ToString("###,###");
        Destroy(this.gameObject, 1.0f);
        isclicked = true;
    }
 
    IEnumerator AppearbubbleTxt(float interval)
    {
        string speakb_txt = DialogueBubble.newtalkTxt;
        int index = 0;
        speakb_txt = speakb_txt.Replace("'", ",");
        while (index <= speakb_txt.Length)
        {
            talkingText.text = speakb_txt.Substring(0, index);
            yield return new WaitForSeconds(interval);
            index++;
        }
    }
}
