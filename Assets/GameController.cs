using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public DamageIndicator DamageIndicator;

    int score = 0;


    

    public void GameOver()
    {
        GameOverScreen.Setup(score);
    }

    public void UpdateScore()
    {
        score++;
        Debug.Log(score);
    }
}
