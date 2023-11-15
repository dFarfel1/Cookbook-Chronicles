using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
        public Camera camera;
        public GameObject player;
        private float cameraHeight;
        private float verticalSensitivity = -0.5f;
        private float leftRightRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraHeight += Input.GetAxis("Mouse Y") * verticalSensitivity;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(camera.transform.position);
        float playerHeight = player.transform.position.y;

        if (cameraHeight > playerHeight + 15) {//keep the camera from going too high
            cameraHeight = playerHeight + 15;
        }

        if (cameraHeight < terrainHeight + 0.5f) {//keep the camera above ground
            cameraHeight = terrainHeight + 0.5f;
        }
        
        camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
        camera.transform.LookAt(player.transform);


        
    }
}
