using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PauseMenu;
    [SerializeField] private GameObject WorldHandle;
    private WorldHandler World;

    public void goBack()
    {
        World = WorldHandle.GetComponent(typeof(WorldHandler)) as WorldHandler;

        if (World.isActive())
        {
            gameObject.SetActive(false);
            PauseMenu.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            MainMenu.SetActive(true);
        }

    }
}

