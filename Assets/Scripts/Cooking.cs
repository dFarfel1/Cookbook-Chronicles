using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    private Dictionary<string, ulong> cookingValues;
    public Dictionary<ulong, GameObject> recipes;
    private static string[] ingredientsList = { "Carrot", "chicken meat" };
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
        recipes[4] = cookedItems[0];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider collision){
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "pickup")
        {
			value += cookingValues[collision.gameObject.name];
            inUseIngredients.Add(collision.gameObject);
            Destroy(collision.gameObject);

            Debug.Log(value);

            if (recipes.ContainsKey(value))
            {
                Vector3 position = transform.position;
                position.y += 1.0f;

				Debug.Log("Recipe Made");
                GameObject.Instantiate(recipes[value], position, Quaternion.identity).SetActive(true);
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

			Debug.Log(value);
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
