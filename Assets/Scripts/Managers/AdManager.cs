using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager instance = null;

    private int retries;
    private readonly int adNumber = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("AdNumber"))
        {
            retries = PlayerPrefs.GetInt("AdNumber");
        }
        else
        {
            retries = 0;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ShowAdvert()
    {
        if (retries >= adNumber)
        {
            Advertisement.Show();
            retries = 0;
        }
        else
        {
            retries++;
        }

        PlayerPrefs.SetInt("AdNumber", retries);
    }
}
