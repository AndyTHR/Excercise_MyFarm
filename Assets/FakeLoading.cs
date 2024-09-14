using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    public Button buttonLoadDone;
    void Start()
    {
        buttonLoadDone.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("PlayScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
