using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, I_Item
{
	public Sprite sprite;

	public int protein;
	public int carbs;
	public int fiber;
	public int calories;
	public bool isPoisoned;

	public string name;


	public Sprite getSprite()
	{
		return sprite;
	}

	public bool isPoison() {
		return isPoisoned;
	}

	public int[] getNutritionInfo() {
		return new int[] {protein, carbs, fiber, calories};
	}

	public string getName() {
		return name;
	}

}
