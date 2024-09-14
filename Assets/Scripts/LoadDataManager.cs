using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;

public class LoadDataManager : MonoBehaviour
{
    public static FirebaseUser firebaseUser;
    public static User userInGame;

    private DatabaseReference reference;

    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
        GetUserInGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUserInGame()
    {
        reference.Child("Users").Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread (task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Doc du lieu thanh cong: " + snapshot.Value.ToString());
                userInGame = JsonConvert.DeserializeObject<User>(snapshot.Value.ToString());
            }
            else
            {
                Debug.Log("Ghi du lieu that bai! " + task.Exception);
            }
        });
    }    
}
