using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadManager : MonoBehaviour
{

    [SerializeField] private Slider progressBar;

    public static string nextScene;

    private void Start()
    {

        StartCoroutine(LoadSceneRoutine(nextScene));


    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        operation.allowSceneActivation = false;
        while(true)
        {
            progressBar.value = operation.progress + 0.08f;
            yield return new WaitForSeconds(0.5f);
            if(progressBar.value >= 0.9f)
            {
                yield return new WaitForSeconds(3f);
                operation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
