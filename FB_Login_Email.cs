using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;

//using GooglePlayGames;
//using Google.basic;
//using GooglePlayGames.BasicApi
public class FB_Login_Email : MonoBehaviour
{
    // FirebaseAuth auth;
    public static Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;
    

    [Header("신규 로그인_이메일")]
    public InputField new_email_input;
    public InputField new_psswrd_input;
    public InputField new_psswrd_input_2;
    public Text signIn_Mssg;
    public GameObject signup_Panel;
    public Button signup_bttn;


    [Header("ㄱㅣ존 로그인_이메일")]
    public GameObject login_panel;
    public InputField user_email_input;
    public InputField user_passwrd_input;
    public Text loginMssg_txt;
   // private string user_newEmail;
   // private string user_passward;
    public Button loin_bttn;
    public Button signupbttn_inlogin;

    [Header("로그인 상태 체크 Bool")]
    public static bool isSignUponce;
    public static bool isLoginonce;
    public static bool isSignUp;
    public static bool isEmailVerified;
    public static bool islogoutd;
    private bool isSendEmailOnce;
    private bool isSignupFail;
    private bool isSignupCancled;
    private bool isloginFail;
    private bool isloginCancled;
    private bool isloginClickedOnce;
   
    [Header("설정창에서 표시될 유아이")]
    public Image option_lock;
    public Sprite option_lock_sp, option_unlock_sp;
    public Text option_text;
    public Button op_savebttn,op_loadbttn;
    public GameObject panel_AccountInfo;
    public GameObject panel_Login;
    public Transform panel_simpleMssg;
    public GameObject panel_simpleMssg_go;

    [Header("그 계정 인포에서 표시될 유아이")]
    public GameObject account_unlock_login;
    public GameObject account_locked_logout;
    //public GameObject accountInfo_go;
    //public GameObject accountInfo_logout_mssg_go;
    public Button account_save, account_load;
    public Text accountInfo_txt;



    SimpleMssgMan smm;//심플 메세지창.

    void Awake()
    {
        islogoutd = true;
        isSignUp = false;
        isEmailVerified = false;
        InitializeFirebase();
        Debug.Log("어웨잌 체인지   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }
    private void Start()
    {
        smm = gameObject.GetComponent<SimpleMssgMan>();
        AccountChck();
        Debug.Log("스타트   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

        //Debug.Log("유저값?" + auth + "그리고 이즈사인잉?  " + isSignUp +"ㅇㅣㅁㅔㅇㅣㄹ" +isEmailVerified);

    }
    private void Update()
    {
        if (auth == null)
        {
            islogoutd = true;
            isSignUp = false;
            isEmailVerified = false;
           Debug.Log("유저값?" + auth + "그리고 이즈사인잉?  " + isSignUp);
        }
        if (isloginClickedOnce)
        {
            loin_bttn.interactable = false;
            signupbttn_inlogin.interactable = false;
        }
        if (isSignUponce)
        {
            signup_bttn.interactable = false;
        }

        if(new_psswrd_input.text == new_psswrd_input_2.text && new_psswrd_input_2.text != "" && new_email_input.text != "")
        {
            signup_bttn.interactable = true;
            signIn_Mssg.text = "<b>[환생마리모] '가입하기'를 눌러주세용\n</b>";
        }
        else if(new_email_input.text == "")
        {
            signup_bttn.interactable = false;
            signIn_Mssg.text = "<b>[환생마리모] 신규 가입을 환영합니다.\n</b>"
                + "이메일 인증을 하셔야 하오니... \n"+ "꼭 사용하시는 이메일로 부탁드려요오오 ";
        }
        else if(new_psswrd_input_2.text == "")
        {
            signup_bttn.interactable = false;
            signIn_Mssg.text = "<b>[환생마리모] 신규 가입을 환영합니다.\n</b>"
                +"비밀번호는 영문,숫자 조합 6자리 이상으로 해주세요";
        }
        else
        {
            signup_bttn.interactable = false;
            signIn_Mssg.text = "입력한 비밀번호와 비밀번호 확인이 서로 일치하지 않습니다. 다시 확인해 주세요.";
        }
        
       
        AccountChck();
    }
    private void LateUpdate()
    {
        Login_Email_Helper();
        newSignup_Helper();
        if (!isloginClickedOnce)
        {
            LoginBttnvan();
        }
    }
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
        Debug.Log(" 온디스트로이?   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }


    void InitializeFirebase()
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
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                isSignUp = false;
                islogoutd = true;
                isEmailVerified = false;
                isLoginonce = false;
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                isSignUp = true;
                islogoutd = false;
                isEmailVerified = false;
                
                Debug.Log("Signed in " + user.UserId);
                if (user.IsEmailVerified)
                {
                    isEmailVerified = true;
                    Debug.Log("유저 이메일 확인됨" + user.Email);
                }
                else
                {
                    isLoginonce = true;
                }
            }
        }
        if(auth.CurrentUser == user && user!= null)
        {
            if (user.IsEmailVerified)
            {
                isEmailVerified = true;
                Debug.Log("유저 이메일 확인됨" + user.Email);
            }
            else
            {
                isLoginonce = true;
            }
        }
    }

    public void NewAccount()
    {
        Firebase.Auth.FirebaseUser newUser;
        if (!isSignUponce)
        {
            isSignUponce = true;
            if (new_psswrd_input.text == new_psswrd_input_2.text)
            {
                //패스워드 확인 넣자! if문으로 동일할때.. 하면 되겠지머 
                auth.CreateUserWithEmailAndPasswordAsync
                (new_email_input.text, new_psswrd_input.text).ContinueWith(task => {

                    if (task.IsCanceled)
                    {
                        isSignupCancled = true;
                        
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {

                        isSignupFail = true;
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        return;
                    }

                    // Firebase user has been created.
                    
                    isSignUp = true;
                    isSignUponce = true;
                        islogoutd = false;
                        isEmailVerified = false;

                        newUser = task.Result; //--이거 약간 이상하네.
                    user = newUser;
                    Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                        SendEmail();
                    //loginText.text = newUser.Email; //--여기 이거봐! 
                });

            }
            else
            {
                signIn_Mssg.text = "입력한 비밀번호와 비밀번호 확인이 서로 일치하지 않습니다. 다시 확인해 주세요.";
                new_psswrd_input.ActivateInputField();
            }
            Debug.Log(" 뉴어카운트.   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

        }

    }
    void newSignup_Helper()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
        FB_EmergeLogOut emo = gameObject.GetComponent<FB_EmergeLogOut>();

        if (isSignUponce)
        {
            isSignUponce = false;
            signup_Panel.SetActive(false);

            new_email_input.interactable = false;
            new_psswrd_input.interactable = false;
            new_psswrd_input_2.interactable = false;
            signup_bttn.interactable = false;

            new_email_input.text = "";
            new_psswrd_input.text = "";
            new_psswrd_input_2.text = "";

/*
            if (isEmailVerified)
            {
                fXController_ep1.SaiyanMarimoClickFX();
                smm.MakeSimpleMssg("로그인 확인", user.Email + "로 로그인 되셨습니다.", "확인");
                signup_Panel.SetActive(false);
                panel_simpleMssg_go.SetActive(true);
            }
            else
            {
                //fXController_ep1.CloseDirY();
                emo.EmergeLogout(user.Email);
                signup_Panel.SetActive(false);

                new_email_input.interactable = true;
                new_psswrd_input.interactable = true;
                new_psswrd_input_2.interactable = true;
                signup_bttn.interactable = true;
            }   */
        }
        if (isSignupCancled)
        {
            isSignupCancled = false;
            fXController_ep1.EndSFX();

            new_email_input.interactable = true;
            new_psswrd_input.interactable = true;
            new_psswrd_input_2.interactable = true;

            string isCancle = "신규 가입이 중단되었습니다.";

            signIn_Mssg.text = isCancle;
            smm.MakeSimpleMssg("가입 오류", isCancle, "확인");
            panel_simpleMssg_go.SetActive(true);

            isSignUponce = false;
            signup_bttn.interactable = true;
        }
        if (isSignupFail)
        {
            isSignupFail = false;
            fXController_ep1.EndSFX();

            new_email_input.interactable = true;
            new_psswrd_input.interactable = true;
            new_psswrd_input_2.interactable = true;

            string isFaulted = "이미 존재하는 계정, 혹은 잘못된 이메일입니다.";

            signIn_Mssg.text = isFaulted;
            smm.MakeSimpleMssg("가입 오류", isFaulted, "확인");
            panel_simpleMssg_go.SetActive(true);
            isSignUponce = false;
            signup_bttn.interactable = true;
        }

        if (isSendEmailOnce)
        {
            isSendEmailOnce = false;
            fXController_ep1.SaiyanMarimoClickFX();

            signIn_Mssg.text = user.Email + "로 가입 이메일이 전송되었습니다!!\n";
            signIn_Mssg.text += "스팸메일함도 한 번 확인해주세요.";
            smm.MakeSimpleMssg("이메일 인증 안내", user.Email + "로 가입 이메일이 전송되었습니다!!\n"+ "스팸메일함도 한 번 확인해주세요.", "확인");
            panel_simpleMssg_go.SetActive(true);
            Logout();
        }
      //  Debug.Log(" 사인업헬퍼 " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);
        AccountChck();
    }
    public void SendEmail()
    {
        user.SendEmailVerificationAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SendEmailVerificationAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Email sent successfully.");
            isSendEmailOnce = true;
         
        });

        /*if (newUser != null)
        {
            
        }*/
        Debug.Log("센드이메   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }

    public void EmailCheck()
    {
        InitializeFirebase();
        if (user == auth.CurrentUser && user != null)
        {
            if (!isloginClickedOnce)
            {
                isloginClickedOnce = true;
                if (user.IsEmailVerified)
                {
                    isEmailVerified = true;
                }
                else
                {
                    isEmailVerified = false;
                    Debug.Log("사인아웃으로 표시되느게 너니?!- 로그인 자체 ");


                    // Debug.Log("이메일 확인해 " + task.Exception);
                }
                isSignUp = true;
                isLoginonce = true;
                islogoutd = false;
            }
        }else
        {
            Debug.Log("이메일 체크 뭐이상한/ " ); ;
        }
    }
    public void LogIn_Email()
    {
        loin_bttn.interactable = false;
        if (!isloginClickedOnce)
        {
            isloginClickedOnce = true;
            auth.SignInWithEmailAndPasswordAsync(user_email_input.text, user_passwrd_input.text).ContinueWith(task => {

                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    isloginCancled = true;

                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                    isloginFail = true;
                    //여기 잘못된 비번 알림


                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;

                if (task.IsCompleted)
                {

                    if (newUser.IsEmailVerified)
                    {
                        isEmailVerified = true;

                        Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                        Debug.Log("이메일 로그인됨" + newUser.Email);

                        //--여기에 로그인 후 UI로 바뀌는 메서드 만들어서 넣어야
                    }
                    else
                    {
                        isEmailVerified = false;
                        Debug.Log("사인아웃으로 표시되느게 너니?!- 로그인 자체 ");


                        // Debug.Log("이메일 확인해 " + task.Exception);
                    }
                    isSignUp = true;
                    isLoginonce = true;
                    islogoutd = false;

                    Debug.Log("테스크 컴플리티드   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

                    return;
                }

            });

            //  Debug.Log("로긴이메일  " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);


        }

    }

    void Login_Email_Helper()
    {
        FXController fXController_ep1 = GameObject.Find("FXManager_Game").GetComponent<FXController>();
        FB_EmergeLogOut emo = gameObject.GetComponent<FB_EmergeLogOut>();

        if (isLoginonce)
        {
           
            isLoginonce = false;

            user_email_input.interactable = false;
            user_passwrd_input.interactable = false;
            loin_bttn.interactable = false;
            signupbttn_inlogin.interactable = false;

            if (isEmailVerified)
            {
                fXController_ep1.SaiyanMarimoClickFX();
                smm.MakeSimpleMssg("로그인 확인", user.Email + "로 로그인 되셨습니다.", "확인");
                login_panel.SetActive(false);
                panel_simpleMssg_go.SetActive(true);
            }
            else
            {
                fXController_ep1.CloseDirY();
                //emo.EmergeLogout(user.Email);
                login_panel.SetActive(false);
            }

        }

        if (isloginCancled)
        {
            isloginCancled = false;

            fXController_ep1.EndSFX();

            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            loginMssg_txt.text = "로그인 시도가 중단되었습니다.";
            smm.MakeSimpleMssg("로그인 오류", "로그인 시도가 중단되었습니다.", "확인");
            panel_simpleMssg_go.SetActive(true);

            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            user_email_input.ActivateInputField();
            isSignUp = false;
            isLoginonce = false;
            islogoutd = true;
            isloginClickedOnce = false;
            isEmailVerified = false;

        }
        if (isloginFail)
        {
            isloginFail = false;

            fXController_ep1.EndSFX();

            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            loginMssg_txt.text = "잘못된 이메일/비밀번호입니다.";
            smm.MakeSimpleMssg("로그인 오류", "잘못된 이메일/비밀번호입니다.", "확인");
            panel_simpleMssg_go.SetActive(true);

            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            user_email_input.ActivateInputField();
            isSignUp = false;
            isLoginonce = false;
            islogoutd = true;
            isloginClickedOnce = false;
            isEmailVerified = false;
        }
        //Debug.Log(" 로긴서포트   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);
        AccountChck();
     
    }

    void LoginBttnvan()
    {
        if (isloginClickedOnce)
        {
            loin_bttn.interactable = false;
            signupbttn_inlogin.interactable = false;
            user_email_input.interactable = false;
            user_passwrd_input.interactable = false;

        }
    }
    public void Logout()
    {
        Debug.Log("로그아웃됨? 진짜?" + user.Email);
        isSignUp = false;
        isloginClickedOnce = false;
        isEmailVerified = false;
        islogoutd = true;
        auth.SignOut();
        AccountChck();
        //Debug.Log("로그아웃  " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }

    void AccountChck() //--불값만 조절하자 -세이브 로드 버튼은 세이브로드파일에서 하고..
    {
        if (isSignUp)
        {
            if (isEmailVerified)
            {
                //option_text.fontSize = 45;
                option_text.text = "현재 로그인된 계정은  \n" + user.Email + "  입니다.";
                option_lock.sprite = option_unlock_sp;
           
                op_loadbttn.interactable = true;

                loin_bttn.interactable = false;
                signupbttn_inlogin.interactable = false;
                signup_bttn.interactable = false;

                account_load.interactable = true;
                account_locked_logout.SetActive(false);
                account_unlock_login.SetActive(true);
                accountInfo_txt.text = user.Email;

                loginMssg_txt.text = user.Email + "<size=40>로 \n  로그인 되었습니다!</size>";

            }
            else
            {
                account_save.interactable = account_load.interactable = false;
                account_locked_logout.SetActive(true);
                account_unlock_login.SetActive(false);
                accountInfo_txt.text = user.Email;
                accountInfo_txt.text += "\n<size=40> 이메일을 확인해 주세요</size>";

                option_lock.sprite = option_lock_sp;

                loin_bttn.interactable = false;
                signupbttn_inlogin.interactable = false;
                signup_bttn.interactable = false;

                option_text.fontSize = 40;
                option_text.text = user.Email + "<size=30>\n로 보내진 인증 메일 속 링크를 클릭해주세요.</size> \n";
                option_text.text += "<size=25> 스팸 메일함도 한 번 확인해주세요ㅠㅠ</size>";
                op_savebttn.interactable = false;
                op_loadbttn.interactable = false;

                loginMssg_txt.text = user.Email + "로 보내진 인증 메일 속 링크를 클릭해주세요.\n ";
                loginMssg_txt.text += "<size=40> 스팸메일함도 한 번 확인해주세요.</size>";
            }
        }
        else if (islogoutd)
        {
            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            signup_bttn.interactable = true;

            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            signup_bttn.interactable = true;


            account_locked_logout.SetActive(true);
            account_unlock_login.SetActive(false);
            account_save.interactable = account_load.interactable = false;

            op_savebttn.interactable = false;
            op_loadbttn.interactable = false;
            option_lock.sprite = option_lock_sp;

            option_text.text = "현재 연동된 계정이 없습니다.";

            loginMssg_txt.text = "현재 연동된 계정이 없습니다.";

            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;
        }
        else
        {
            loin_bttn.interactable = true;
            signupbttn_inlogin.interactable = true;
            signup_bttn.interactable = true;

            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;

            account_locked_logout.SetActive(true);
            account_unlock_login.SetActive(false);
            account_save.interactable = account_load.interactable = false;

            op_savebttn.interactable = false;
            op_loadbttn.interactable = false;
            option_lock.sprite = option_lock_sp;

            loginMssg_txt.text = "현재 연동된 계정이 없습니다.";

            option_text.text = "현재 연동된 계정이 없습니다.";

            user_email_input.interactable = true;
            user_passwrd_input.interactable = true;
        }

       // Debug.Log("어카운트 체크   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);
    }

    public void AccountBttn()
    {
        FB_EmergeLogOut emo = gameObject.GetComponent<FB_EmergeLogOut>();

        if (isSignUp && isEmailVerified)
        {
            panel_AccountInfo.SetActive(true);
            //Debug.Log("어카운트인포 열려!");

        }else if(isSignUp && !isEmailVerified)
        {
           emo.EmergeLogout(user.Email);

            //Debug.Log("임머전시 열려!");

        }
        else if (!isSignUp)
        {
            panel_Login.SetActive(true);
            //Debug.Log("로긴 열려!");

        }
        else
        {
            panel_Login.SetActive(true);
          //  Debug.Log("로긴 열려!");
        }
        //Debug.Log(" 어카운트버   " + "isSignup-" + isSignUp + "  isLoginonce -" + isLoginonce + "  isEmail....-" + isEmailVerified + "  islogout - " + islogoutd);

    }
}
