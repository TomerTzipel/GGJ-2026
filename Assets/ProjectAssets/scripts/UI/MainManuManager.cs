using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuManager : MonoBehaviour
{
    [SerializeField] private Button button;
    
    public GameObject MainMenuFrame;
    public GameObject SettingsFrame;
    public void StartButton()
    {
        SceneManager.LoadScene("Game Scene");
    }
    
    public void SettingsButton(bool isSettingActive)
    {
        MainMenuFrame.SetActive(!isSettingActive);
        SettingsFrame.SetActive(isSettingActive);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
