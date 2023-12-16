using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playAgainMenu : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject pickUpArea;

    public static bool GameIsPaused = false;
    public GameObject resumeMenu;

    public GameObject bookCanvas;
    public GameObject inventory;

    private bool wasBookCanvasEnabled = false;
    private bool wasInventoryEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpArea.GetComponent<Character>().isGameOver()){
            callMenu();
        }
        
    }

    void callMenu(){
        wasBookCanvasEnabled = bookCanvas.active;
        wasInventoryEnabled = inventory.active;
        bookCanvas.SetActive(false);
        inventory.SetActive(false);
        resumeMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverCanvas.SetActive(true);
    }
    

    //this is here to manage cursor locks inbetween scenes
    void helperFunc(){
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
        pickUpArea.GetComponent<Character>().setGameOver(false);
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
