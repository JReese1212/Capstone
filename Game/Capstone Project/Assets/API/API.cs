using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using CryptSharp;

public class API : MonoBehaviour
{
    private static readonly string BASE_URL = "http://3.22.125.44:8000/api";
    private static readonly string USERS = "/Logins";
    private static readonly string NAME = "/name";
    private static readonly string EMAIL = "/email";
    private static readonly string SCORES = "/Scores";

    public static IEnumerator ValidateCredentials(string email, string passwd, Action<LoginInfo> completed)
    {
        var url = $"{BASE_URL}{USERS}{EMAIL}/{email}";
        var request = UnityWebRequest.Get(url);
        var info = new LoginInfo();

        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Something went wroing, returned error: " + request.error);
            info.Status = -2;
            completed(info);
        }
        else
        {
            Debug.Log("user json: " + request.downloadHandler.text);

            if (request.responseCode == 401) // an occasional unauthorized error
            {
                Debug.Log("Error 401: Unauthorized");
                info.Status = -2;
            }
            else if (request.responseCode == 404)
            {
                Debug.Log("Not found, invalid username");
                info.Status = -1;
            }

            else if (request.result != UnityWebRequest.Result.ProtocolError)
            {
                var newUser = JsonUtility.FromJson<UserModel>(request.downloadHandler.text);
                Debug.Log("deserialized json: " + newUser.username);
                info.User = newUser;
                bool matches = Crypter.CheckPassword(passwd, newUser.pass);
                if (matches == true)
                {
                    //login successfull
                    Debug.Log("Login Successful! Welcome " + newUser.username);
                    info.Status = 1;
                }
                else
                {
                    //login unsuccessful
                    Debug.Log("Wrong password! you entered: " + passwd + " .the correct password was: " + newUser.pass);
                    info.Status = -1;
                }
            }
            completed(info);
        }
    }
    
    public static IEnumerator GetAccount(string email, Action<LoginInfo> completed)
    {
        var url = $"{BASE_URL}{USERS}{EMAIL}/{email}";
        var request = UnityWebRequest.Get(url);
        var info = new LoginInfo();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Something went wroing, returned error: " + request.error);
            info.Status = -2;
            completed(info);
        }
        else
        {
            // Show results as text
            Debug.Log("user json: " + request.downloadHandler.text);

            if (request.responseCode == 404)
            {
                Debug.Log("Not found, invalid username");
                info.Status = -1;
            }

            else if (request.result != UnityWebRequest.Result.ProtocolError)
            {
                var newUser = JsonUtility.FromJson<UserModel>(request.downloadHandler.text);
                info.User = newUser;
                info.Status = 1;
            }
            completed(info);
        }
    }

    public static IEnumerator GetScore(string username, Action<ScoreModel> completed)
    {
        var url = $"{BASE_URL}{SCORES}{NAME}/{username}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Something went wrong, and returned error: " + request.error);
            completed(null);
        }
        else
        {
            Debug.Log("Request finished successfully!");
            var model = JsonUtility.FromJson<ScoreModel>(request.downloadHandler.text);
            completed(model);
        }

    }


    public static IEnumerator CreateScore(ScoreModel score, Action<bool> completed)
    {
        var url = $"{BASE_URL}{SCORES}";
        var json = JsonUtility.ToJson(score);

        Debug.Log(json);

        var data = System.Text.Encoding.UTF8.GetBytes(json);

        var request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(data);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Something went wrong, and returned error: " + request.error);
            completed(false);
        }
        else
        {
            Debug.Log("Request finished successfully!");
            completed(true);
        }

    }

    public static IEnumerator UpdateScore(long userid, ScoreModel score, Action<int> completed)
    {
        var url = $"{BASE_URL}{SCORES}/{userid}";

        var json = JsonUtility.ToJson(score);

        var data = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = UnityWebRequest.Put(url, data);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Something went wrong, and returned error: " + request.error);
            completed(-1);
        }
        else
        {
            Debug.Log("Request finished successfully!");
            completed(1);
        }
    }



    public class LoginInfo
    {
        public int Status;
        public UserModel User;
    }
}
