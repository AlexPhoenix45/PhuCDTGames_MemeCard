using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Slider")]
    public Slider slider;

    [Header("Scene to Load")]
    public string sceneToLoad;

    [Header("Wait Amount (Animation Wait)")]
    public float waitTime = 1f;

    public void Start()
    {
        IEnumerator startWait()
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(LoadLevelASync(sceneToLoad));
        }
        StartCoroutine(startWait());
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / .9f);
            slider.value = progressValue;
            yield return null;
        }
    }
}
