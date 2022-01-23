using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static int lives;
    public Image[] hearts;
    public Image[] heartsDepleted;
    public Text Points;
    public Text TimePass;
    public Text Mana;
    public Text Wall;
    public Text highScore;
    public static int WallHP;
    public static int points;
    public static float mana;
    public static float time;
    public GameObject lost;
    public Text FinalTime;
    public Text FinalPoint;
    float seconds;
    float minutes;
    float manaTime;
    int HighScore;


    void Start()
    {
        HighScore = PlayerPrefs.GetInt("highs");
        time = 0;
        WallHP = 100;
        lives = 3;
        mana = 0;
        points = 0;
        lost.SetActive(false);
        FinalTime.text = "";
        FinalPoint.text = "";
        Time.timeScale = 1f;
        highScore.text = "";
    }

    void Update()
    {
        Updatelives();
        if ((lives <= 0) || (WallHP <= 0))
        {
            Lose();
        }
        if ((lives > 0) && (WallHP > 0))
        {
            Timer();
            Points.text = "Score:" + points.ToString();
            Mana.text = "Mana:" + mana.ToString();
            Wall.text = "Wall:" + WallHP.ToString();
        }
        if (mana >= 100)
        {
            ManaCount();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lives++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WallHP += 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mana += 100;
        }
        if (points > HighScore)
        {
            HighScore = points;
            StoreHighscore();
        }
    }


    void Updatelives()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < lives)
            {
                hearts[i].gameObject.SetActive(true);
                heartsDepleted[i].gameObject.SetActive(false);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
                heartsDepleted[i].gameObject.SetActive(true);
            }
        }
    }
    void Lose()
    {
      
         Time.timeScale = 0f;
         lost.SetActive(true);
         FinalTime.text= "Time: " + minutes.ToString() + ":" + Mathf.RoundToInt(seconds).ToString();
         TimePass.text = "";
         FinalPoint.text = "Points: " + points.ToString();
         Points.text="";
        Mana.text = "";
        highScore.text = "High Score: " + HighScore.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Endless");
        }
    }
    void ExtraLives()
    {
        if ((lives < 3)&&(points>0))
        {
            if ((points % 100 == 0)||((points-10)%100==0))
            {
                lives++;
            }
        }
    }
    void Timer()
    {
        time += Time.deltaTime;
        minutes= Mathf.Floor(time / 60);
        seconds= time % 60;
        TimePass.text = "Time:" + minutes.ToString() + ":" + Mathf.RoundToInt(seconds).ToString();
    }
    void ManaCount()
    {
        manaTime += Time.deltaTime;
        if (manaTime < 10f)
        {
            MainCharacterMoving.arrowFrequency = 0.2f;
        }
        else
        {
            mana -=100 ;
            MainCharacterMoving.arrowFrequency = 1;
            manaTime = 0;
        }
    }
    void StoreHighscore()
    {
        PlayerPrefs.SetInt("highs", HighScore);
        PlayerPrefs.Save();
    }
}
