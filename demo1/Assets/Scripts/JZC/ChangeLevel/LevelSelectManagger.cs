using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LevelSelectManagger : MonoBehaviour
{
    [Header("Player属性")] 
    public GameObject player;

    public Vector3 _firstLoc;
    public Vector3 menuLoc;
    
    [Header("地图")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuLoadScene;
    private GameSceneSO currentLoadScene;
    private GameSceneSO _sceneSo;
    private Vector3 _posLoc;
    private bool _fadeScreen;
    private bool _isloading = false;
    
    public float fadeDuration;

    [Header("事件监听")]
    public VoidEventSO newGameSO;
    [Header("事件广播")]
    public SeceneLoadEventSO LoadEventSo;
    public VoidEventSO afterLoadedEvent;
    public FadeEventSO fadeEvent;
    
    private void Start()
    {
        LoadEventSo.RaiseLoadRequestEvent(menuLoadScene, menuLoc, true);
    }

    private void NewGame()
    {
        _sceneSo = firstLoadScene;
        LoadEventSo.RaiseLoadRequestEvent(_sceneSo, _firstLoc, true);
    }
    private void OnEnable()
    {
        LoadEventSo.LoadRequestEvent += OnLoadRequestEvent;
        newGameSO.OnEventRasied += NewGame;
    }
    private void OnDisable()
    {
        LoadEventSo.LoadRequestEvent -= OnLoadRequestEvent;
        newGameSO.OnEventRasied -= NewGame;
    }
    private void OnLoadRequestEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        if (_isloading) return;
        _isloading = true;
        _sceneSo = arg0;
        _posLoc = arg1;
        _fadeScreen = arg2;

        if(currentLoadScene != null) StartCoroutine(UnLoadPrevScene());
        else LoadNewScene();
    }

    private IEnumerator UnLoadPrevScene()
    {
        //player.SetActive(false);
        if (_fadeScreen)
        {
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);
        if (currentLoadScene != null)
        {
            yield return currentLoadScene.sceneRef.UnLoadScene();
        }
        
        LoadNewScene();
    }
    //加载场景
    private void LoadNewScene()
    {
        var loadingOption = _sceneSo.sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    //场景切换结束
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = _sceneSo;
        player.transform.position = _posLoc;
        if (_fadeScreen)
        {
            fadeEvent.FadeOut(fadeDuration);
        }
        //player.SetActive(true);
        player.GetComponent<PawnMove>().isDead = false;
        player.GetComponent<PlayerInfo>().InitialHP();
        _isloading = false;
        if(currentLoadScene.sceneType != SceneType.Main) afterLoadedEvent.RasiedEvent();
        
        //能力切换
        var playerbhv = player.GetComponent<PlayerBeheviour>();
        if (playerbhv && currentLoadScene.sceneType != SceneType.Main)
        {
            playerbhv.ChangeState();
            playerbhv.ChangeSpeed();
        }

    }
}
