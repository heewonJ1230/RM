using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class FB_ImageLoader : MonoBehaviour //--세이버도 통합한다.
{
    public RawImage[] rawIms;
    public RawImage rawimg;

    public RectTransform tran_rawimg;
    public Vector2 vec_rawimg;
    public GameObject rawImage_pref;


    FirebaseStorage storage;
    StorageReference cartoonReference;
    private Animator anim;
    private bool isOpened;
    public GameObject loading_panel;

    public Text load_title;
    public GameObject shrimpGo;

    [Header("유저에게 안보이는 정보")]
    public int hwaNum;
    public int pageCount;

    string imgSavePath;
   
    List<string> local_savePaths = new List<string>();
    int downloadingPage;
    bool isReadytoGetPage;
    CartoonReplayData crd;
    StorageReference ct_image;
    PageTurning pageTurning;
    CartoonManager cm;

    public static Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;


    private void Awake()
    {
        InitializeFirebase();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadasObject(); //-- 여기
        crd  = GameObject.Find("CartoonNHcho").GetComponent<CartoonReplayData>();
        cm = GameObject.Find("CartoonNHcho").GetComponent<CartoonManager>();

       
        anim = shrimpGo.GetComponent<Animator>();
        imgSavePath = Application.persistentDataPath + "/Cartoons/";

        isOpened = false;
        downloadingPage = 0;
     
        storage = FirebaseStorage.DefaultInstance;
        cartoonReference = storage.GetReferenceFromUrl("gs://rm-rel.appspot.com/Cartoons/");

        if (null != GameObject.Find("Panel_Webtoon"))
        {   //--GameManager에서 되서 넘어옴 그래서 여기에 ShrimpGo 또 불러줄 필요 없음
            pageTurning = GameObject.Find("Panel_Webtoon").GetComponent<PageTurning>();
          //  Chawoogi();
        }
    }
    private void Update()
    {
        if(null!= GameObject.Find("GameManager"))
        {
            LoadCartoon();
        }

        if (null != GameObject.Find("Panel_Webtoon"))
        {
            pageTurning = GameObject.Find("Panel_Webtoon").GetComponent<PageTurning>();
           // Chawoogi();
            if (isReadytoGetPage)
            {
                pageTurning.GetPage();
                isReadytoGetPage = false;
            }
        }
        else if (isOpened)
        {
//            anim.SetBool("MoveCheck", true);
        }
    }

    //로우 이미지 복붙  되는지 알아보기 - 내가 할 수 없어!
    //프리팹으로 로우 이미지 해보기
    /* void Chawoogi() //로우 이미지 게임 오브젝트 연결
     {
         rawimg = GameObject.FindGameObjectWithTag("RawImage").GetComponent<RawImage>();
         tran_rawimg = rawimg.rectTransform;
         RawImgMaker();
     }

     void RawImgMaker() //로우 이미지 복제 
     {
         GameObject rawimg_new =
         Instantiate(rawImage_pref, tran_rawimg.position, Quaternion.identity);
         rawimg_new.transform.parent = rawimg.transform;

     }
    */

    void InitializeFirebase() //--이거 없어서 그런가 하
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        // AccountChck();
        //Debug.Log("이니셜라이즈파이어베이  " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
        }
    }
    //--- 오브젝트로 불러오는 방식
    public List<object> fObj = new List<object>();
    void LoadasObject()
    {
        string[] fName;
        //--뭐 가 맞는 거야?
       /* UnityEngine.Object[] fObj;
        fName = Directory.GetFiles(Application.persistentDataPath + "Cartoons" + " / " + hwaNum + " / ", " *.psd");

        for (int i = 0; i < fName.Length; i++)
            UnityEngine.Object fObj[i] = AssetDatabase.LoadAssetAtPath(fName[i], typeof(UnityEngine.Object));
       */

       // object[] fObj;
       if(hwaNum != 0)
        {
            fName = Directory.GetFiles(Application.persistentDataPath + "Cartoons" + " / " + hwaNum + " / ", " *.psd");

            for (int i = 0; i < fName.Length; i++)
            {

                fObj.Add(AssetDatabase.LoadAssetAtPath(fName[i], typeof(object)));
                Debug.Log(fObj.Count);
            }

        }
    }




    public void ShrimpGo(int _hwaNum, int _pageCount) //그리고 세이브로드도너가 해!
    {
        hwaNum = _hwaNum;
        pageCount = _pageCount;
        isOpened = true;
        loading_panel.SetActive(true);
        Debug.Log("쉬림프고 ! 이거 되는거임?" + " 화넘  " + hwaNum + "  페이지카운트" +   pageCount );
        CheckCTImages();
        //--쎄이브 기능. 폴더 만들었으니 이제 장수대로 가져오기
        // Directory.CreateDirectory(imgSavePath + hwaNum);

    }
   
    //-- 장수대로 가져오기 체크 카툰 이미지스 & 폴더 만들기 
     void CheckCTImages()
    {
     
        //폴더 만들기
        if (!System.IO.File.Exists(imgSavePath + hwaNum.ToString() + "/" + hwaNum.ToString() + "_" + (pageCount-1).ToString()+".png"))
        {
            Directory.CreateDirectory(imgSavePath + hwaNum.ToString());
        }
        cartoonReference = storage.GetReferenceFromUrl("gs://rm-rel.appspot.com/Cartoons/");

        StartCoroutine(MakingPath());
       
    }
    //-- 버튼 누르면 그 패널 켜지고 anim.SetBool("MoveCheck",true); 이거 해주기
    //-- 다운로드 포문

    IEnumerator MakingPath()
    {
        string ct_path = "Cartoons" + "/" + hwaNum + "/" ;

        Debug.Log("다운로드 이미지 진입은 되나?");
       if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < pageCount; ++i)
            {
                isReadytoGetPage = false;
                //cartoonReference = cartoonReference.Child(hwaNum + "/");
                ct_image = cartoonReference.Child(hwaNum + "/" + hwaNum + "_" + i.ToString() + ".png");

                downloadingPage = i;
                string filename = hwaNum.ToString() + "_" + i.ToString() + ".png";
                local_savePaths.Add(Path.Combine(Application.persistentDataPath, ct_path + filename));
                DownloadImgs();
              
                yield return new WaitForSeconds(0.1f);
            }
        }
       else if(Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < pageCount; ++i)
            {
                isReadytoGetPage = false;
                //cartoonReference = cartoonReference.Child(hwaNum + "/");
                ct_image = cartoonReference.Child(hwaNum + "/" + hwaNum + "_" + i.ToString() + ".png");

                downloadingPage = i;
                string filename = hwaNum.ToString() + "_" + i.ToString() + ".png";

                string path = Application.persistentDataPath + ct_path;
                path = path.Substring(0, path.LastIndexOf('/'));
                local_savePaths.Add(Path.Combine(path, filename));
                DownloadImgs();
                Debug.LogError("너 안드로이드야?/");
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            /*for (int i = 0; i < pageCount; ++i)
            {
                downloadingPage = i;
                string filename = hwaNum.ToString() + "_" + i.ToString() + ".png";

                string path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                local_savePaths.Add(Path.Combine(path, ct_path + filename));

                DownloadImgs();
                yield return new WaitForSeconds(0.1f);
            }
            */
            for (int i = 0; i < pageCount; ++i)//어? 이거 뭐야? 
            {
                isReadytoGetPage = false;
                downloadingPage = i;

                local_savePaths.Add(imgSavePath + "/" + hwaNum + "/" + hwaNum.ToString() + "_" + i.ToString() + ".png");
                //--너 이거 왜이랬냐? 언제 이랬냐 ? ct_image = cartoonReference.Child(hwaNum + "/" + hwaNum + "_" + downloadingPage.ToString() + ".png");
                // cartoonReference = cartoonReference.Child(hwaNum + "/");

                ct_image = cartoonReference.Child(hwaNum+ "/"+ hwaNum +"_" +i.ToString() + ".png");

                Debug.LogError("local_savePath  " + local_savePaths[downloadingPage]); // 이게 왜 주르륵 나오는지 이해불[
                Debug.LogError("아래위로 같아야/");
                Debug.Log("불러오는 파이어베이스파일 " + hwaNum + "/" + hwaNum + "_" + i.ToString() + ".png");

                DownloadImgs();
                yield return new WaitForSeconds(0.1f);

            }
           
        }
     

      
    }
    void DownloadImgs()
    {
        ct_image.GetFileAsync(local_savePaths[downloadingPage]).ContinueWith(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                if(downloadingPage >= (pageCount - 1))
                {
                                       Debug.Log("다 다운되었나?");
                    Debug.Log("다운로드 완료 " + hwaNum + "_" + downloadingPage.ToString());

                    if(downloadingPage ==(pageCount-1))
                        isReadytoGetPage = true;
                }
            }
            else
            {
                if(downloadingPage >= (pageCount - 1))
                {  
                    Debug.Log(task.Exception + " " + hwaNum + "_" + downloadingPage.ToString());
                    Debug.Log("뭘다운 받았냐 ? " + local_savePaths[downloadingPage]);
                    print("실패  Download URL: " + task.Exception + downloadingPage.ToString());
                    // 
                }
            }
        });


    }

    public void LoadCartoon()
    {
        if (isReadytoGetPage)
        {
            Debug.Log("다운로드 완료 " + "카툰씬간다! ");

            LoadingManager.LoadScene("2Cartoon");

        }
    }

}
