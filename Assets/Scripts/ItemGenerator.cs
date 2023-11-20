using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public int count;
	public static int max = 50;

	public GameObject prefab;
	public float radius;
	public float growthFactor;

	void Start()
	{
		count = 0;
		radius = 50.0f;
		growthFactor = 1.05f;

		StartCoroutine("spawn");
		StartCoroutine("grow");
	}


	void Update()
	{
		
	}

	IEnumerator spawn()
	{
		while (true)
		{
			if (count < max)
			{
				count++;
				Vector3 position = new Vector3(transform.position.x + Random.Range(-radius, radius), transform.position.y, transform.position.z + Random.Range(-radius, radius));
				GameObject newObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
				newObject.SetActive(true);

				newObject.transform.SetParent(gameObject.transform);
			}

			yield return new WaitForSeconds(1.5f);
		}
	}

	IEnumerator grow()
	{
		while (true)
		{
			foreach(Transform item in transform)
			{
				item.localScale *= growthFactor;
			}
			yield return new WaitForSeconds(6.0f);
		}
	}
}
