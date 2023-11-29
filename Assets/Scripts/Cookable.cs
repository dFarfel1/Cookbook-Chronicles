using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookable : MonoBehaviour, I_Cookable
{
    public Sprite sprite;

	public Sprite getSprite()
	{
		return sprite;
	}
}
