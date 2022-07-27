 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Keeps track of the player */

public class PlayerManager : MonoBehaviour {

	#region Singleton

	public GameOverScreen GameOverScreen;
	int maxPlatform;

	public static PlayerManager instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public GameObject player;

	public void KillPlayer ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GameOver()
    {
		GameOverScreen.Setup(maxPlatform);
    }

}