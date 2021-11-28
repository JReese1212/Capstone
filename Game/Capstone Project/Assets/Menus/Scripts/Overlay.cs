using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }
    }
}
