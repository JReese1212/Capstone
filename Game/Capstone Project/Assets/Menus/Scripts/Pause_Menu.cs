using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject mainMenu;
    public GameObject Cam;

    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject WorldHandle;

    private WorldHandler World;

    private void Start()
    {
        World = WorldHandle.GetComponent(typeof(WorldHandler)) as WorldHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        World.DestroyWorld();

        Cam.SetActive(true);
        gameObject.SetActive(false);
        Background.SetActive(true);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToSettings()
    {
        SettingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
