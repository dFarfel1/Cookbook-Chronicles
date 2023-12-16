using UnityEngine;

public interface I_Item
{
	public Sprite getSprite();

	public bool isPoison();

	public int[] getNutritionInfo();

	public string getName();

}