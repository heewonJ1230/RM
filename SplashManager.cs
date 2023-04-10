using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Text text1episode, text2copyrifht, testingTxt;
    
    public Image splashImg;
    public float delayTime;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);
        NextScene();
    }
    void Update()
    {
        Invoke("TextEffects", 2.2f); 
        Invoke("ByeBye", 3.2f);
    }
    void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    void ByeBye()
    {
        Image img = splashImg.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.009f);
    }   
    void TextEffects()
    {
        Text txt1 = text1episode.GetComponent<Text>();
        Text txt2 = text2copyrifht.GetComponent<Text>();
        Text testing = testingTxt.GetComponent<Text>();
       
        txt1.color = new Color(txt1.color.r, txt1.color.g, txt1.color.b, txt1.color.a + 0.1f);
        txt2.color = new Color(txt2.color.r, txt2.color.g, txt2.color.b, txt2.color.a + 0.05f);
        testingTxt.color = new Color(testing.color.r, testing.color.g, testing.color.b, testing.color.a + 0.1f);

    }
}
