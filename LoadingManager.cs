using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    public GameObject loadingTxt;
    public Text loadingText;
    public GameObject loadedImg;
    public Button startBttn;
    float timeC = 0;
    public bool check = true;
    public float waitsecAfterbttn;
   
    void Start()
    {
        loadingBar.fillAmount = 0;
        StartCoroutine(LoadAsyncScene());
        Debug.Log(Application.persistentDataPath);
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) 
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void OnClickStartBttn()
    {
        check = false;
        StartCoroutine(WaitForAfterIt());
        string path = Application.persistentDataPath + "/save.xml";
        if(System.IO.File.Exists(path))
        {
            LoadScene("3ep1");
        }
        else
        {
            LoadScene("2Cartoon");
        }
        
    }
    IEnumerator WaitForAfterIt()
    {
        yield return new WaitForSeconds(waitsecAfterbttn);
        check = true;
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator TextAppear(float interval)
    {
        string txt = loadingText.text;
        int index = 0;
        while (index <= txt.Length-1)
        {
            loadingText.text = txt.Substring(0, index);
            yield return new WaitForSeconds(interval);
            ++index;
        }
        if(index >= txt.Length - 1)
        {
            index = 0;
        }

    }
    IEnumerator LoadAsyncScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("3ep1");
        asyncScene.allowSceneActivation = false;
        AsyncOperation asyncScene2 = SceneManager.LoadSceneAsync("2Cartoon");
        asyncScene2.allowSceneActivation = false;
        loadingBar.fillAmount = 1;
        while (!asyncScene.isDone && !asyncScene2.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;
            if(asyncScene.progress >= 0.9f)
            {

                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeC);
                if(loadingBar.fillAmount == 1.0f)
                {
                    startBttn.interactable = true;
                    loadingTxt.SetActive(false);
                    loadedImg.SetActive(true);
                    yield break;
                }
            }
            else
            {            
                StartCoroutine(TextAppear(0.1f));
                
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if(loadingBar.fillAmount >= asyncScene.progress)
                {
                    startBttn.interactable = false;
                    loadingTxt.SetActive(true);
                    loadedImg.SetActive(false);
                    timeC = 0f;
                }
            }
        }
    }
}
