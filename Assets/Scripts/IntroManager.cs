using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject introObject;
    public GameObject characterObject; 

    void Start()
    {
        
        introObject.SetActive(true);
        characterObject.SetActive(false);
    }

    
    public void EndIntro()
    {
        introObject.SetActive(false);
        characterObject.SetActive(true);
    }
}
