using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemShuffle;

public class ParserDialogue : MonoBehaviour
{
    public DialogueDataTag[] Parse(string _CSVFileName) 
     {
            List<DialogueDataTag> dialogueList = new List<DialogueDataTag>(); 
            TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); 
       
        string[] data = csvData.text.Split(new char[] { '\n' });
       // Array.shuffle(data);
        for (int i = 1; i < data.Length; ++i)
        {
            string[] row = data[i].Split(new char[] { ',' });
            DialogueDataTag dialogue = new DialogueDataTag(); 
            List<string> innerNums = new List<string>();
            List<string> talkingCretures = new List<string>();
            List<string> talkingContents = new List<string>();
            for (int j = 0; j < row.Length; ++j)
            {
                if (row[j] != "") 
                {
                    if (j == 0)
                    {
                        int.TryParse(row[j], out dialogue.ID);
                    }
                    else if (j % 3 == 1 && j < row.Length)
                    {
                        innerNums.Add(row[j]);
                    }
                    else if (j % 3 == 2 && j < row.Length)
                    {
                        talkingCretures.Add(row[j]);
                    }
                    else if (j != 0 && j % 3 == 0 && j < row.Length)
                    {
                        talkingContents.Add(row[j]);
                    }
                    else { break; }
                }
                else { break; }
            }
            dialogue.innerNum1 = innerNums.ToArray();
            dialogue.speaking_Creature1 = talkingCretures.ToArray();
            dialogue.speaking_Text1 = talkingContents.ToArray();
            dialogueList.Add(dialogue);
            List.shuffle(dialogueList);
        }
        return dialogueList.ToArray();
    }
}

