using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellGCreature : MonoBehaviour //ㅍㅏㄴ매컨트롤 게임매니저처
{
    GameObject[] delete_gsh;
    GameObject[] delete_hsh;
    GameObject[] delete_nrdp;

    public GameObject sellGhostBttn;
    public int sellGhostCount;

    public Button bttn_Sell_Gshrimps;
    public Text bttn_Sell_Text_Gshrimps;

    public static long sellP_Gshrimps = 77377;

    public int sold_GshrimpsCount;


    public Button bttn_Sell_Hshrimps;
    public Text bttn_Sell_Text_Hshrimps;

    public static long sellP_Hshrimps = 77477;

    public int sold_HshrimpsCount = 0;


    public Button bttn_Sell_NRDP;
    public Text bttn_Sell_Text_NRDP;

    public static long sellP_NRDP = 77977;
    public int sold_NRDPCount = 0;

    CartoonManager cm;
    GameManager gm;

    int ranSellGhost;

    void Start()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        ranSellGhost = Random.Range(1, 100);

        Debug.Log("ㄹㅐㄴ덤셀?" + ranSellGhost + " 셀고스트카운트는" + sellGhostCount);
        if (cm.doneCartoon[7])
        {
            if (!gm.isKilled)
               {
                   if (ranSellGhost > 51 || sellGhostCount == 0) //ㄹㅐㄴㄷㅓㅁ 51
                   {
                       sellGhostBttn.SetActive(true);
                   }
                   else
                   {
                       sellGhostBttn.SetActive(false);
                   }
               }
               else
               {
                   sellGhostBttn.SetActive(false);
               }

            if(cm.doneCartoon[8] && !cm.doneCartoon[9])
            {
                sellGhostBttn.SetActive(false);
            }
        }

        else
        {
            sellGhostBttn.SetActive(false);
        }
        delete_gsh = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");
        delete_hsh = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");
        delete_nrdp = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");
     
        UpdatePanelCell_Gshrimps();
        UpdatePanelCell_Hshrimps();
        UpdatePanelCell_NRDP();
    }

    void Update()
    {
        if (cm.doneCartoon[7] && !gm.isKilled)
        {
            ButtonCell_GshrimpActiveCheck();
            ButtonCell_HshrimpActiveCheck();
            ButtonCell_NRDPActiveCheck();
            delete_gsh = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");
            delete_hsh = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");
            delete_nrdp = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");
        }
        else if (gm.isKilled)
        {
            sellGhostBttn.SetActive(false);
        }
    }

    public void OnclickSellGhostBttn()
    {
        sellGhostCount += 1;
    }
    public void UpdatePanelCell_Gshrimps()
    {
        PriceUpdate_Gshrimps();
        if (gm.ghost_ShrimpsCount == 0)
            bttn_Sell_Text_Gshrimps.text = "현재 0 마리 서식 중" + " \n";
        else
            bttn_Sell_Text_Gshrimps.text = "현재 " + gm.ghost_ShrimpsCount + " 마리 서식 중" + " \n";

        bttn_Sell_Text_Gshrimps.text += " 1 마리당 매매가 : "+"  ";

        if (gm.ghost_ShrimpsCount == 0)
            bttn_Sell_Text_Gshrimps.text += "이잉~, 한마리도 없는데 가격 알아 뭐하게. 쯔쯧";
        else
            bttn_Sell_Text_Gshrimps.text += sellP_Gshrimps.ToString("###,###") + " 물력";
    }

    void PriceUpdate_Gshrimps()
    {
        sellP_Gshrimps += gm.ghost_ShrimpsCount * 9001 - 101 * sold_GshrimpsCount;
        if(sellP_Gshrimps < 0)
        {
            sellP_Gshrimps = 101;
        }
    }

    public void Sell_Gshrimps()
    {
        if (gm.ghost_ShrimpsCount > 0)
        {
            if (gm.ghost_ShrimpsCount == 1)
            {
                GameObject a_delete_sh = GameObject.FindGameObjectWithTag("Ghost_normalShrimps");
                Destroy(a_delete_sh);
            }
            else
            {
                Destroy(delete_gsh[delete_gsh.Length - 1]);
            }
            gm.money += sellP_Gshrimps;
            gm.ghost_CreatureCount -= 1;
            gm.ghost_ShrimpsCount -= 1;
            sold_GshrimpsCount += 1;
            delete_gsh = GameObject.FindGameObjectsWithTag("Ghost_normalShrimps");

            UpdatePanelCell_Gshrimps();

            gm.Save();
        }
    }
    void ButtonCell_GshrimpActiveCheck()
    {
        if (gm.ghost_ShrimpsCount > 0)
        {
            bttn_Sell_Gshrimps.interactable = true;
        }
        else
        {
            bttn_Sell_Gshrimps.interactable = false;
        }
    }

    //----
    public void UpdatePanelCell_Hshrimps()
    {
        PriceUpdate_Hshrimps();
        if (gm.horse_ShrimpsCount == 0)
            bttn_Sell_Text_Hshrimps.text = "현재 0 마리 서식 중" + " \n";
        else
            bttn_Sell_Text_Hshrimps.text = "현재 " + gm.horse_ShrimpsCount + " 마리 서식 중" + " \n";

        bttn_Sell_Text_Hshrimps.text += " 1 마리당 매매가 : " + "  ";

        if (gm.horse_ShrimpsCount == 0)
            bttn_Sell_Text_Hshrimps.text += "눈 씻구 찾아보라, 없자네!";
        else
            bttn_Sell_Text_Hshrimps.text += sellP_Hshrimps.ToString("###,###") + " 물력";
    }

    void PriceUpdate_Hshrimps()
    {
        sellP_Hshrimps += gm.horse_ShrimpsCount * 9007 - 103 * sold_HshrimpsCount;
        if (sellP_Hshrimps < 0)
        {
            sellP_Hshrimps = 101;
        }
    }

    public void Sell_Hshrimps()
    {
        
        if (gm.horse_ShrimpsCount > 0)
        {
            if (gm.horse_ShrimpsCount == 1)
            {
                GameObject a_delete_sh = GameObject.FindGameObjectWithTag("Ghost_horseShrimps");
                Destroy(a_delete_sh);
            }
            else
            {
                Destroy(delete_hsh[delete_hsh.Length - 1]);
            }
            gm.money += sellP_Hshrimps;
            gm.ghost_CreatureCount -= 1;
            gm.horse_ShrimpsCount -= 1;
            sold_HshrimpsCount += 1;
            delete_hsh = GameObject.FindGameObjectsWithTag("Ghost_horseShrimps");

            UpdatePanelCell_Hshrimps();

            gm.Save();
        }
    }
    void ButtonCell_HshrimpActiveCheck()
    {
        if (gm.horse_ShrimpsCount > 0)
        {
            bttn_Sell_Hshrimps.interactable = true;
        }
        else
        {
            bttn_Sell_Hshrimps.interactable = false;
        }
    }


    //---
    public void UpdatePanelCell_NRDP()
    {
        PriceUpdate_NRDP();
        if (gm.noRe_DPsCount == 0)
            bttn_Sell_Text_NRDP.text = "현재 0 마리 서식 중" + " \n";
        else
            bttn_Sell_Text_NRDP.text = "현재 " + gm.noRe_DPsCount + " 마리 서식 중" + " \n";

        bttn_Sell_Text_NRDP.text += " 1 마리당 매매가 : " + "  ";

        if (gm.noRe_DPsCount == 0)
            bttn_Sell_Text_NRDP.text += "읍어! 읍다구!";
        else
            bttn_Sell_Text_NRDP.text += sellP_NRDP.ToString("###,###") + " 물력";
    }

    void PriceUpdate_NRDP()
    {
        sellP_NRDP += gm.noRe_DPsCount * 9011 - 107 * sold_NRDPCount;
        if (sellP_NRDP < 0)
        {
            sellP_NRDP = 107;
        }
    }

    public void Sell_NRDP()
    {
        if (gm.noRe_DPsCount > 0)
        {
            if (gm.noRe_DPsCount == 1)
            {
                GameObject a_delete_sh = GameObject.FindGameObjectWithTag("Ghost_noReDPs");
                Destroy(a_delete_sh);
            }
            else
            {
                Destroy(delete_nrdp[delete_nrdp.Length - 1]);
            }
            gm.money += sellP_NRDP;
            gm.ghost_CreatureCount -= 1;
            gm.noRe_DPsCount -= 1;
            sold_NRDPCount += 1;
            delete_nrdp = GameObject.FindGameObjectsWithTag("Ghost_noReDPs");

            UpdatePanelCell_NRDP();
            gm.Save();
        }
    }
    void ButtonCell_NRDPActiveCheck()
    {
        if (gm.noRe_DPsCount > 0)
        {
            bttn_Sell_NRDP.interactable = true;
        }
        else
        {
            bttn_Sell_NRDP.interactable = false;
        }
    }
}
