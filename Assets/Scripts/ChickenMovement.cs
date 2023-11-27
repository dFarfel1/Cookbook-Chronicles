using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{

    public Animator chickenAnimator;
    public GameObject player;
    public GameObject chickenGenerator;
    private float walkSpeed = 2.5f;
    private float runSpeed = 4.0f;
    private float turnSpeed = 5.0f;
    private Vector3 pathfindingLocation;
    private Vector3 rangeCenter;
    private float eatDistance = 10.0f;
    public float strayDistance;

    // Start is called before the first frame update

    private void setNewPathfindingLocation() {
        Vector3 unbiasedLocation = Random.insideUnitSphere* eatDistance;
        pathfindingLocation = transform.position + unbiasedLocation;
        Vector3 bias = (pathfindingLocation - rangeCenter) / strayDistance; //stay around the area of rangecenter
        pathfindingLocation -= bias; 
        pathfindingLocation.y = 0;
    }
    private bool isAtPathfindingLocation() {
        return Mathf.Abs(transform.position.x - pathfindingLocation.x) < 1.0f && Mathf.Abs(transform.position.z - pathfindingLocation.z) < 1.0f;
    }
    void Start()
    {
        pathfindingLocation = transform.position ;
        rangeCenter = chickenGenerator.transform.position;
        rangeCenter.y = 0;
        if (strayDistance <= 0) {
            strayDistance = 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude < 10 ) {//run away from player
            Vector3 awayDirection = transform.position - player.transform.position;
            Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, Time.deltaTime * turnSpeed);

            Vector3 nextPosition = transform.position + transform.forward * Time.deltaTime * runSpeed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            nextPosition.y = nextTerrainHeight;
            transform.position = nextPosition;
            chickenAnimator.SetBool("Run", true);
            chickenAnimator.SetBool("Walk", false);
            chickenAnimator.SetBool("Eat", false);
            pathfindingLocation = transform.position;
            pathfindingLocation.y = 0;
        }
        else if ( !chickenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Eat") && isAtPathfindingLocation())
        {
            chickenAnimator.SetBool("Run", false);
            chickenAnimator.SetBool("Walk", false);
            chickenAnimator.SetBool("Eat", true);
        } else if (!isAtPathfindingLocation()){
        //move toward pathfinding location
            Vector3 newDirection = pathfindingLocation - transform.position;
            newDirection.y = 0;
            Quaternion forwardDirection = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, forwardDirection, Time.deltaTime * turnSpeed);

            Vector3 nextPosition = transform.position + transform.forward * Time.deltaTime * walkSpeed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            nextPosition.y = nextTerrainHeight;
            transform.position = nextPosition;
            chickenAnimator.SetBool("Run", false);
            chickenAnimator.SetBool("Walk", true);
            chickenAnimator.SetBool("Eat", false);
        }
        else {
            setNewPathfindingLocation();

        }
        

    }
}
