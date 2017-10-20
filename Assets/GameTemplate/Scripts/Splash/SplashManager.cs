using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickStart()
    {
        GameManager.LoadSceneAsync("Menu");
    }
}
