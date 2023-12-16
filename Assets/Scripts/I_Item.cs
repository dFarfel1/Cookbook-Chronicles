using UnityEngine;

public interface I_Item
{
	public Sprite getSprite();

	public int getProtien();

	public int getCarbs();

	public int getFiber();

	public int getCalories();

	public bool isPoison();

}