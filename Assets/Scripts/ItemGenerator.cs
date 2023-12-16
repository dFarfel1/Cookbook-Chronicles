using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public int count;
	public int max = 50;
	public float maxGrowth;

	public GameObject prefab;
	public float radius;
	public float growthFactor;

	void Start()
	{
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
			count = transform.childCount;
			if (count < max)
			{

				Vector3 position = new Vector3(transform.position.x + Random.Range(-radius, radius), transform.position.y, transform.position.z + Random.Range(-radius, radius));
				position.y = Terrain.activeTerrain.SampleHeight(position); //spawn on ground
				GameObject newObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
				//newObject.tag = "ingredient";
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
				if (item.localScale.x < maxGrowth)
				{
					item.localScale *= growthFactor * Random.Range(.75f, 1.25f);
				}
				else if (item.gameObject.GetComponent<Plant>() != null) {
					GameObject.Instantiate(item.gameObject.GetComponent<Plant>().getFruit(), item.position, Quaternion.identity).SetActive(true);
					Destroy(item.gameObject);
				}
				
				
			}
			yield return new WaitForSeconds(6.0f);
		}
	}

	
}
