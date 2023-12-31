using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	private static int numItems = 24;
	private static int numRows = 4;
	private static int numCols = 6;
	private int selectedIndex;
	private int selectedRow;
	private int selectedCol;
	private inventoryItem[] items;
	public GameObject[] itemsDisplay;

	//For testing purposes

	private bool open;
	public void selectItem(int i){
		selectedCol = i % numCols - 1;
		selectedRow = i / numCols;
		select();
	}

	public bool isInventoryOpen() {
		return open;
	}
	// Start is called before the first frame update
	void Start()
	{
		selectedIndex = 0;
		selectedRow = 0;
		selectedCol = 0;
		items = new inventoryItem[numItems];
		itemsDisplay = new GameObject[numItems];


		open = false;

		for (int i = 1; i <= numItems; i++)
		{
			itemsDisplay[i - 1] = GameObject.Find("item" + i.ToString());
			items[i - 1] = new inventoryItem();
		}


		/*Debug.Log(itemsDisplay.Length);
		Debug.Log(itemsDisplay[6]);*/

	}

	// Update is called once per frame
	void Update()
	{
		bool wasLocked = false;

		if (Cursor.lockState == CursorLockMode.Locked) { 
			wasLocked = true; 
		}

		if (Input.GetKeyDown("b"))
		{
			if (open)
			{
				GetComponent<Canvas>().enabled = false;
				open = false;

				if (wasLocked) {
					Cursor.lockState = CursorLockMode.Locked;
				}
				
			}
			else
			{
				GetComponent<Canvas>().enabled = true;
				open = true;
				Cursor.lockState = CursorLockMode.None;
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) && open)
		{
			selectedCol = (selectedCol - 1 + numCols) % numCols;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)&& open)
		{
			selectedCol = (selectedCol + 1) % numCols;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) && open)
		{
			selectedRow = (selectedRow - 1 + numRows) % numRows;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && open)
		{
			selectedRow = (selectedRow + 1) % numRows;
			select();
		}
		else if (Input.GetKeyDown("l"))
		{
			drop();
		}



		//Temporary adding items
		// if (Input.GetKeyDown("p"))
		// {
		// 	GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		// 	pickup(sphere);
		// }
	}

	private void select()
	{
		int index = selectedRow * numCols + selectedCol;

		/*Debug.Log("Row: " + selectedRow.ToString() + "Col: " + selectedCol.ToString());
		Debug.Log("New: " + index.ToString() + " Old: " + selectedIndex.ToString());*/

		itemsDisplay[selectedIndex].GetComponent<Image>().color = Color.white;

		selectedIndex = index;
		itemsDisplay[selectedIndex].GetComponent<Image>().color = Color.green;
	}

	public void pickupItem (GameObject item)
	{
		if (item.GetComponent<I_Item>() != null)
		{
			item.transform.SetParent(null);
			pickup(item);
 		}
	}

	private void drop()
	{
		if (selectedIndex >= 0 && items[selectedIndex].getName() != "None")
		{
			items[selectedIndex].generateClone(GameObject.Find("FemaleCharacterPBR").transform.position);
			int numItems = items[selectedIndex].drop();

			if (numItems == 0)
			{
				itemsDisplay[selectedIndex].GetComponentInChildren<Image>().sprite = null;
			}

			itemsDisplay[selectedIndex].GetComponentInChildren<Text>().text = "Count: " + numItems.ToString();
			
		}
	}

	private void pickup(GameObject item)
	{
		item.name = item.name.Replace("(Clone)","").Trim();
		bool found = false;
		for (int i = 0; i < numItems; i++)
		{
			if (items[i].getName() == item.name)
			{
				found = true;
				itemsDisplay[i].GetComponentInChildren<Text>().text = "Count: " + items[i].add().ToString();
				Destroy(item);
				break;
			}
		}

		if (!found)
		{
			for (int i = 0; i < numItems; i++)
			{
				if (items[i].getName() == "None")
				{
					items[i].addFirstItem(item, gameObject);
					itemsDisplay[i].GetComponent<Image>().sprite = item.GetComponent<I_Item>().getSprite();
					itemsDisplay[i].GetComponentInChildren<Text>().text = "Count: 1";
					item.SetActive(false);
					break;
				}
			}
		}
	}

}

class inventoryItem
{
	private int count;
	private GameObject gameObject;

	public inventoryItem()
	{
		
	}

	public int add()
	{
		count++;
		return count;
	}

	public void addFirstItem(GameObject gameObject, GameObject inventory)
	{
		gameObject.transform.SetParent(inventory.transform);
		this.gameObject = gameObject;
		count = 1;
	}

	public int drop()
	{
		if (count == 1)
		{
			Object.Destroy(gameObject);
			gameObject = null;
		}

		count--;
		return count;
	}

	public void generateClone(Vector3 position)
	{
		GameObject newClone = GameObject.Instantiate(gameObject, position, Quaternion.identity);
		newClone.SetActive(true);
		newClone.transform.position = new Vector3(newClone.transform.position.x, newClone.transform.position.y + 0.25f, newClone.transform.position.z);
		newClone.transform.SetParent(null);

		newClone.name = newClone.name.Replace("(Clone)","").Trim(); //remove clone from the cloned object
	}

	public string getName()
	{
		if (gameObject == null)
		{
			return "None";
		}

		return gameObject.name;
	}
}