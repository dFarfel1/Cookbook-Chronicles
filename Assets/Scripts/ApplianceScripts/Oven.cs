using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oven : Cooking
{
	public override void populateRecipes() {
		ingredientsList = new List<string> {"Carrot", "chicken meat","eggplant","corn","pumpkin","onion","green apple", "red apple"};

		recipes = new Dictionary<ulong, GameObject>
		{
			//level 1: 2 carrot + 1 chicken = drumstick //0110
			{6, cookedItems[1]},
			//level 2: 3 eggplants = grilled eggplant //110000
			{48, cookedItems[2]},
			//level 3: 2 pumpkin, 3 corn, 1 onion = pumpkin corn chowder //011011000000
			{1728, cookedItems[3]},
			//level 4: 2 green apple, 2 red apple, 1 carrot, 1 chicken, 1 onion = salad //1010010000000101
			{41989, cookedItems[4]}
		};


		
	}
}
