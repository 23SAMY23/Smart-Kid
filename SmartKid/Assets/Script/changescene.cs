using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class changescene : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public Text progressText;
    // Start is called before the first frame update
    public void LoadLevel(int sceneIndex)
    {
        
        StartCoroutine(LoadYourAsyncScene(sceneIndex)); //change scene from 0 to 1 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onclick()
    {
        
    }
    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); //get the loading time
        loadingscreen.SetActive(true); // active the loading screen
        while (!operation.isDone) //while loop for wait until the next scene is loaded then change the scene
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); //get the time of scen change
            slider.value = progress;//change the slider value accordingly
            progressText.text = progress * 100f + "%";
            yield return null;

        }
    }
    }
