using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    private Dictionary<string, ulong> cookingValues;
    public Dictionary<ulong, GameObject> recipes;
    private static string[] ingredientsList = { "Carrot", "chicken meat" };
    
    
    //public string test;

    public GameObject[] cookedItems;
    public GameObject instructions;

	private ulong value;
    // Start is called before the first frame update
    void Start()
    {
		cookingValues = new Dictionary<string, ulong>();
		recipes = new Dictionary<ulong, GameObject>();
		instructions.GetComponent<Canvas>().enabled = false;

		value = 0;

        for (int i = 0; i < ingredientsList.Length;  i++)
        {
            cookingValues[ingredientsList[i]] = pow2(i);
        }

        //One Recipe for now 2 carrots one chicken = carrot chicken
        recipes[4] = cookedItems[0];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider collision){
		//Debug.Log(collision.gameObject.tag);
		//Debug.Log(value);
		if (collision.gameObject.GetComponent<Cookable>()!= null)
        {
			value += cookingValues[collision.gameObject.name];
            Destroy(collision.gameObject);

            Debug.Log(value);

            
		}
        else if (collision.gameObject.GetComponent<CharacterMovement>() != null) {
            instructions.GetComponent<Canvas>().enabled = true;
			Cursor.lockState = CursorLockMode.None;
			Debug.Log("Cooking Instrcutions Enabled");
		}
        else {
            Debug.Log("Collision Type not recognized. Name: " + collision.gameObject.name);
        }
    }

    void OnTriggerExit(Collider collision) {
		if (collision.gameObject.GetComponent<CharacterMovement>() != null) {
			instructions.GetComponent<Canvas>().enabled = false;
			Cursor.lockState = CursorLockMode.Locked;
			Debug.Log("Cooking Instrcutions Disabled");
		}
		else {
			Debug.Log("De-Collision Type not recognized. Name: " + collision.gameObject.name);
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
		if (recipes.ContainsKey(value)) {
			Vector3 position = transform.position;
			position.y += 1.5f;

			Debug.Log("Recipe Made");
			GameObject.Instantiate(recipes[value], position, Quaternion.identity).SetActive(true);
			value = 0;
		}
	}
}
