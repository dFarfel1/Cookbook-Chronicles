using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
        public GameObject player;
        public GameObject inventory;
        public pauseMenu pauseMenuScript;
        public GameObject bookCanvas;
        private float cameraOffset;
        private float verticalSensitivity = -0.5f;
        private float leftRightRotation;
        private float cameraTerrainHeightAdjustment = 0.0f;



    // Update is called once per frame
    void Update()
    {
        bool doNothing = inventory.GetComponent<Inventory>().isInventoryOpen();
        //bool
        bool doNothing2 = pauseMenuScript.isGamePaused();
        bool doNothing3 = bookCanvas.active;

        if (doNothing || doNothing2 || doNothing3) {
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
