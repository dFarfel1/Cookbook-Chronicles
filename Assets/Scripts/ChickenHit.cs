using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHit : MonoBehaviour, I_OnHit
{
    public GameObject objectToDrop;
    public GameObject chicken;
    
    public void onHit() {
        GameObject droppedObject = Object.Instantiate(objectToDrop, transform.position, Quaternion.identity);
        droppedObject.SetActive(true);
        droppedObject.name = "chicken meat";
        droppedObject.transform.position = new Vector3(droppedObject.transform.position.x, droppedObject.transform.position.y + 0.25f, droppedObject.transform.position.z);//keep from being in the ground
        droppedObject.transform.localScale = droppedObject.transform.localScale * 0.5f;
        Destroy(chicken);
    }
}
