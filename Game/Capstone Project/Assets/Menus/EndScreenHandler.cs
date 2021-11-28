using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenHandler : MonoBehaviour
{
    [SerializeField] private GameObject WorldHandle;
    [SerializeField] private GameObject Overlay;
    public Text ScoreText;
    public Text HighScoreText;
    int tmp = 0;
    int finalScore = 0;
 
    

    public static ScoreModel Score;

    private WorldHandler World;


    public void PlayGame()
    {
        World = WorldHandle.GetComponent(typeof(WorldHandler)) as WorldHandler;


        World.DestroyWorld();
        World.BuildWorld();
        Overlay.SetActive(true);
        gameObject.SetActive(false);
        //HealthBar.SetActive(true);
        tmp = 0;
    }

    public void Quit()
    {
        Debug.Log("quitting...");
        Application.Quit();
    }

    public void DisplayScore()
    {
        if(tmp == 0)
        {
            tmp += 1;
            string username;
            username = LoginMenu.User.username;
            float s = GameObject.Find("World Generator").GetComponent<WorldHandler>().totalDistance;
            finalScore = (int)s;

            ScoreText.text = "Score: " + finalScore.ToString();
            

           

            

            StartCoroutine(TryGetScore(username));
            
            
        }
    }

    IEnumerator TryGetScore(string username)
    {
        StartCoroutine(API.GetScore(username, sModel =>
        {
            if(sModel == null)
            {
                Debug.Log("User has no score recorded");
                return;
            }
            else
            {
                Score = sModel;
                //Debug.Log(Score.highscore);

                // this list of if statements decides if the finalScore the player got needs to be inserted into their top 10 scores in the database
                if(finalScore == 0)
                {
                    return;
                }
                else if (finalScore > Score.highscore)
                {
                    Debug.Log(Score.userid);
                    Score.highscore = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                    HighScoreText.text = "NEW HIGHSCORE!";

                }
                else if (finalScore > Score.score2)
                {
                    // put new
                    Score.score2 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score3)
                {
                    // put new
                    Score.score3 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score4)
                {
                    // put new
                    Score.score4 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score5)
                {
                    // put new
                    Score.score5 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score6)
                {
                    // put new
                    Score.score6 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score7)
                {
                    // put new
                    Score.score7 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score8)
                {
                    // put new
                    Score.score8 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score9)
                {
                    // put new
                    Score.score9 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else if (finalScore > Score.score10)
                {
                    // put new
                    Score.score10 = finalScore;
                    StartCoroutine(TryUpdateScore(Score.userid, Score));
                }
                else
                {
                    // do something if finalScore is not greater tahn top 10 scores.
                    Debug.Log("Score was less than top 10");
                }
            }

        }));
        yield return null;
    }

    IEnumerator TryUpdateScore(long userid, ScoreModel ScoreM)
    {
        StartCoroutine(API.UpdateScore(userid, ScoreM, status =>
        {
            switch (status)
            {
                case -1:
                    Debug.Log("error updating score");
                    break;
                case 1:
                    Debug.Log("Updated Score");
                    Debug.Log(Score.highscore);
                    break;
            }
        }));
        yield return null;
    }    
}
