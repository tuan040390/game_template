using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static GameManager staticScript;

    [SerializeField]
    private SceneLoader _sceneLoader;
    public SceneLoader SceneLoader { get { return _sceneLoader; } }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        staticScript = this;
    }

    private void Start()
    {
        if(_sceneLoader == null)
        {
            _sceneLoader = GetComponentInChildren<SceneLoader>();
        }
    }

    public static void LoadSceneAsync(string sceneName)
    {
        staticScript._sceneLoader.LoadSceneAsync(sceneName);
    }
}
