using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private bool loadingScene = false;
    private bool fadingIn = false;
    private bool fadingOut = false;
    private float startingFadeTime = 0f;

    [SerializeField]
    private GameObject _cameraTopObj;
    [SerializeField]
    private Renderer _loadingBGObj;

    // Updates once per frame
    void Update()
    {
        // If the new scene has started loading...
        if (loadingScene == true)
        {
            if (fadingIn)
            {
                if(startingFadeTime <= 1f)
                {
                    startingFadeTime += Time.deltaTime;
                    UpdateCutoffLoadingBG(Mathf.Lerp(0f, 1f, Mathf.Min(startingFadeTime, 1f)));
                }
                else
                {
                    fadingIn = false;
                    startingFadeTime = 0f;
                }
            }

            if (fadingOut)
            {
                if (startingFadeTime <= 1f)
                {
                    startingFadeTime += Time.deltaTime;
                    UpdateCutoffLoadingBG(Mathf.Lerp(1f, 0f, Mathf.Min(startingFadeTime, 1f)));
                }
                else
                {
                    _cameraTopObj.SetActive(false);
                    fadingOut = false;
                    startingFadeTime = 0f;
                }
            }
        }
    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator IE_LoadSceneAsync(string sceneName)
    {
        loadingScene = true;
        // This line waits for 3 seconds before executing the next line in the coroutine.
        FadeIn();
        yield return new WaitForSeconds(1);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
        FadeOut();
        yield return new WaitForSeconds(1);
        loadingScene = false;

    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(IE_LoadSceneAsync(sceneName));
    }

    void FadeIn()
    {
        if (_cameraTopObj != null)
            _cameraTopObj.SetActive(true);

        UpdateCutoffLoadingBG(0f);
        startingFadeTime = 0;
        fadingIn = true;
    }
    
    void FadeOut()
    {
        if (_cameraTopObj != null)
            _cameraTopObj.SetActive(true);

        UpdateCutoffLoadingBG(1f);
        startingFadeTime = 0;
        fadingOut = true;
    }

    void UpdateCutoffLoadingBG(float _cutoff)
    {
        MaterialPropertyBlock _block = new MaterialPropertyBlock();
        _block.SetFloat("_Cutoff", _cutoff);
        _loadingBGObj.SetPropertyBlock(_block);
    }
}
