using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuObject, CreditsMenuObject, GameMenu;
    private Stack<GameObject> _openedMenus = new();
    private newInputSystem _inputs;

    void Awake()
    {
        _inputs = new();
        _inputs.UI.Esc.performed += _ => OnBack();
    }

    void OnEnable()
    {
        _inputs.Enable();
    }

    void OnDisable()
    {
        _inputs.Disable();
    }

    void Start()
    {
        OpenMenu(StartMenuObject);
    }

    public void OpenMainGameMenu() => OpenMenu(GameMenu);

    public void OpenCreditsMenu() => OpenMenu(CreditsMenuObject);

    public void OnBack()
    {
        if (_openedMenus.Count <= 1)
            return;

        GameObject m_menu = _openedMenus.Pop();
        m_menu.SetActive(false);
        _openedMenus.Peek().SetActive(true);
    }

    private void OpenMenu(GameObject menu)
    {
        if (_openedMenus.Count >= 1)
            _openedMenus.Peek()?.SetActive(false);

        _openedMenus.Push(menu);
        _openedMenus.Peek().SetActive(true);
    }

    public void OnPlay()
    {
        AsyncOperation m_sceneLoad = SceneManager.LoadSceneAsync("Prototype");
        m_sceneLoad.completed += progress =>
        {
            GameEventBus.CutConnectionsBeforeSceneTransition();
            m_sceneLoad.allowSceneActivation = true;
        };
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
