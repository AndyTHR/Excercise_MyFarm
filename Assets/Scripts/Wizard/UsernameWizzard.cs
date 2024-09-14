using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UsernameWizzard : MonoBehaviour
{
    public Text Username;
    public Text gold;
    public Text diamond;
    public GameObject usernameWizzard;
    public InputField ipUsername;
    public Button ButtonOk;
    private FirebaseDatabaseManager databaseManager;
    // Start is called before the first frame update
    void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        if (LoadDataManager.userInGame.name == "")
        {
            usernameWizzard.SetActive(true);
        }
        else
        {
            Username.text = LoadDataManager.userInGame.name;
            
        }
        gold.text = "Gold: " +  LoadDataManager.userInGame.Gold.ToString();
        diamond.text = "Diamond: " +  LoadDataManager.userInGame.Diamond.ToString();
        ButtonOk.onClick.AddListener(SetNewUsername);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNewUsername()
    {
        if(ipUsername.text != "")
        {
            LoadDataManager.userInGame.name = ipUsername.text;
            databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
            Username.text = ipUsername.text;
            usernameWizzard.SetActive(false);
        }     
    }    
}
