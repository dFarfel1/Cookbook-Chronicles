using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
        public Camera camera;
        public GameObject player;
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
        cameraTerrainHeightAdjustment = 0.0f;
        cameraOffset += Input.GetAxis("Mouse Y") * verticalSensitivity;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(camera.transform.position);
        float playerHeight = player.transform.position.y;

        if (cameraOffset > 15) {//keep the camera from going too high
            cameraOffset = 15;
        }

        if (cameraOffset + playerHeight < terrainHeight + 1.0f) {//keep the camera above ground
            cameraTerrainHeightAdjustment = terrainHeight + 1.0f - playerHeight - cameraOffset;
        }
        
        camera.transform.position = new Vector3(camera.transform.position.x, playerHeight + cameraOffset + cameraTerrainHeightAdjustment, camera.transform.position.z);
        camera.transform.LookAt(player.transform);


        
    }
}
