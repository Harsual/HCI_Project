using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void StartForestLvl()
    {
        SceneManager.LoadScene("scary_scene");
    }

    public void StartOceanLvl()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void StartHorrorLvl()
    {
        SceneManager.LoadScene("SampleScene");
    }
}