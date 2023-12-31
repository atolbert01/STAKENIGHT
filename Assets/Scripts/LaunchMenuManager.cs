using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class LaunchMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private GameObject _hostMenu;

    [SerializeField]
    private GameObject _joinMenu;

    [SerializeField]
    private GameObject _optionsMenu;

    [SerializeField]
    private SessionManager _sessionManager;

    private Stack<GameObject> _visitedMenus;

    private GameObject _currentMenu;

    public delegate void OnGameStarted();
    public OnGameStarted GameStarted;

    public void Start()
    {
        _visitedMenus = new Stack<GameObject>();
        _currentMenu = _mainMenu;
    }

    public void OnHostClicked()
    {
        Debug.Log("Host Button Clicked");
        _currentMenu.SetActive(false);
        _hostMenu.SetActive(true);
        _visitedMenus.Push(_hostMenu);
        _currentMenu = _hostMenu;
    }

    public void OnStartHostClicked()
    {
        //SceneManager.LoadScene("TestScene");
        //NetworkManager.Singleton.StartHost();

        _sessionManager.HostMode = true;
        GameStarted?.Invoke();
        SceneManager.LoadScene("TestScene");
    }

    public void OnStartClientClicked()
    {
        //SceneManager.LoadScene("TestScene");
        //NetworkManager.Singleton.StartHost();
        _sessionManager.HostMode = false;
        GameStarted?.Invoke();
        SceneManager.LoadScene("TestScene");
    }

    public void OnJoinClicked()
    {
        Debug.Log("Join Button Clicked");
        _currentMenu.SetActive(false);
        _joinMenu.SetActive(true);
        _visitedMenus.Push(_joinMenu);
        _currentMenu = _joinMenu;
    }

    public void OnOptionsClicked()
    {
        Debug.Log("Options Button Clicked");
        _currentMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _visitedMenus.Push(_optionsMenu);
        _currentMenu = _optionsMenu;
    }

    public void OnBackButtonClicked() 
    {
        if (_visitedMenus.Count > 1)
        {
            _currentMenu.SetActive(false);
            _visitedMenus.Pop();
            _currentMenu = _visitedMenus.Peek();
            _currentMenu.SetActive(true);
        }
        else
        {
            _currentMenu.SetActive(false);
            _visitedMenus.Pop();
            _mainMenu.SetActive(true);
            _currentMenu = _mainMenu;
        }
    }

    public void OnExitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
