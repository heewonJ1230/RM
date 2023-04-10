using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]//클레스 자체를 직렬화
public class CreatureData
{
    [Tooltip("게임에 보이지 않는 고유 넘버")]
    public int creatureID;

    [Tooltip("일반인지 스페셜인지")]
    public int creatureType;

    [Tooltip("이름")]
    public string creatureName;

    [Tooltip("의미없는 스피드...ㅎ")]
    public int moveSpeed;

    [Tooltip("얼마나 싼지;ㅋㅋ")]
    public int breeding;

    [Tooltip("물력 생산력")]
    public int productivity;

    [Tooltip("게임에 보이지 않는 고유 넘버")]
    public string story;
    [Tooltip("이미지")]
    public string path;
    [Tooltip("한번이라도 있었는지")]
    public bool openCheck;
}

public class SaveLoadCard : MonoBehaviour
{
    public CreatureData[] data;

    void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //data = CardLoad();

        data[0].openCheck = true;
        if (gm.normalShrimp || gm.shrimpsCount>=1)
        {
            data[1].openCheck = true;
        }
        if (gm.normalDP || gm.dpsCount >= 1)
        {
            data[2].openCheck = true;
        }
        if (gm.EggShrimp)
        {
            data[3].openCheck = true;
        }
        if (gm.angryDP)
        {
            data[4].openCheck = true;
        }
        if (gm.CHS)
        {
            data[5].openCheck = true;
        }
        if (gm.AS)
        {
            data[6].openCheck = true;
        }
        if (gm.SDP)
        {
            data[7].openCheck = true;
        }
        if (gm.ghost_normalShrimp)
        {
            data[8].openCheck = true;
        }
        if (gm.ghost_horseShrimp)
        {
            data[9].openCheck = true;
        }
        if (gm.ghost_noRe_DP)
        {
            data[10].openCheck = true;
        }
        if (gm.betta)
        {
            data[11].openCheck = true;
        }
        if (gm.nLS)
            data[12].openCheck = true;
        if (gm.parentS)
            data[13].openCheck = true;
        if (gm.saskS)
            data[14].openCheck = true;


        CardSave(data);
        data = CardLoad();
    }
    void Update()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        data[0].openCheck = true;
        if (gm.normalShrimp || gm.shrimpsCount >= 1)
        {
            data[1].openCheck = true;
        }
        if (gm.normalDP || gm.dpsCount >= 1)
        {
            data[2].openCheck = true;
        }
        if (gm.EggShrimp)
        {
            data[3].openCheck = true;
        }
        if (gm.angryDP)
        {
            data[4].openCheck = true;
        }
        if (gm.CHS)
        {
            data[5].openCheck = true;
        }
        if (gm.AS)
        {
            data[6].openCheck = true;
        }
        if (gm.SDP)
        {
            data[7].openCheck = true;
        }
        if (gm.ghost_normalShrimp)
        {
            data[8].openCheck = true;
        }
        if (gm.ghost_horseShrimp)
        {
            data[9].openCheck = true;
        }
        if (gm.ghost_noRe_DP)
        {
            data[10].openCheck = true;
        }
        if (gm.betta)
        {
            data[11].openCheck = true;
        }
        if (gm.nLS)
            data[12].openCheck = true;
        if (gm.parentS)
            data[13].openCheck = true;
        if (gm.saskS)
            data[14].openCheck = true;

        CardSave(data);
        data = CardLoad();
    }
    private void OnApplicationPause() { CardSave(data); }
    public void OnApplicationQuit()
    {
        CardSave(data);
    }

    public void CardSave(CreatureData[] data)
    {
        var formatter = new BinaryFormatter();
        using (var stream = new FileStream(Application.persistentDataPath + "/Save.CreatureData", FileMode.Create))
        {
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    static public CreatureData[] CardLoad()
    {
        if(File.Exists(Application.persistentDataPath + "/Save.CreatureData") == false)
        {
            return null;
        }
        var formatter = new BinaryFormatter();

        using (var stream = new FileStream(Application.persistentDataPath + "/Save.CreatureData", FileMode.Open))
        {
            var result = formatter.Deserialize(stream) as CreatureData[];
            return result;
        }
    }
}
