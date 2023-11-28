using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject resumeMenu;

    public bool isGamePaused(){
        return GameIsPaused;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        resumeMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    void Pause(){
        resumeMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //LoadMainMenu
    public void loadMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    //quitGame
    public void quitToDesktop(){
        Application.Quit();
    }
}
