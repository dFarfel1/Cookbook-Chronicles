using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{

    public Animator chickenAnimator;
    public GameObject player;
    private float walkSpeed = 1.5f;
    private float runSpeed = 6.0f;
    private float turnSpeed = 5.0f;
    private Vector3 pathfindingLocation;
    public Vector3 rangeCenter = new Vector3(10, 0, 10);
    public float wanderRadius = 30.0f;

    // Start is called before the first frame update

    private void setNewPathfindingLocation() {
        Vector3 unbiasedLocation = Random.insideUnitSphere* wanderRadius;
        pathfindingLocation = unbiasedLocation - 0.05f * rangeCenter;
    }
    void Start()
    {
        pathfindingLocation = transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude < 50 ) {//run away from player
            Vector3 awayDirection = transform.position - player.transform.position;
            Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, Time.deltaTime * turnSpeed);

            Vector3 nextPosition = transform.position + transform.forward * Time.deltaTime * runSpeed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            nextPosition.y = nextTerrainHeight;
            transform.position = nextPosition;
            chickenAnimator.SetBool("run", true);
            chickenAnimator.SetBool("walk", false);
            chickenAnimator.SetBool("eat", false);
        }
        else if ( !chickenAnimator.GetCurrentAnimatorStateInfo(0).IsName("eat") && transform.position == pathfindingLocation)
        {
            chickenAnimator.SetBool("run", false);
            chickenAnimator.SetBool("walk", false);
            chickenAnimator.SetBool("eat", true);
        } else if (transform.position != pathfindingLocation){
        //move toward pathfinding location
            Vector3 newDirection = pathfindingLocation - transform.position;
            Quaternion forwardDirection = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, forwardDirection, Time.deltaTime * turnSpeed);

            Vector3 nextPosition = transform.position + transform.forward * Time.deltaTime * walkSpeed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            nextPosition.y = nextTerrainHeight;
            transform.position = nextPosition;
            chickenAnimator.SetBool("run", false);
            chickenAnimator.SetBool("walk", true);
            chickenAnimator.SetBool("eat", false);
        }
        else {
            setNewPathfindingLocation();
        }
        

    }
}
