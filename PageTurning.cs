using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Firebase.Storage;
using Firebase;
using System.IO;
using Firebase.Database;


[System.Serializable]//클레스 자체를 직렬화
public class CartoonData
{
    [Tooltip("몇화인지")]
    public int hwaNum;
    [Tooltip("페이지 수")]
    public int pageCount;
    [Tooltip("이미지들의 주소록")]
    public string[] imgPath;
}
public class PageTurning : MonoBehaviour
{
    //---유니티 문서에서 일다 가져와본다!
    public Texture2D sourceTex;
    public float warpFactor = 1.0f;

    Texture2D destTex;
    Color[] destPix;

    //--
    RectTransform rectTransform;
    


    public string[] scenename;
    bool isTurningNextScene = false;
    public Image sceneImgZero, sceneImgOne;
    public GameObject sceneGOzero, ScenCOOne;
    public Vector2 imgSize;

    public float slideSpeed = 10f;
    public Image imgZero;
    public Image imgOne;
    public Image imgTwo;

    RectTransform rectImgZero;
    RectTransform rectImgOne;
    RectTransform rectImgTwo;
    public GameObject goZero;//- 셋엑티브 제어를 위해서
    public GameObject goOne;
    public GameObject goTwo;
    public GameObject cartoonGuide;

    bool isTurningNext = false;
    bool isTurningPrev = false;
    int curShowingImg;
    CartoonManager cm;
    FB_ImageLoader fb_il;

    //-- 저장된 것에서 가져옴
    int hwaNum;
    int pageCount;
    [Header("잘가져왔는지 확인")]
    public int curIndex; //-이게 전체 만화중 몇번째 데이터인지 몇화인지 고르는 부분. 고유 데이터 순서값.
    public string[] ctLoadPath;
    //Texture2D texture = new Texture2D(0, 0);

    //--- 내가 추가한거
    public int curPageIndex; //- 0,1,2가 계속 바뀌는 함수 만들려고 이게 몇페이지인지 쪽!
    public CartoonData[] cartoons;
    string imgPathTxtZero, imgPathTxtOne, imgPathTxtTwo;//--변화해서 집어넣는 부분

    string imgPathLocalZero, imgPathLocalOne, imgPathLocalTwo; //-- 로컬 이미지 

    private Transform tr;
    private Vector2 firstTouch;

    //--ㅍㅏ이어베이스 스토리지.
    DatabaseReference reference;
    Firebase.Auth.FirebaseUser user;

    //--지금 보는 거!
    // public int lookinghwa;
    // public int lookinghwaPageCount;

    // 내 파이어베이스 gs://rm-rel.appspot.com
    //var storage = FirebaseStorage.GetInstance("gs://my-custom-bucket");

    // Create a storage reference from our storage service

    void Awake()
    {
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();
        fb_il = GameObject.Find("FB_ImageLoader").GetComponent<FB_ImageLoader>();
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        StorageReference storageRef = storage.GetReferenceFromUrl("gs://rm-rel.appspot.com");
        curIndex = CartoonManager.curIndex;
        rectImgZero = imgZero.GetComponent<RectTransform>();
        rectImgOne = imgOne.GetComponent<RectTransform>();
        rectImgTwo = imgTwo.GetComponent<RectTransform>();
        curShowingImg = 0;
        curPageIndex = 0;
        UpdateCartoonWha();
        Debug.Log("몇화인지UpdateCartoonWha   " + curIndex + "화");
        Debug.Log("어웨이크 커 페이지 인덱"+ curPageIndex);
       // Debug.LogError("Awake할때  지금 커인덱스 " + curIndex);
       // rectTransform = GetComponent<RectTransform>();
        //float width = rectTransform.rect.width;
        //float height = rectTransform.rect.height;
    }
    void Start()
    {

        tr = GetComponent<Transform>();
        goOne.SetActive(false);
        curPageIndex = 0;
        if(curIndex == 0)
            StartCoroutine(PageUpdate());
        else
        {
            GetPage(); //--이거 넣으니까 2화 걸리는거 해결됨 근데 몰라 또 다음화에서는 안될지.
            StartCoroutine(PageUpdate());//이게 왜 2화가 걸려

        }
        StartCoroutine(NextScenego());
        Debug.LogError("몇화인지UpdateCartoonWha   " + curIndex + "화");
        if (curIndex == 0 || curIndex == 1)
        {
            cartoonGuide.SetActive(true);
        }
        else
        {
            cartoonGuide.SetActive(false);
        }
        Debug.Log("시작할때  지금 커인덱스 " + curIndex);

    }

    public float delayTime;

    IEnumerator NextScenego()
    {
        if (curIndex == 9)
        {
            yield return new WaitForSeconds(180.0f);

            SceneManager.LoadScene(5);
        }
        else
        {
            yield return new WaitForSeconds(180.0f);

            SceneManager.LoadScene(3);
        }
    }
    void Update()
    {
        PageTurn();
        // UpdateCartoonWha(); --ㅇㅣ거 하면 매 업데이트 마다 카툰 넘어간다고;
        FakeTurn();

    }

    //--- 신 업데이트로 넘어가는 함수 

    void GoNextSyncScene()
    {
        if (isTurningNextScene)
        {
            SceneManager.LoadScene(3);
        }
    }
    void GoMK()
    {
        if (isTurningNextScene)
        {
            SceneManager.LoadScene(5);
        }
    }

    //--- 이미지 체우는 함수
    public void GetPage()
    {
        Debug.Log("fb_il에서 넘어온것들 " + fb_il.hwaNum + "   " + fb_il.pageCount);
        curIndex = fb_il.hwaNum;
        hwaNum = curIndex;
        pageCount = fb_il.pageCount;
        string cartoonPath = Application.persistentDataPath + "/Cartoons/" +
            hwaNum + "/" + hwaNum + "_";

        for (int i = 0; i <= pageCount; ++i)
        {
           
            if (i == pageCount)
            {
                cartoons[hwaNum].imgPath[i] = cartoonPath + (i - 1).ToString() + ".png";

            }
            else
            {
                ctLoadPath[curPageIndex] = cartoonPath + i.ToString() + ".png";
                cartoons[hwaNum].imgPath[i] = cartoonPath + i.ToString() + ".png";

            }
            Debug.Log("이미지 패스 겟페이지에  " + i +"ctloadPath" + ctLoadPath[curPageIndex]);
            //Debug.Log(" 겟페이지 되는거야? " + ctLoadPath[i]);
        }

    }
    //------카드가 도는 함수

    void PageTurn()
    {
        FXController fXController_Cartoon = GameObject.Find("FXController").GetComponent<FXController>();

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("누른거입력");
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (curPageIndex >= ctLoadPath.Length - 2)
            {
                Debug.LogError("페이지턴  지금 커인덱스 " + curIndex);
                if (Vector2.Distance(firstTouch, currentTouch) > 0.7f) //드래그 범위일때
                {
                    if (firstTouch.x > currentTouch.x)
                    {
                        Debug.Log(
      "PageTurnning - 페이지 넘기기 넥스트   컬페이지 인덱스는?" + curPageIndex + "   지금 커인덱스 " + curIndex + "      씨티 로드 패스 랭쓰에서 문제나는듯 - " + ctLoadPath.Length);

                        isTurningNext = false;
                        fXController_Cartoon.PageTurnFX();
                        isTurningNextScene = true;
                        if (curIndex == 4)
                        {
                            CommonDataController.hchomoney += 151 * CommonDataController.hchomoneyIncreaseAmount;
                        }
                        if (curIndex == 9)
                        {
                            Invoke("GoMK", 0.2f);
                        }
                        else
                        {
                            Invoke("GoNextSyncScene", 0.2f);
                        }
                    }
                }
            }
            else
            {
                if (Vector2.Distance(firstTouch, currentTouch) > 0.7f) //드래그 범위일때
                {
                    if (firstTouch.x > currentTouch.x) //처음 터치보다 오른쪽으로 드래그 했을때
                    {
                        fXController_Cartoon.PageTurnFX();
                        curPageIndex++;
                        isTurningNext = true;
                        StartCoroutine(PageUpdate());

                        OnNextPageFinished();
                    }
                    else if (firstTouch.x < currentTouch.x)//처음 왼쪽으로 드래그했을때
                    {
                        if (curPageIndex == 0)
                        {
                            isTurningPrev = false;
                        }
                        else
                        {
                            fXController_Cartoon.PageTurnFX();
                            isTurningPrev = true;
                            StartCoroutine(PageUpdate());
                            OnPrevPageFinished();
                            curPageIndex--;
                        }

                        //Debug.Log(curPageIndex);

                    }
                }

            }
        }


    }
    //---버튼으로 가기ㄱ
    public void OnclickNExt()
    {
        FXController fXController_Cartoon = GameObject.Find("FXController").GetComponent<FXController>();
        //Debug.Log(
          //  "PageTurnning - 온클릭 넥스트   컬페이지 인덱스는?" + curPageIndex + "   지금 커인덱스 " + curIndex + "      씨티 로드 패스 랭쓰에서 문제나는듯 - " + ctLoadPath.Length);
        if (curPageIndex >= ctLoadPath.Length - 2)
        {
            isTurningNext = false;
            fXController_Cartoon.PageTurnFX();
            isTurningNextScene = true;
            if (curIndex == 4)
            {
                CommonDataController.hchomoney += 151 * CommonDataController.hchomoneyIncreaseAmount;
            }
            if (curIndex == 9)
            {
                Invoke("GoMK", 0.2f);
            }
            else
            {
                Invoke("GoNextSyncScene", 0.2f);
            }
        }
        else
        {
            fXController_Cartoon.PageTurnFX();
            curPageIndex++;
            isTurningNext = true;
            StartCoroutine(PageUpdate());

            OnNextPageFinished();
        }

    }

    public void OnClickPrev()
    {
        FXController fXController_Cartoon = GameObject.Find("FXController").GetComponent<FXController>();

        if (curPageIndex == 0)
        {
            isTurningPrev = false;
        }
        else
        {
            fXController_Cartoon.PageTurnFX();
            isTurningPrev = true;
            StartCoroutine(PageUpdate());
            OnPrevPageFinished();
            curPageIndex--;
        }
    }

    public void OnNextPageFinished()
    {
        if (curShowingImg == 0)
        {
            curShowingImg = 1;
        }
        else if (curShowingImg == 1)
        {
            curShowingImg = 2;
        }
        else
        {
            curShowingImg = 0;
        }
        if (curShowingImg == 0)//여기에 조건 늘리기 현재 페이지수가 같은 화의 페이지량보다 작은가 그거
        {
            rectImgZero.anchoredPosition = new Vector2(1549, rectImgZero.anchoredPosition.y);
            goOne.SetActive(false);
            goZero.SetActive(true);
        }
        else if (curShowingImg == 1)
        {
            rectImgOne.anchoredPosition = new Vector2(1549, rectImgOne.anchoredPosition.y);
            goTwo.SetActive(false);
            goOne.SetActive(true);
        }
        else
        {
            rectImgTwo.anchoredPosition = new Vector2(1549, rectImgTwo.anchoredPosition.y);
            goZero.SetActive(false);
            goTwo.SetActive(true);
        }
        isTurningNext = true;
    }
    public void OnPrevPageFinished()
    {
        if (curShowingImg == 0)
        {
            curShowingImg = 2;
        }
        else if (curShowingImg == 1)
        {
            curShowingImg = 0;
        }
        else
        {
            curShowingImg = 1;
        }


        if (curShowingImg == 0)
        {
            rectImgZero.anchoredPosition = new Vector2(-1549, rectImgZero.anchoredPosition.y);
            goOne.SetActive(true);
            goZero.SetActive(true);
            goTwo.SetActive(false);
        }
        else if (curShowingImg == 1)
        {
            rectImgOne.anchoredPosition = new Vector2(-1549, rectImgOne.anchoredPosition.y);
            goTwo.SetActive(true);
            goOne.SetActive(true);
            goZero.SetActive(false);
        }
        else
        {
            rectImgTwo.anchoredPosition = new Vector2(-1549, rectImgTwo.anchoredPosition.y);
            goZero.SetActive(true);
            goTwo.SetActive(true);
            goOne.SetActive(false);
        }
        isTurningPrev = true;
    }
    void FakeTurn()//이전으로 가는것도 만들어야함.
    {
        if (isTurningNext == true)
        {
            //Debug.LogError("페이크 턴할때  지금 커인덱스 " + curIndex);

            float targetZero = 0;
            float targetOne = 0;
            float targetTwo = 0;
            if (curShowingImg == 0)
            {
                targetZero = 0;
                targetOne = 1549;
                targetTwo = -1549;
            }
            else if (curShowingImg == 1)
            {
                targetZero = -1549;
                targetOne = 0;
                targetTwo = 1549;
            }
            else if (curShowingImg == 2)
            {
                targetZero = 1549;
                targetOne = -1549;
                targetTwo = 0;
            }

            float posZero = rectImgZero.anchoredPosition.x;
            posZero = Mathf.Lerp(posZero, targetZero, Time.deltaTime * slideSpeed);
            float posOne = rectImgOne.anchoredPosition.x;
            posOne = Mathf.Lerp(posOne, targetOne, Time.deltaTime * slideSpeed);
            float posTwo = rectImgTwo.anchoredPosition.x;
            posTwo = Mathf.Lerp(posTwo, targetTwo, Time.deltaTime * slideSpeed);

            rectImgZero.anchoredPosition = new Vector2(posZero, rectImgZero.anchoredPosition.y);
            rectImgOne.anchoredPosition = new Vector2(posOne, rectImgOne.anchoredPosition.y);
            rectImgTwo.anchoredPosition = new Vector2(posTwo, rectImgTwo.anchoredPosition.y);

            if (Mathf.Abs(rectImgZero.anchoredPosition.x - targetZero) < 0.05f && Mathf.Abs(rectImgOne.anchoredPosition.x - targetOne) < 0.05f &&
                 Mathf.Abs(rectImgTwo.anchoredPosition.x - targetTwo) < 0.05f)
            {
                rectImgZero.anchoredPosition = new Vector2(targetZero, rectImgZero.anchoredPosition.y);
                rectImgOne.anchoredPosition = new Vector2(targetOne, rectImgOne.anchoredPosition.y);
                rectImgTwo.anchoredPosition = new Vector2(targetTwo, rectImgTwo.anchoredPosition.y);
                isTurningNext = false;
            }
        }

        else if (isTurningPrev == true)
        {
            // Debug.LogError("페이크 턴할때  지금 커인덱스 " + curIndex);

            float targetZero = 0;
            float targetOne = 0;
            float targetTwo = 0;
            if (curShowingImg == 0)
            {
                targetZero = 0;
                targetOne = 1549;
                targetTwo = -1549;
            }
            else if (curShowingImg == 1)
            {
                targetZero = -1549;
                targetOne = 0;
                targetTwo = 1549;
            }
            else if (curShowingImg == 2)
            {
                targetZero = 1549;
                targetOne = -1549;
                targetTwo = 0;
            }

            float posZero = rectImgZero.anchoredPosition.x;
            posZero = Mathf.Lerp(posZero, targetZero, Time.deltaTime * slideSpeed);
            float posOne = rectImgOne.anchoredPosition.x;
            posOne = Mathf.Lerp(posOne, targetOne, Time.deltaTime * slideSpeed);
            float posTwo = rectImgTwo.anchoredPosition.x;
            posTwo = Mathf.Lerp(posTwo, targetTwo, Time.deltaTime * slideSpeed);

            rectImgZero.anchoredPosition = new Vector2(posZero, rectImgZero.anchoredPosition.y);
            rectImgOne.anchoredPosition = new Vector2(posOne, rectImgOne.anchoredPosition.y);
            rectImgTwo.anchoredPosition = new Vector2(posTwo, rectImgTwo.anchoredPosition.y);

            if (Mathf.Abs(rectImgZero.anchoredPosition.x - targetZero) < 0.05f && Mathf.Abs(rectImgOne.anchoredPosition.x - targetOne) < 0.05f &&
                 Mathf.Abs(rectImgTwo.anchoredPosition.x - targetTwo) < 0.05f)
            {
                rectImgZero.anchoredPosition = new Vector2(targetZero, rectImgZero.anchoredPosition.y);
                rectImgOne.anchoredPosition = new Vector2(targetOne, rectImgOne.anchoredPosition.y);
                rectImgTwo.anchoredPosition = new Vector2(targetTwo, rectImgTwo.anchoredPosition.y);
                isTurningPrev = false;
            }
        }

    }

    //----------------- 여기서 부터 페이지 수  다시 하는 함수 제작
    IEnumerator PageUpdate() //-- 이거 바꿔야됨; //-- 보여지는 페이지 교체
    {
        while (curPageIndex < ctLoadPath.Length - 1)
        {
//            Debug.LogError(curPageIndex +"페이지 업데이트 "+ctLoadPath[0]);
           // Debug.LogError("ctLoadPath[curPageIndex]:"+ctLoadPath[curPageIndex] );
            if (curPageIndex == 0)
            {
                imgPathTxtZero = ctLoadPath[0];
                imgPathLocalZero = ctLoadPath[0];
               // Debug.LogError("페이지 업데이트시 지금 시티로드패스 curPageIndex == 0" + ctLoadPath[0]);


                if (curIndex == 0)
                    SetImgZero(Resources.Load<Sprite>(imgPathTxtZero));
                else
                    SetImgfrLocalZero(imgPathLocalZero);
            }
            else if (curPageIndex != 0 && curPageIndex % 3 == 1)
            {
                imgPathTxtOne = ctLoadPath[curPageIndex];
                imgPathLocalOne = ctLoadPath[curPageIndex];

               // Debug.LogError("페이지 업데이트시 지금 시티로드패스 ONE  " + ctLoadPath[curPageIndex]);

            }
            else if (curPageIndex != 0 && curPageIndex % 3 == 2)
            {
               // Debug.LogError("페이지 업데이트시 지금 시티로드패스 TwO" + ctLoadPath[curPageIndex]);

                imgPathTxtTwo = ctLoadPath[curPageIndex];
                imgPathLocalTwo = ctLoadPath[curPageIndex];
            }
            else if (curPageIndex != 0 && curPageIndex % 3 == 0 && curPageIndex != 0)
            {
               // Debug.LogError("페이지 업데이트시 지금 시티로드패스 Three " + ctLoadPath[curPageIndex]);

                imgPathTxtZero = ctLoadPath[curPageIndex];
                imgPathLocalZero = ctLoadPath[curPageIndex];
                if (curIndex == 0)
                    SetImgZero(Resources.Load<Sprite>(imgPathTxtZero));
                else
                    SetImgfrLocalZero(imgPathLocalZero);

            }
            if (curIndex == 0)
            {
                // Debug.LogError("페이지 업데이트시 지금 시티로드패스 " + ctLoadPath[0]);

                SetImgOne(Resources.Load<Sprite>(imgPathTxtOne));
                SetImgTwo(Resources.Load<Sprite>(imgPathTxtTwo));
            }
            else
            {
                // Debug.LogError("페이지 업데이트시 지금 시티로드패스 " + ctLoadPath[0]);

                SetImgfrLocalOne(imgPathLocalOne);
                SetImgfrLocalTwo(imgPathLocalTwo);
            }
            yield return new WaitForSeconds(0.03f);
        }
    }
    private void UpdateCartoonWha()
    {
        if (cartoons == null || cartoons.Length == 0)
            return;
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Debug.Log("몇화인지UpdateCartoonWha   " + curIndex + "화");
            SaveData saveData = XmlManager.XmlLoad<SaveData>(path);
            for (int i = 0; i <= CartoonManager.curIndex; ++i)
            {
                cm.doneCartoon[i] = true;
            }

            if (curIndex == 0)
            {
                curIndex = 1;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 1)
            {
                curIndex = 2;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 2)
            {
                curIndex = 3;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 3)
            {
                curIndex = 4;
                CartoonManager.curIndex += 1;

            }
            else if (curIndex == 4)
            {
                curIndex = 5;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 5)
            {
                curIndex = 6;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 6)
            {
                curIndex = 7;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 7)
            {
                curIndex = 8;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 8)
            {
                curIndex = 9;
                CartoonManager.curIndex += 1;
            }
            else if (curIndex == 9)//curindex10은 11화임
            {
                curIndex = 10;
                CartoonManager.curIndex += 1;
            }
            Debug.Log("몇화인지" + curIndex + "화");

            var cartoonData = cartoons[curIndex];
            ctLoadPath = cartoonData.imgPath;

            SetHwaNum(cartoonData.hwaNum);
            SetPageCount(cartoonData.pageCount);
            SetimgPath(cartoonData.imgPath);
        }
        else
        {
            curIndex = 0;
            var cartoonData = cartoons[curIndex];
            SetHwaNum(cartoonData.hwaNum);
            SetPageCount(cartoonData.pageCount);
            SetimgPath(cartoonData.imgPath);
        }
    }
    void SetHwaNum(int num)
    {
        hwaNum = num;
    }
    void SetPageCount(int count)
    {
        pageCount = count;
    }


    private void SetimgPath(string[] txts)
    {
        if (curIndex == 0)
        {
            ctLoadPath = txts;

        }
        else
        {

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                for (int i = 0; i < txts.Length; i++)
                {
                    ctLoadPath[i] =
                    Path.Combine(Application.persistentDataPath, txts[i]);

                    // Path.Combine(Application.persistentDataPath, txts[i]);

                };
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                for (int i = 0; i < txts.Length; i++)
                {
                    string path = Application.persistentDataPath;
                    path = path.Substring(0, path.LastIndexOf('/'));
                    ctLoadPath[i] = Path.Combine(path, txts[i]);
                    //아니 시발 모르겠다.
                    //이거 아닌데 ;; 
                }

            }
            else
            {
                for (int i = 0; i < txts.Length; i++)
                {
                    string path = Application.persistentDataPath;
                    path = path.Substring(0, path.LastIndexOf('/'));
                    ctLoadPath[i] = Path.Combine(path, txts[i]);
                }
            }
        }

        /*
        // ctLoadPath = txts;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < txts.Length; i++) {

                Path.Combine(Application.persistentDataPath, txts[i]);

                // Path.Combine(Application.persistentDataPath, txts[i]);
                /* byte[] byteTexture = System.IO.File.ReadAllBytes(Path.Combine(Application.persistentDataPath, txts[i]));
                 Path = System.IO.Path.Combine(Application.streamingAssetsPath, byteTexture);

                 if (byteTexture.Length > 0) 
                 {
                     texture = new Texture2D(0, 0);
                     texture.LoadImage(byteTexture);
                 }
            };
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < txts.Length; i++)
            {
                string path = txts[i];
                path = path.Substring(0, path.LastIndexOf('/'));
                Path.Combine(path, txts[i]);
                //아니 시발 모르겠다.
                //이거 아닌데 ;; 
            }

        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, "파일저장폴더/" + 파일이름);
        }


        for (int i = 0; i< txts.Length; i++)
        {
            string imgSavePath = Application.persistentDataPath + "/Cartoons/";


        }*/
    }

    /*void SystemIOFileLoad()
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(Path);
        //Path = System.IO.Path.Combine(Application.streamingAssetsPath, imgSavePat);

        if (byteTexture.Length > 0)
        {
            texture = new Texture2D(0, 0);
            texture.LoadImage(byteTexture);
        }
    }*/
    //이거 밑에 세개도 바꿔야하나봄
    public void SetImgZero(Sprite sprite)
    {
        imgZero.sprite = sprite;
    }
    public void SetImgOne(Sprite sprite)
    {
        imgOne.sprite = sprite;
    }
    public void SetImgTwo(Sprite sprite)
    {
        imgTwo.sprite = sprite;
    }

    //요 아래 세개가 괄호안의 텍스트를 스트라이프로 바꿔야함 
    public void SetImgfrLocalZero(string str)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(str);
        Texture2D texture = new Texture2D(0, 0);
        Rect rect = new Rect(0, 0, texture.width * 0.01f, texture.height * 0.01f);
        if (byteTexture.Length > 0)
        {
            imgZero.sprite = SpriteFromTexture2D(ScaleTexture(texture, 1000, 1000));
        }

        //imgZero.sprite = sp;
    }
    public void SetImgfrLocalOne(string str)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(str);
        Texture2D texture = new Texture2D(0, 0);
        Rect rect = new Rect(0, 0, texture.width * 0.02f, texture.height * 0.02f);
        if (byteTexture.Length > 0)
        {
            imgOne.sprite = SpriteFromTexture2D(ScaleTexture(texture, 100, 100));
        }
        //imgOne.sprite = sp;
    }
    public void SetImgfrLocalTwo(string str)
    {
        byte[] byteTexture = System.IO.File.ReadAllBytes(str);
        Texture2D texture = new Texture2D(0, 0);
        //Rect rect = new Rect(0, 0, texture.width, texture.height);
        Rect rect = new Rect(0, 0, texture.width * 0.02f, texture.height * 0.02f);

        if (byteTexture.Length > 0)
        {
            // imgTwo.sprite = SpriteFromTexture2D(ScaleTexture(texture, 2 , 2));
            imgTwo.sprite = SpriteFromTexture2D(ScaleTexture(texture, 100, 100));
        }
        //imgTwo.sprite = sp;
    }
    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f,
            texture.width, texture.height), new Vector2(imgZero.rectTransform.rect.width, imgZero.rectTransform.rect.height), 100.0f);
    }//imgZero.rectTransform.rect.width이부분 0.5f 엿음 

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        Debug.Log("아니 이 스케일 작동은 하는거?"); 
        return result;
    }

    public static Texture2D ResizeTexture(Texture2D source, Vector2 size)
    {
        //***getAllthe Source pixels

        Color[] aSourceColor = source.GetPixels(0);
        Vector2 vSourceSize = new Vector2(source.width, source.height);

        //Caculate New Size
        float xWidth = size.x;
        float xHeight = size.y;
        //Make New Texture2D
       Texture2D oNewTex = new Texture2D((int)xWidth, (int)xHeight, TextureFormat.RGBA32, false);

        //Makedestination array

        int xLength = (int)xWidth * (int)xHeight;
        Color[] aColor = new Color[xLength];

        Vector2 vPixelSize = new Vector2(vSourceSize.x / xWidth, vSourceSize.y / xHeight);

        //Loop  through destination pixels and process
        Vector2 vCenter = new Vector2();
        for(int ii = 0; ii < xLength;ii++)
        {
            //figure out x&y
            float xX = (float)ii % xWidth;
            float xY = Mathf.Floor((float)ii / xWidth);

            //CalculateCenter
            vCenter.x = (xX / xWidth) * vSourceSize.x;
            vCenter.y = (xY / xHeight) * vSourceSize.y;

            //Average
            //Calculate grid around point
            int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x-(vPixelSize.x*0.2f)), 0);
            int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.2f)));
            int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.2f)), 0);
            int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.2f)), vSourceSize.y);

            //Loop and accumulate
            Vector4 oColorTotal = new Vector4();
            Color oColorTemp = new Color();
            float xGridCount = 0;
            for(int iy = xYFrom; iy < xYTo; iy++)
            {
                for(int ix = xXFrom;ix<xXTo; ix++)
                {
                    //GetColor
                    oColorTemp += aSourceColor[(int)(((float)iy * vSourceSize.x) + ix)];

                    //Sum
                    xGridCount++;
                }
            }
            //AverageColor
            aColor[ii] = oColorTemp / (float)xGridCount;
    }
        //SetPixels
        oNewTex.SetPixels(aColor);
        oNewTex.Apply();

        //Returen
        return oNewTex;
    }
    void MolabyUnity()
    {
        //---
        // Set up a new texture with the same dimensions as the original.
        destTex = new Texture2D(sourceTex.width, sourceTex.height);
        destPix = new Color[destTex.width * destTex.height];

        // For each pixel in the destination texture...
        for (var y = 0; y < destTex.height; y++)
        {
            for (var x = 0; x < destTex.width; x++)
            {
                // Calculate the fraction of the way across the image
                // that this pixel positon corresponds to.
                float xFrac = x * 1.0f / (destTex.width - 1);
                float yFrac = y * 1.0f / (destTex.height - 1);

                // Take the fractions (0..1)and raise them to a power to apply
                // the distortion.
                float warpXFrac = Mathf.Pow(xFrac, warpFactor);
                float warpYFrac = Mathf.Pow(yFrac, warpFactor);

                // Get the non-integer pixel positions using GetPixelBilinear.
                destPix[y * destTex.width + x] = sourceTex.GetPixelBilinear(warpXFrac, warpYFrac);
            }
        }

        // Copy the pixel data to the destination texture and apply the change.
        destTex.SetPixels(destPix);
        destTex.Apply();

        // Set our object's texture to the newly warped image.
        GetComponent<Renderer>().material.mainTexture = destTex;
        //-- 요 위의 스타트  유니티 공식사이트에서 가져온 거
    }
}

