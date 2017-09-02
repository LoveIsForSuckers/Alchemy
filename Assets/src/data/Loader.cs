using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public const string SCENE_ID = "MainScene";
    public const string API_URL = "";

    [SerializeField]
    private Texture2D _loadBarEmpty;
    [SerializeField]
    private Texture2D _loadBarFull;

    private AsyncOperation _sceneLoader;
    private WWW _libraryLoader;

    // IEnumerator so this becomes a coroutine thing
    IEnumerator Start ()
    {
        yield return loadLibrary();
        yield return loadScene();
	}

    private IEnumerator loadLibrary()
    {
        // TODO: ADD OPTION TO LOAD LOCAL LIBRARY FROM FILE
        _libraryLoader = new WWW(API_URL);
        yield return _libraryLoader;
        Debug.Log("Loaded library");
        new Library(_libraryLoader.text);
    }

    private IEnumerator loadScene()
    {
        _sceneLoader = SceneManager.LoadSceneAsync(SCENE_ID);
        yield return _sceneLoader;
        Debug.Log("Loaded scene");
    }
}
