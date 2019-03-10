using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard
{
    int highscore;
    readonly int maximumNumberOfScores;

    public Scoreboard()
    {
        maximumNumberOfScores = 10;
        LoadScore();
    }

    private void LoadScore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
    }

    public bool SetScore(int _newScore)
    {
        if (_newScore > highscore)
        {
            highscore = _newScore;
            SaveScore();
            return true;
        }
        return false;
    }

    public int GetScore()
    {
        LoadScore();
        return highscore;
    }

    public void ResetScoreboard()
    {
        PlayerPrefs.SetInt("Highscore", 0);
    }
}
