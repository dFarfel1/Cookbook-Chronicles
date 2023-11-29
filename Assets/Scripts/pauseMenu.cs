using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject resumeMenu;

    public GameObject bookCanvas;
    public GameObject inventory;

    private bool wasBookCanvasEnabled = false;
    private bool wasInventoryEnabled = false;

    public bool isGamePaused(){
        return GameIsPaused;
    }

    void Start(){
        resumeMenu.SetActive(false);


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
        bookCanvas.SetActive(wasBookCanvasEnabled);
        inventory.SetActive(wasInventoryEnabled);
        resumeMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Pause(){
        wasBookCanvasEnabled = bookCanvas.active;
        wasInventoryEnabled = inventory.active;
        bookCanvas.SetActive(false);
        inventory.SetActive(false);
        resumeMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //this is here to manage cursor locks inbetween scenes
    void helperFunc(){
        resumeMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //LoadMainMenu
    public void loadMainMenu(){

        //refresh the resume menu
        helperFunc();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Main Menu");
        
    }

    //quitGame
    public void quitToDesktop(){
        Application.Quit();
    }
}
