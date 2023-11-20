using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public int count;
	private static int max = 50;

	public GameObject prefab;
	public float radius;

	void Start()
	{
		count = 0;
		radius = 5.0f;

		StartCoroutine("spawn");
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
				GameObject.Instantiate(prefab, position, Quaternion.identity).SetActive(true);
			}

			yield return new WaitForSeconds(1.5f);
		}
	}
}
