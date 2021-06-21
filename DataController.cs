using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    void Start ()  
    {
        DontDestroyOnLoad (gameObject); //keeps the script functioning throughout the game

        SceneManager.LoadScene ("MenuScreen"); // load the main menu
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
