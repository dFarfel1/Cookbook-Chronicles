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

		inventory.GetComponent<Canvas>().enabled = false;
		inventoryOpen = false;

    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
