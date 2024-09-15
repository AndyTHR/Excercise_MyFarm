using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject progressBar;
    public Text textpercent;
    private float fixedLoadingTime = 3f;
    public static string NEXTSCENE = "Play Scene";

    public IEnumerator LoadingSceneAsync (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.GetComponent<Image>().fillAmount = progress;
            textpercent.text = (progress * 100).ToString("0") + "%";
            yield return null;
        }
    }
    private void Start()
    {
        StartCoroutine(LoadSceneFixedTime(NEXTSCENE));
    }

    public IEnumerator LoadSceneFixedTime (string sceneName)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fixedLoadingTime)
        {
            float progress = Mathf.Clamp01(elapsedTime/fixedLoadingTime);
            progressBar.GetComponent <Image>().fillAmount = progress;
            textpercent.text = (progress * 100).ToString("0") + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
