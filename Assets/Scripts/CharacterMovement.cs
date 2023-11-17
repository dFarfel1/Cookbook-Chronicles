using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    // Start is called before the first frame update

    private float lookDir;
    public float mouseSensitivity = 3.0f;
    public GameObject player;
    public Animator playerAnimator;
    private int speed = 8;
    private float jump_power = 10.0f;
    private float gravity = 20.0f;
    private float maxWallClimbHeight = 0.03f;

    private float vertical_speed = 0.0f;
    private bool onGround = true;

    private bool jumping = false;
    private bool forwarding = false;
    private bool backing = false;
    private bool landing = false;
    private bool lefting = false;
    private bool righting = false;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // player.transform.position = new Vector3 (900,1,50);
        player.transform.rotation = new Quaternion (0,0,0,0);

    }

    public bool CheckGround()

    
    {
        float terrainHeight = Terrain.activeTerrain.SampleHeight(player.transform.position);//user terrain map to check height
        if (player.transform.position.y > terrainHeight + 0.02) { //in air but allow for a little leeway
            return false;
        } else { //on ground or below, so go to top of terrain.
            player.transform.position = new Vector3 (player.transform.position.x, terrainHeight, player.transform.position.z ); 
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //mouse rotation control
        lookDir += Input.GetAxis("Mouse X") * mouseSensitivity;
        player.transform.localRotation = Quaternion.Euler(0, lookDir, 0);

        //check if landed
        onGround = CheckGround();
        Debug.Log(onGround);
        
        //used for animation control
        jumping = false;
        forwarding = false;
        backing = false;
        landing = false;
        lefting = false;
        righting = false;


        //movement
        if (Input.GetKey("w")) {
            //prevent vertical wall climbs
            Vector3 nextPosition = player.transform.position +  player.transform.forward * Time.deltaTime * speed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            if (nextTerrainHeight < player.transform.position.y + maxWallClimbHeight) {
                forwarding = true;
                player.transform.position = nextPosition;
            }
        }
        else if (Input.GetKey("s")) {

            Vector3 nextPosition = player.transform.position - player.transform.forward * Time.deltaTime * speed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            if (nextTerrainHeight < player.transform.position.y + maxWallClimbHeight) {
                backing = true;
                player.transform.position = nextPosition;
            }
        }
        else if (Input.GetKey("a")) {
    
            Vector3 nextPosition = player.transform.position - player.transform.right * Time.deltaTime * speed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            if (nextTerrainHeight < player.transform.position.y + maxWallClimbHeight) {
                lefting = true;
                player.transform.position = nextPosition;
            }
        }
        else if (Input.GetKey("d")) {
            
            Vector3 nextPosition = player.transform.position + player.transform.right * Time.deltaTime * speed;
            float nextTerrainHeight = Terrain.activeTerrain.SampleHeight(nextPosition);
            if (nextTerrainHeight < player.transform.position.y + maxWallClimbHeight) {
                righting = true;
                player.transform.position = nextPosition;
            }
        }

        //jumping
        if (Input.GetKeyDown("space")) {//if player is only slightly off the ground from walking downhill still jump
            if (onGround) { 
                jumping = true;
                vertical_speed = jump_power;
                onGround = false;
            }
        }

        //check if in air and apply gravity
        if (!onGround) {
            player.transform.position += player.transform.up * Time.deltaTime * vertical_speed;
            vertical_speed -= gravity * Time.deltaTime; //apply gravity to vertical speed when in the air
        } else {
            if (vertical_speed != 0.0f) {
                landing = true;
                vertical_speed = 0.0f;
            }
        }

        //set animation stuff
        playerAnimator.SetBool("jump", jumping);
        playerAnimator.SetBool("fwd", forwarding);
        playerAnimator.SetBool("back", backing);
        playerAnimator.SetBool("land", landing);
        playerAnimator.SetBool("left", lefting);
        playerAnimator.SetBool("right", righting);
        playerAnimator.SetBool("air", !onGround);

    }
}
