using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Game_manager;
    public GameObject StartScreen;
    public GameObject GameoverScreen;


    private void Awake()
    {
        Game_manager = this;
        
    }
    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
        GameoverScreen.SetActive(false);
        StartScreen.SetActive(true);

    }
    



    // Update is called once per frame
    void Update () {
		
	}


    public void StartGame()
    {
        Time.timeScale = 1;
        StartScreen.SetActive(false);

    }
    public void GameOver()
    {
        GameoverScreen.SetActive(true);

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            
            Time.timeScale = 1;
            GameoverScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            GameoverScreen.SetActive(true);
        }
    }
    public void reLoad()
    {
        GameoverScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
