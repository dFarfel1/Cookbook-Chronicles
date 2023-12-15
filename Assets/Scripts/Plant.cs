using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, I_Plant
{
    public GameObject fruit;

    public GameObject getFruit() { return fruit; }
}
