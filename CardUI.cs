using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Text creatureName;
    public Button prevBttn, nextBttn;
    public Image creatureImg;
    public Image[] mSlvl, breedingLvl,productivityLvl;

    public Text typeTxt;
    public Text typeMssgTxt;
    public Text txtStory;

    public Color statEmptyColor, moveSpeed1Color, breeding1Color, productivity1Color;

    CreatureData[] data;
    public GameObject cardBack;
   
    
    public bool openCheck = false;
    int curIndex;

    public enum CreatureType
    { 
        Normal,
        Special,
        Ghost
    }
    private void Start()
    {
        data = SaveLoadCard.CardLoad();
        UpdateCardUI();
     }

    private void Update()
    {
        data = SaveLoadCard.CardLoad();
        UpdateCardUI();
        BttnCheck();
    } 
    //---------------
    public void SetName(string txt)
    {
        creatureName.text = txt;
    }
    public void SetCreatureType(CreatureType type)
    {
        if (type == CreatureType.Normal)
        {
            typeTxt.text = "Normal";
            typeMssgTxt.text = "생물 구매로 획득 가능";
        }
        else if (type == CreatureType.Special)
        {
            typeTxt.text = "!Special!";
            typeMssgTxt.text = "생물 구매가 아닌 다른 특수한 방법으로 확률 획득";
        }
        else
        {
            typeTxt.text = "Ghost";
            typeMssgTxt.text = "생물이 죽으면 확률 획득";
        }
        //typeTxt.text = type == CreatureType.Normal ? "Normal" : "!Special!";
        //typeMssgTxt.text = type == CreatureType.Normal ? "생물 구매로 획득 가능" : "* Special * 생물 구매로 획득 불가능";
    }
    public void SetStory(string txt)
    {
        txtStory.text = txt;
    }

    public void SetMSStat(int level)
    {
        for(int i = 0; i<mSlvl.Length; i++)
        {
            var imgs = mSlvl;
            if (i < level)
            {
                imgs[i].color = moveSpeed1Color;
            }
            else
            {
                imgs[i].color = statEmptyColor;
            }
        }
    }
    public void SetBreedingStat(int level)
    {
        for (int i = 0; i < breedingLvl.Length; i++)
        {
            var imgs = breedingLvl;
            if (i < level)
            {
                imgs[i].color = breeding1Color;
            }
            else
            {
                imgs[i].color = statEmptyColor;
            }
        }
    }
    public void SetProductivityStat(int level)
    {
        for (int i = 0; i < productivityLvl.Length; i++)
        {
            var imgs = productivityLvl;
            if (i < level)
            {
                imgs[i].color = productivity1Color;
            }
            else
            {
                imgs[i].color = statEmptyColor;
            }
        }
    }

    public void SetCreatureImg(Sprite sprite)
    {
        creatureImg.sprite = sprite;
    }
 
     void MoveIndex(bool PrvorNext)
    {
        if (PrvorNext)
        {
            curIndex--;
        }
        else
        {
            curIndex++;
        }

        curIndex = Mathf.Clamp(curIndex, 0, data.Length - 1);

        UpdateCardUI();
    }
    private void UpdateCardUI()
    {
        if (data == null || data.Length == 0)
            return;

        var creatureData = data[curIndex];
        SetName(creatureData.creatureName);
        SetCreatureType((CreatureType)creatureData.creatureType);
        SetMSStat(creatureData.moveSpeed);
        SetBreedingStat(creatureData.breeding);
        SetProductivityStat(creatureData.productivity);
        SetStory(creatureData.story);
        SetCreatureImg(Resources.Load<Sprite>(creatureData.path));
        OpenCheck(creatureData.openCheck);
    }
    public void OpenCheck(bool check)
    {
        if (check)
        {
            cardBack.SetActive(false);
        }
        else
        {
            cardBack.SetActive(true);
        }
    }
    public void OnClickPrv()
    {
        if (null != GameObject.Find("Scrollbar_story"))
        {
            Scrollbar cardstorySB = GameObject.Find("Scrollbar_story").GetComponent<Scrollbar>();
            cardstorySB.value = 1;
        }
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
        fXController_ep1.NextCard();
        MoveIndex(true);
    }
    public void OnClickNext()
    {
        if (null != GameObject.Find("Scrollbar_story"))
        {
            Scrollbar cardstorySB = GameObject.Find("Scrollbar_story").GetComponent<Scrollbar>();
            cardstorySB.value = 1;
        }
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
        fXController_ep1.NextCard();
        MoveIndex(false);
    }
    void BttnCheck()
    {
        prevBttn.interactable = curIndex > 0;
        nextBttn.interactable = curIndex < data.Length-1;
    }
}
