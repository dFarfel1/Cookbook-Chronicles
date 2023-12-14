using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oven : Cooking
{
	public override void populateRecipes() {
		ingredientsList = new List<string> { "Carrot", "chicken meat" };

		//One Recipe for now 2 carrots one chicken = carrot chicken
		recipes = new Dictionary<ulong, GameObject>
		{
			{6, cookedItems[1]}
		};
		
	}
}
