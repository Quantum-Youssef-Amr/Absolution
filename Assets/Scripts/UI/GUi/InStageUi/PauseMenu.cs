using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuObject, LoseMenu;

    private newInputSystem _inputs;
    void Awake()
    {
        _inputs = new();
        _inputs.UI.Esc.performed += _ => HidePauseMenu();
        GameEventBus.OnGameLoss += () => LoseGame();
    }

    private void LoseGame()
    {
        LoseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        QuitToMainMenu();
    }

    void OnEnable()
    {
        _inputs.Enable();
    }

    void OnDisable()
    {
        _inputs.Disable();
    }

    public void HidePauseMenu()
    {
        PauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowPauseMenu()
    {
        PauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitToMainMenu()
    {
        AsyncOperation m_sceneLoading = SceneManager.LoadSceneAsync("Prototype_MainMenu");
        m_sceneLoading.completed += (progress) =>
        {
            GameEventBus.CutConnectionsBeforeSceneTransition();
            m_sceneLoading.allowSceneActivation = true;
        };
    }

}
