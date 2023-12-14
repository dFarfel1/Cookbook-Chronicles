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

    private int time;
    public Animator playerAnimator;

    public GameObject pauseCanvas;

    void Start()
    {
        inventory = GameObject.Find("Inventory");
        health = 3;
        time = 10000;

		inventory.GetComponent<Canvas>().enabled = false;
		inventoryOpen = false;

    }

    void Update()
    {	
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
				time = 10000;
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


    void OnTriggerStay(Collider collision)
    {
		if (collision.gameObject.GetComponent<Cooking>() != null) {
            inCookingArea = true;
        }
        else {
			if (Input.GetKey("p")) {
				inventory.GetComponent<Inventory>().pickupItem(collision.gameObject);
			}
			if (collision.gameObject.GetComponent<I_OnHit>() != null && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("swing")) {
				collision.gameObject.GetComponent<I_OnHit>().onHit();
			}
		}
		
    }

    
}
