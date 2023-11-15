using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static int numItems = 5;
    private int selectedIndex;
    private GameObject[] items;
    public GameObject[] itemsDisplay;
    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = -1;
        items = new GameObject[numItems];
		itemsDisplay = new GameObject[numItems];

        for (int i = 1; i <= numItems; i++)
        {
            itemsDisplay[i-1] = GameObject.Find("item" + i.ToString());
        }


        //Debug.Log(itemsDisplay[0].name);

        
	}

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i <= numItems; i++)
        {

			KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i.ToString());

			if (Input.GetKey(keyCode)){
                //Debug.Log();
                select(i);
                break;
            }
        }

        if (selectedIndex >= 0)
        {
            if (Input.GetKey("l"))
            {
                
            }
        }
    }

    private void select(int index)
    {
        if (index != selectedIndex && index <= numItems && index > 0){
            if (selectedIndex != -1)
            {
				itemsDisplay[selectedIndex - 1].GetComponent<Image>().color = Color.white;
			}
            
            selectedIndex = index;
			itemsDisplay[selectedIndex - 1].GetComponent<Image>().color = Color.green;
		}

        if (index == 0 && selectedIndex > 0)
        {
			itemsDisplay[selectedIndex - 1].GetComponent<Image>().color = Color.white;
			selectedIndex = -1;
        }
    }

}
