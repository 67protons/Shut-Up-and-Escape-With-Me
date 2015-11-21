using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {
    
    public GameObject dronePrefab;

    private GameObject playerController;
    private GameObject drone;
    private PlayerState state;
    private bool droneOut = false;

	// Use this for initialization
	void Start () {
        playerController = this.transform.FindChild("OVRPlayerController").gameObject;
        state = this.GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreateDrone();
        }
        
        //Debugging
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ReturnPlayer();
        }
        //End debugging
	}

    private void CreateDrone()
    {
        if (!droneOut)
        {
            droneOut = true;
            playerController.GetComponentInChildren<OVRScreenFade>().OnEnable();
            drone = (GameObject)Instantiate(dronePrefab,
                playerController.transform.position + playerController.transform.forward, Quaternion.identity);//playerController.transform.rotation);
            //Physics.IgnoreCollision(drone.GetComponent<Collider>(), this.GetComponent<Collider>());
            playerController.transform.parent = drone.transform;
            Debug.Log(-state.RotationToVector(playerController.transform.rotation.y));
            drone.GetComponent<Rigidbody>().AddForce(state.RotationToVector(playerController.transform.rotation.y) * -50, 0);
        }        
    }

    private void ReturnPlayer()
    {
        if (droneOut)
        {
            playerController.transform.position = this.transform.position;
            playerController.transform.parent = this.transform;
            Destroy(drone);
            droneOut = false;
        }
    }
}
