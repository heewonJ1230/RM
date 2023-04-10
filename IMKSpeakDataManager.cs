using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMKSpeakDataManager : MonoBehaviour
{
    public static IMKSpeakDataManager instance;

    [SerializeField] string csv_FileNameIML;
    Dictionary<int, IMKSpeakTag> speakDic = new Dictionary<int, IMKSpeakTag>();

    public static bool isFinished = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            IMKParserSpeak iMKParser = GetComponent<IMKParserSpeak>();
            IMKSpeakTag[] iMKSpeaks = iMKParser.ParseIMK(csv_FileNameIML);

            for(int i= 0; i < iMKSpeaks.Length; i++)
            {
                speakDic.Add(i + 1, iMKSpeaks[i]);
            }
            isFinished = true;
        }
    }

    public IMKSpeakTag[] GetIMKSpeak(int _startNum, int _endNum)
    {
        List<IMKSpeakTag> iMKSpeaksList = new List<IMKSpeakTag>();
        for(int i= 0; i <= _endNum -_startNum; i++)
        {
            iMKSpeaksList.Add(speakDic[_startNum + i]);
        }
        return iMKSpeaksList.ToArray();
    }
}
