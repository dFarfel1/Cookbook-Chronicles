using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, I_Item
{
	public Sprite sprite;

	public Sprite getSprite()
	{
		return sprite;
	}
}
