using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cooking : MonoBehaviour
{
    private Dictionary<string, ulong> cookingValues;
    public Dictionary<ulong, GameObject> recipes;
    protected List<string> ingredientsList;
    private Dictionary<string, int> inUse;
    
    //public string test;

    public GameObject[] cookedItems;
    public GameObject instructions;
    public GameObject mistake; 
    //public GameObject pickUpArea;

    //public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
		cookingValues = new Dictionary<string, ulong>();
        inUse = new Dictionary<string, int>();
		instructions.GetComponent<Canvas>().enabled = false;

        
        populateRecipes();
        clear();

        for (int i = 0; i < ingredientsList.Count * 2;  i += 2)
        {
            cookingValues[ingredientsList[i/2]] = pow2(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "Level: " + GetComponent<Character>().curLevel(4);
    }

	void OnTriggerEnter(Collider collision){
		//Debug.Log(collision.gameObject.tag);
		//Debug.Log(value);
		if (collision.gameObject.GetComponent<Cookable>()!= null)
        {
            inUse[collision.gameObject.name]++;
			Destroy(collision.gameObject);
		}
        else if (collision.gameObject.GetComponent<CharacterMovement>() != null) {
            instructions.GetComponent<Canvas>().enabled = true;
			Cursor.lockState = CursorLockMode.None;
			//Debug.Log("Cooking Instrcutions Enabled");
		}
        else {
            //Debug.Log("Collision Type not recognized. Name: " + collision.gameObject.name);
        }
    }

    void OnTriggerExit(Collider collision) {
		if (collision.gameObject.GetComponent<CharacterMovement>() != null) {
			instructions.GetComponent<Canvas>().enabled = false;
			Cursor.lockState = CursorLockMode.Locked;
			//Debug.Log("Cooking Instrcutions Disabled");
		}
		else {
			//Debug.Log("De-Collision Type not recognized. Name: " + collision.gameObject.name);
		}
	}


    ulong pow2(int i)
    {
        ulong two = 2;
        if (i == 0)
        {
            return 1;
        }

        return two * pow2(i - 1);
    }

    public void cook() {
		Vector3 position = transform.position;
		position.y += 1.5f;

        ulong value = 0;

		foreach (string ingredient in ingredientsList) {
            ulong quantity = (ulong) inUse[ingredient];
            if (quantity >= 4) {
                value = 0;
                break; 
            }

            value += quantity * cookingValues[ingredient];
		}

        // successful
        //Debug.Log(value);
        if(value == 6 ||value ==48||value == 1728 ||value == 41989){
            //pickUpArea.GetComponent<Character>().levelUp();
        }


   
		if (recipes.ContainsKey(value)) {
            Debug.Log("Recipe Made");
            GameObject.Instantiate(recipes[value], position, Quaternion.identity).SetActive(true);
        }
        else {
			GameObject.Instantiate(mistake, position, Quaternion.identity).SetActive(true);
		}
        clear();
	}

    public virtual void populateRecipes() {
        Debug.Log("Wrong One!");
    }

    public void clear() {
        foreach(string ingredient in ingredientsList) {
            inUse[ingredient] = 0;
        }
    }
}
