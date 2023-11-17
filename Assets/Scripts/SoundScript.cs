using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public GameObject sphere;
    private float distanceToObject(GameObject thing) {
        Vector3 myPosition = sphere.transform.position;
        Vector3 objectPosition = thing.transform.position;
        return Vector3.Distance(myPosition, objectPosition);
    }

    void OnTriggerEnter(Collider other) //start playing nearby sounds
    {
        AudioSource thatAudio = other.gameObject.GetComponent<AudioSource>();
        if (thatAudio) {
            thatAudio.volume = 0;
            thatAudio.Play();
        }
    }

    void OnTriggerStay(Collider other) //while in range, adjust volume by distance
    {
        AudioSource thatAudio = other.gameObject.GetComponent<AudioSource>();   
        if (thatAudio) {
            //volume of sound is based on the distance, falling off exponentially
            thatAudio.volume = Mathf.Pow(Mathf.Abs(distanceToObject(other.gameObject) - sphere.transform.localScale.x) / sphere.transform.localScale.x, 2) - 0.2f;
        }
    }

    void OnTriggerExit(Collider other) //sound is too far away, stop playing it
    {
        AudioSource thatAudio = other.gameObject.GetComponent<AudioSource>();
        if (thatAudio) {
            thatAudio.Stop();
        }
    }

}
