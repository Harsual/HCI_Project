using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    //public Text pointsText;
    public TextMeshProUGUI pointsText;

    public void Setup(int score)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
}
