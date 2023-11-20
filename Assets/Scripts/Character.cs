using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameObject inventory;
    private bool inventoryOpen;
    void Start()
    {
        inventory = GameObject.Find("Inventory");

        inventory.SetActive(false);
        inventoryOpen = false;

    }

    
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            if (inventoryOpen)
            {
                inventory.SetActive(false);
				inventoryOpen = false;
			}
            else
            {
                inventory.SetActive(true);
                inventoryOpen = true;
            }
        }
    }
}
