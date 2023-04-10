using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMKParserSpeak : MonoBehaviour
{
   public IMKSpeakTag[] ParseIMK(string _CSVFileName)
    {
        List<IMKSpeakTag> iMKSpeaksList = new List<IMKSpeakTag>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        string[] data = csvData.text.Split(new char[] { '\n' });
        for(int i = 1; i < data.Length;)//여기서 첫번째줄 제외 - 그 엑셀에 구분하는. 
        {
            string[] row = data[i].Split(new char[] { ',' });
            IMKSpeakTag iMKSpeak = new IMKSpeakTag();

            //int.TryParse(row[0], out iMKSpeak.ID);
            iMKSpeak.situationNum_IMK = row[0];
            iMKSpeak.situationIMK = row[1];
            //int.TryParse(row[2], out iMKSpeak.ID);
            //iMKSpeak.ID = row[2];

            List<string> mSpeakIDList = new List<string>();
            List<string> mSpeakList = new List<string>();

            do
            {
                mSpeakIDList.Add(row[2]);
                mSpeakList.Add(row[3]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while(row[0].ToString() == "");

            iMKSpeak.ID = mSpeakIDList.ToArray();
            iMKSpeak.speakingTexts_IMK = mSpeakList.ToArray();
            iMKSpeaksList.Add(iMKSpeak);
            /*
            for (int j = 0; j < row.Length; j++) << 이꼴보라고 ㅠㅠ 이렇게 해놨다고
            {
                if(row[j] != "")
                {
                    if (j == 0)
                    {
                        int.TryParse(row[j], out iMKSpeak.ID);
                    }
                    else if(j == 1)
                    {
                        iMKSpeak.situationIMK = row[j];
                    }
                    else if (j == 2)
                    {
                        iMKSpeak.situationNum = row[j];
                    }
                    else if(j == 3)
                    {
                        iMKSpeak.speakingText_IMK = row[j];
                    }
                    else
                    {
                        break;
                    }
                }
                else { break; }*/
        }
        return iMKSpeaksList.ToArray();
        
        //iMKSpeaksList.Add(iMKSpeak);
        //return iMKSpeaksList.ToArray();
    }
}
