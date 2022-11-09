using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LoadManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] private Slider progressBar;
    private static LoadManager instance;
    public static LoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                LoadManager manager = FindObjectOfType<LoadManager>();
                if (manager != null)
                {
                    instance = manager;
                }
                else
                {
                    instance = new LoadManager();
                }
            }
            return instance;
        }
        set { instance = value; }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        
    }


    IEnumerator LoadSceneRoutine()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            if (operation.progress < 0.98f)
                progressBar.value = operation.progress;
            else
            {
                yield return new WaitForSeconds(1.5f);
                progressBar.value = operation.progress;
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }
        }
    }



}
