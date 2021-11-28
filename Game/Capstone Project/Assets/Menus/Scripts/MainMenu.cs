using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject WorldHandle;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject Overlay;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject HealthBar;

    private WorldHandler World;

    private void Start()
    {
        World = WorldHandle.GetComponent(typeof(WorldHandler)) as WorldHandler;
    }

    public void PlayGame()
    {
        World.BuildWorld();
        Cam.SetActive(false);
        Background.SetActive(false);
        Overlay.SetActive(true);
        gameObject.SetActive(false);
        //HealthBar.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("quitting...");
        Application.Quit();
    }
    public void ViewHighScores()
    {
        Application.OpenURL("http://3.22.125.44/Website/highscores.php");
    }

    public void ContactUsLink()
    {
        Application.OpenURL("http://3.22.125.44/Website/contact.php");
    }

    public void GoToSettings()
    {
        SettingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
