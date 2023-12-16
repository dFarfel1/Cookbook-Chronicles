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

	public Sprite getSprite()
	{
		return sprite;
	}


	public int getProtien() {
		return protein;
	}

	public int getCarbs() {
		return carbs;
	}

	public int getFiber() {
		return fiber;
	}

	public int getCalories() {
		return calories;
	}

	public bool isPoison() {
		return isPoisoned;
	}

}
