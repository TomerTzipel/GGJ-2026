using UnityEditor;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private VoidEventChannel playerDeath;
    [SerializeField] private GameObject pauseMenuUI;

    private void OnEnable()
    {
        playerDeath.OnEvent += OnPlayerDeath;
    }
    private void OnDisable()
    {
        playerDeath.OnEvent -= OnPlayerDeath;
    }
    public void OnPlayerDeath()
    {
        pauseMenuUI.SetActive(true);
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}