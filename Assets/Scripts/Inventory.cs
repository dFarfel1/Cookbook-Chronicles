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

	private bool open;

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
		if (Input.GetKeyDown("b"))
		{
			if (open)
			{
				GetComponent<Canvas>().enabled = false;
				open = false;
				Cursor.lockState = CursorLockMode.Locked;
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
		if (item.tag == "pickup")
		{
			pickup(item);
 		}
	}

	private void drop()
	{
		if (selectedIndex >= 0 && items[selectedIndex].getName() != "None")
		{
			items[selectedIndex].generateClone(GameObject.Find("ModularCharacterPBR").transform.position);
			int numItems = items[selectedIndex].drop();

			if (numItems == 0)
			{
				itemsDisplay[selectedIndex].GetComponentInChildren<Text>().text = "none";
			}

			itemsDisplay[selectedIndex].GetComponentsInChildren<Text>()[1].text = "Count: " + numItems.ToString();
			
		}
	}

	private void pickup(GameObject item)
	{
		bool found = false;
		for (int i = 0; i < numItems; i++)
		{
			if (items[i].getName() == item.name)
			{
				found = true;
				itemsDisplay[i].GetComponentsInChildren<Text>()[1].text = "Count: " + items[i].add().ToString();
				item.SetActive(false);
				break;
			}
		}

		if (!found)
		{
			for (int i = 0; i < numItems; i++)
			{
				if (items[i].getName() == "None")
				{
					items[i].addFirstItem(item);
					itemsDisplay[i].GetComponentInChildren<Text>().text = item.name;
					itemsDisplay[i].GetComponentsInChildren<Text>()[1].text = "Count: ";
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

	public void addFirstItem(GameObject gameObject)
	{
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