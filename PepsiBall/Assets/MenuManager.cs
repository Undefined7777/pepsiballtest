using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour

{
    public static MenuManager MenuManagerInstance;
    public bool GameState;
    public GameObject menuElement;
    public GameObject shop;


    void Start()
    {
        GameState = false; 
        MenuManagerInstance = this;
    }

    // Update is called once per frame
    public void StartTheGame()
    {
        GameState = true;
        menuElement.SetActive(false);
    }
    public void Retry_btn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void go_toshop()
    {
        shop.SetActive(true);
    }

    public void level2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
