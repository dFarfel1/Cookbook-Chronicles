using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private GameObject inventory;
    private bool inventoryOpen;
    private bool inCookingArea;
	
    public Slider hunger; 
    public int health;
	public int numOfHearts;
	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	public bool gameOver;
	public bool gameWon;

    private int time;
    public Animator playerAnimator;

    public GameObject pauseCanvas;
	public GameObject playAgainCanvas;

	public GameObject nutritionLabel;
	public Text nutriotionLabelTitle;
	public Text Eat;

	public bool[] levels;

    void Start()
    {
		//levels system
		levels = new bool[] {false,false,false,false,false};

        inventory = GameObject.Find("Inventory");
        health = 3;
        time = 10000;
		gameOver = false;
		gameWon = false;

		inventory.GetComponent<Canvas>().enabled = false;
		inventoryOpen = false;
    }

    void Update()
    {	
		if(health <= 0){
			Debug.Log("You Lost!");
			gameOver = true;
		}
			
		//deals with siturations if extra health was added
		if(health > numOfHearts){
			health = numOfHearts;
		}
		//updating number of hearts
		for(int i= 0; i<hearts.Length; i++){

			//cosmetics of filled or unfilled heart
			if(i < health){
				hearts[i].sprite = fullHeart;
			}
			else{
				hearts[i].sprite = emptyHeart;
			}

			if(i < numOfHearts){
				hearts[i].enabled  = true;
			}
			else{
				hearts[i].enabled  = false;
			}

		}
        if (!pauseCanvas.GetComponent<pauseMenu>().isGamePaused()) {
			time--;
			hunger.value = time;

			if (time <= 0) {
				time = 2000;
				health -- ;
				Debug.Log("Lost a life");

			}

			if (Input.GetKeyDown("b")) {
				if (inventoryOpen) {
					inventory.GetComponent<Canvas>().enabled = false;
					inventoryOpen = false;

				}
				else {
					inventory.GetComponent<Canvas>().enabled = true;
					inventoryOpen = true;
				}
			}

		}

	}

	//returns gameOver status if needed by other scripts
	public bool isGameOver(){
		return gameOver;
	}

    //checks if game is won
	public bool isGameWon(int num){
		bool result = levels[0];
		for(int i = 1; i<num; i++){
			result = result && levels[i];
		}
		if(result){
			gameWon = true;
		}
		return result;
	}

	//helper function for levelingUp
	public void levelUp(){
		int index = -1;
		for(int i = 0; i<levels.Length; i++){
			if(levels[i] == true){
				continue;
			}
			else{
				index = i;
				break;
			}
		}
		if(index!= -1){
			levels[index] = true;
		}
	}

	public int curLevel(int max){
		int result = 0;
		for(int i=0;i<max; i++){
			if(levels[i]){
				result ++;
			}
		}
		return result;
	}

	//set game over boolean
	public void setGameOver(bool condition){
		gameOver = condition;
	}

	//setter for game won
	public void setGameWon(bool condition){
		gameWon = condition;

	}

    void OnTriggerStay(Collider collision)
    {
		if (collision.gameObject.GetComponent<Cooking>() != null) {
            inCookingArea = true;
        }
        else if (collision.gameObject.GetComponent<Plant>() == null && collision.gameObject.GetComponent<Item>() != null) {
			nutritionLabel.SetActive(true);

			nutriotionLabelTitle.text = collision.gameObject.GetComponent<Item>().getName();

			Slider[] sliders = nutritionLabel.GetComponentsInChildren<Slider>();
			int[] nutritionInfo = collision.gameObject.GetComponent<Item>().getNutritionInfo();

			for(int i = 0; i < nutritionInfo.Length; i++) {
				sliders[i].value = nutritionInfo[i];
			}




			if (Input.GetKey("p")) {
				inventory.GetComponent<Inventory>().pickupItem(collision.gameObject);
				nutritionLabel.SetActive(false);
			}
			else if (Input.GetKey("e")) {
				GameObject.Destroy(collision.gameObject);
				time += nutritionInfo[4];

				if (time > 2000) {time = 2000;}

				if (collision.gameObject.GetComponent<Item>().isPoison()) {
					health--;
				}
			}
			if (collision.gameObject.GetComponent<I_OnHit>() != null && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("swing")) {
				collision.gameObject.GetComponent<I_OnHit>().onHit();
			} 

			
		}
		
    }

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.GetComponent<Plant>() == null && collision.gameObject.GetComponent<Item>() != null) {
			nutritionLabel.SetActive(false);
		}
	}

    
}
