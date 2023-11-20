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
	private GameObject[] items;
	public GameObject[] itemsDisplay;
	// Start is called before the first frame update
	void Start()
	{
		selectedIndex = 0;
		selectedRow = 0;
		selectedCol = 0;
		items = new GameObject[numItems];
		itemsDisplay = new GameObject[numItems];

		for (int i = 1; i <= numItems; i++)
		{
			itemsDisplay[i - 1] = GameObject.Find("item" + i.ToString());
		}


		/*Debug.Log(itemsDisplay.Length);
		Debug.Log(itemsDisplay[6]);*/

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			selectedCol = (selectedCol - 1 + numCols) % numCols;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			selectedCol = (selectedCol + 1) % numCols;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			selectedRow = (selectedRow - 1 + numRows) % numRows;
			select();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			selectedRow = (selectedRow + 1) % numRows;
			select();
		}

		if (Input.GetKey("l"))
		{
			drop();
		}

		//Temporary adding items
		if (Input.GetKeyDown("p"))
		{
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			pickup(sphere);
		}
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

	void OnEnterItemTrigger(GameObject item)
	{
		if (Input.GetKey("p"))
		{
			pickup(item);
		}
	}

	private void drop()
	{
		if (selectedIndex >= 0 && items[selectedIndex] != null)
		{
			items[selectedIndex].SetActive(true);
			items[selectedIndex].transform.position = GameObject.Find("ModularCharacterPBR").transform.position;
			itemsDisplay[selectedIndex].GetComponentInChildren<Text>().text = "none";
			items[selectedIndex] = null;
		}
	}

	private void pickup(GameObject item)
	{
		for (int i = 0; i < numItems; i++)
		{
			if (items[i] == null)
			{
				items[i] = item;
				itemsDisplay[i].GetComponentInChildren<Text>().text = item.name;
				item.SetActive(false);
				break;
			}
		}
	}

}