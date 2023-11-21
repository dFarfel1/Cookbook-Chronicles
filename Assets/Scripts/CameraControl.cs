using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
        public GameObject player;
        public GameObject inventory;
        private float cameraOffset;
        private float verticalSensitivity = -0.5f;
        private float leftRightRotation;
        private float cameraTerrainHeightAdjustment = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        bool doNothing = inventory.GetComponent<Inventory>().isInventoryOpen();
        if (doNothing) {
            return;
        }
        
        cameraTerrainHeightAdjustment = 0.0f;
        cameraOffset += Input.GetAxis("Mouse Y") * verticalSensitivity;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(GetComponent<Camera>().transform.position);
        float playerHeight = player.transform.position.y;

        if (cameraOffset > 15) {//keep the camera from going too high
            cameraOffset = 15;
        }

        if (cameraOffset + playerHeight < terrainHeight + 1.0f) {//keep the camera above ground
            cameraTerrainHeightAdjustment = terrainHeight + 1.0f - playerHeight - cameraOffset;
        }
        
        GetComponent<Camera>().transform.position = new Vector3(GetComponent<Camera>().transform.position.x, playerHeight + cameraOffset + cameraTerrainHeightAdjustment, GetComponent<Camera>().transform.position.z);
        GetComponent<Camera>().transform.LookAt(player.transform);


        
    }
}
