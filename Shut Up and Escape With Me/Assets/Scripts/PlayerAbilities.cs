using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {
    
    public GameObject dronePrefab;
    public GameObject playerController;
    public Material redSeal;
    public Material yellowSeal;
    public Material blueSeal;
    public Material defaultWall;
    public GameObject[] head = new GameObject[8];

    private GameObject OVRcamera;
    private GameObject drone;
    private GameObject redWall;
    private GameObject blueWall;
    private GameObject yellowWall;
    private PlayerState state;
    private bool droneOut = false;
    private float acceleration;
    private float rotation;
	    
	void Start () {
        state = this.GetComponent<PlayerState>();
        OVRcamera = playerController.transform.FindChild("OVRCameraRig").gameObject;
        acceleration = playerController.GetComponent<OVRPlayerController>().Acceleration;
        rotation = playerController.GetComponent<OVRPlayerController>().RotationAmount;
        SetHeadTo(false);
    }
		
	void Update () {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            PaintWall(redSeal);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            PaintWall(blueSeal);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            PaintWall(yellowSeal);
        }

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

        SetHeadTo(true);

        float yDegrees = playerController.transform.localRotation.eulerAngles.y;

        //Debug.Log(state.RotationToVector(yDegrees));
        //Quaternion rotation = Quaternion.Euler(state.RotationToVector(yDegrees));

        drone = (GameObject)Instantiate(dronePrefab,
            playerController.transform.position + playerController.transform.forward, Quaternion.identity);
        OVRcamera.transform.parent = drone.transform;
        drone.GetComponent<Rigidbody>().AddForce(state.RotationToVector(yDegrees) * 140, 0);        
    }

    private void ReturnPlayer()
    {
        if (droneOut)
        {
            SetHeadTo(false);
            OVRcamera.GetComponentInChildren<OVRScreenFade>().OnEnable();

            //OVRcamera.transform.rotation = Quaternion.identity;
            OVRcamera.transform.position = playerController.transform.position;
            OVRcamera.transform.parent = playerController.transform;
            Destroy(drone);
            playerController.GetComponent<OVRPlayerController>().Acceleration = acceleration;
            playerController.GetComponent<OVRPlayerController>().RotationAmount = rotation;      
            droneOut = false;
        }
    }

    private void PaintWall(Material color)
    {
        float yDegrees = playerController.transform.localRotation.eulerAngles.y;
        Vector3 directionForward = state.RotationToVector(yDegrees);
        RaycastHit hit;
        if (Physics.Raycast(playerController.transform.position, directionForward, out hit, 3))
        {

            if (MaterialToWall(color) != null)
            {
                MaterialToWall(color).GetComponent<MeshRenderer>().material = defaultWall;
            }

            hit.collider.gameObject.GetComponent<MeshRenderer>().material = color;
            UpdateWall(color, hit.collider.gameObject);

        }
    }

    private void UpdateWall(Material color, GameObject newWall)
    {
        if (color == redSeal)
        {
            redWall = newWall;
        }
        else if (color == blueSeal)
        {
            blueWall = newWall;
        }
        else if (color == yellowSeal)
        {
            yellowWall = newWall;
        }
    }

    private GameObject MaterialToWall(Material color)
    {
        if (color == redSeal)
        {
            return redWall;
        }
        else if (color == blueSeal)
        {
            return blueWall;
        }
        else if (color == yellowSeal)
        {
            return yellowWall;
        }
        else
        {
            return null;
        }
    }

    private void SetHeadTo(bool visible)
    {
        foreach (GameObject headPiece in head)
        {
            headPiece.SetActive(visible);
        }
    }
}
