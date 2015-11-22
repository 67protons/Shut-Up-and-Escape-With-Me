using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {
    
    public GameObject dronePrefab;
    public GameObject playerController;

    private GameObject OVRcamera;
    private GameObject drone;
    private PlayerState state;
    private bool droneOut = false;
	
	void Start () {
        state = this.GetComponent<PlayerState>();
        OVRcamera = playerController.transform.FindChild("OVRCameraRig").gameObject;
	}
		
	void Update () {
        if (!droneOut && (Input.GetAxisRaw("LeftTrigger") == 1 || Input.GetKeyDown(KeyCode.Mouse1)))
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreateDrone();
        }


        if (droneOut)
        {
            if (drone.GetComponent<DroneController>().collided)
            {
                ReturnPlayer();
            }
        }
        //Debugging
        //if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton5))
        //{
        //    ReturnPlayer();
        //}
        //End debugging        
	}

    void LateUpdate()
    {
        if (!droneOut)
        {
            this.transform.position = playerController.transform.position;
        }
    }

    private void CreateDrone()
    {
        droneOut = true;
        playerController.GetComponent<OVRPlayerController>().Acceleration = 0;
        playerController.GetComponent<OVRPlayerController>().RotationAmount = 0;

        float yDegrees = playerController.transform.localRotation.eulerAngles.y;

        //Debug.Log(state.RotationToVector(yDegrees));
        //Quaternion rotation = Quaternion.Euler(state.RotationToVector(yDegrees));

        drone = (GameObject)Instantiate(dronePrefab,
            playerController.transform.position + playerController.transform.forward, Quaternion.identity);
        OVRcamera.transform.parent = drone.transform;
        drone.GetComponent<Rigidbody>().AddForce(state.RotationToVector(yDegrees) * 100, 0);        
    }

    private void ReturnPlayer()
    {
        if (droneOut)
        {
            OVRcamera.GetComponentInChildren<OVRScreenFade>().OnEnable();

            //OVRcamera.transform.rotation = Quaternion.identity;
            OVRcamera.transform.position = playerController.transform.position;
            OVRcamera.transform.parent = playerController.transform;
            Destroy(drone);
            playerController.GetComponent<OVRPlayerController>().Acceleration = 0.3f;
            playerController.GetComponent<OVRPlayerController>().RotationAmount = 1.5f;      
            droneOut = false;
        }
    }
}
