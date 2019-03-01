using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text highscore;
    Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = new Scoreboard();
        highscore.text = "Highscore: " + scoreboard.GetScore().ToString();
    }

    public void ResetHighscore()
    {
        scoreboard.ResetScoreboard();
        highscore.text = "Highscore: " + scoreboard.GetScore().ToString();
    }

}
