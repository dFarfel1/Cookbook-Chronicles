using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    private Dictionary<string, ulong> cookingValues;
    public Dictionary<ulong, GameObject> recipes;
    private static string[] ingredientsList = { "Carrot", "WholeBirdRaw" };
    private List<GameObject> inUseIngredients;
    //public string test;

    public GameObject[] cookedItems;

	private ulong value;
    // Start is called before the first frame update
    void Start()
    {
		cookingValues = new Dictionary<string, ulong>();
		recipes = new Dictionary<ulong, GameObject>();
		inUseIngredients = new List<GameObject>();

		value = 0;

        for (int i = 0; i < ingredientsList.Length;  i++)
        {
            cookingValues[ingredientsList[i]] = pow2(i);
        }

        //One Recipe for now 2 carrots one chicken = carrot chicken
        recipes[6] = cookedItems[0];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerStay(Collider collision){
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "pickup")
        {
			value += cookingValues[collision.gameObject.name];
            inUseIngredients.Add(collision.gameObject);

            Debug.Log(value);

            if (recipes.ContainsKey(value))
            {
                GameObject.Instantiate(recipes[value], transform.position, Quaternion.identity);
                inUseIngredients.Clear();
			}
		}
    }

    void OnCollisionExit(Collision collision)
    {
		if (collision.gameObject.tag == "pickup")
        {
			value += cookingValues[collision.gameObject.name];
			inUseIngredients.Remove(collision.gameObject);
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
}
