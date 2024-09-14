using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireBaseLoginManager : MonoBehaviour
{
    [Header("Đăng Kí")]
    public InputField ipRegisterEmail;
    public InputField ipRegisterPassword;
    public Button buttonRegister;




    [Header("Đăng Nhập")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;
    public Button buttonLogin;


    [Header("Chuyển đổi giữa đăng kí và đăng nhập")]
    public Button buttonMoveToSignIn;
    public Button buttonMoveToRegister;

    public GameObject loginForm;
    public GameObject registerForm;


    private FirebaseDatabaseManager databaseManager;

    private FirebaseAuth auth;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        buttonRegister.onClick.AddListener(RegisterAccountFirebase);
        buttonLogin.onClick.AddListener(SigninAccountWithFirebase);
        buttonMoveToRegister.onClick.AddListener(switchForm);
        buttonMoveToSignIn.onClick.AddListener(switchForm);
        databaseManager = GetComponent<FirebaseDatabaseManager>();
    }

    public void RegisterAccountFirebase()
    {
        string email = ipRegisterEmail.text;
        string password = ipRegisterPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Dang ki bi huy");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Dang ki that bai");
            }
            if (task.IsCompleted)
            {
                Debug.Log("Dang ki thanh cong");
                Map mapInGame = new Map();
                User userInGame = new User("",100, 50,mapInGame);
                FirebaseUser FirebaseUser = task.Result.User;
                databaseManager.WriteDatabase("Users/" + FirebaseUser.UserId, userInGame.ToString());
                SceneManager.LoadScene("FakeLoading");
            }

        });
    }
   
    public void SigninAccountWithFirebase()
    {
        string email = ipLoginEmail.text;
        string password = ipLoginPassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Dang Nhap bi huy");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Dang nhap that bai");
            }
            if (task.IsCompleted)
            {
                Debug.Log("Dang nhap thanh cong");

                SceneManager.LoadScene("FakeLoading");
            }
        });
    }

    public void switchForm()
    {
        loginForm.SetActive(!loginForm.activeSelf);
        registerForm.SetActive(!registerForm.activeSelf);    
    }    
}
