using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameObject inventory;
    private bool inventoryOpen;
    private bool inCookingArea;


    public Animator playerAnimator;

    void Start()
    {
        inventory = GameObject.Find("Inventory");


		inventory.GetComponent<Canvas>().enabled = false;
		inventoryOpen = false;

    }

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            if (inventoryOpen)
            {
				inventory.GetComponent<Canvas>().enabled = false;
				inventoryOpen = false;

			}
            else
            {
				inventory.GetComponent<Canvas>().enabled = true;
				inventoryOpen = true;
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
