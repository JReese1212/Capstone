using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LoginScene;
    [SerializeField] private InputField Email, Password;

    public static UserModel User;

    private volatile int _errorRequests;

    private void Start()
    {
        MainMenu.SetActive(false);
    }

    public void Login()
    {
        Debug.Log("Login");


        StartCoroutine(TryLogIn(Email.text, Password.text));


        
    }

    IEnumerator TryLogIn(string email, string password)
    {
        StartCoroutine(API.ValidateCredentials(email, password, state =>
        {
            switch (state.Status)
            {
                case -2:
                    Debug.Log("Error -2");
                    //Interlocked.Increment(ref _errorRequests);
                    //StartCoroutine(DisplayErrorMessage("Something went wrong."));
                    break;

                case -1:
                    Debug.Log("Error -1");
                    //Interlocked.Increment(ref _errorRequests);
                    //StartCoroutine(DisplayErrorMessage("Invalid username or password."));
                    break;

                case 1:
                    User = state.User;
                    Debug.Log("SUCCESSFUL LOGING");
                    LoginScene.SetActive(false);
                    MainMenu.SetActive(true);
                    break;
            }
        }));
        yield return null;
    }


    public void Register()
    {
        Application.OpenURL("http://3.22.125.44/Website/register.php");
    }
}
