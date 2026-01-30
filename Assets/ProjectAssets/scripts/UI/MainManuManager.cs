using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image frame;

    public void StartButton()
    {
        SceneManager.LoadScene("Game Scene");
    }
    
    public void SettingsButton()
    {
        frame.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
