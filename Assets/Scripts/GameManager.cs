using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject FPanel;

    int point;
   public Text pointText;

    public Text pointGOText;
    public Text pointFNText;

    public bool GameOver = false;

    string level;
    void Start()
    {
        Time.timeScale = 1f;
        level = SceneManager.GetActiveScene().name;
        TinySauce.OnGameStarted(levelNumber: level);
    }

    
    void Update()
    {
        
    }

   public void gameOver()
    {
        panel.SetActive(true);
        pointGOText.text = "" + point;
        GameOver = true;
        Time.timeScale = 0;
        TinySauce.OnGameFinished(GameOver, point);
    }

    public void pointCount(int pointC)
    {
        point = pointC;
        pointText.text = "" + point;
    }

    public void Finish()
    {
        FPanel.SetActive(true);
        pointFNText.text = "" + point;
        GameOver = true;
        Time.timeScale = 0;
        TinySauce.OnGameFinished(GameOver, point);
    }

}
