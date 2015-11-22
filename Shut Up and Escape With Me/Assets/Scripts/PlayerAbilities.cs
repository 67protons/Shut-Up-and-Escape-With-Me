using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilities : MonoBehaviour {
    
    public GameObject dronePrefab;
    public GameObject playerController;
    public Material redSeal;
    public Material yellowSeal;
    public Material blueSeal;
    public Material defaultWall;
    public GameObject[] head = new GameObject[8];
    public float paintRange = 3;
    public float meleeDistance = 1.5f;

    private GameObject OVRcamera;
    private GameObject drone;
    private GameObject redWall;
    private GameObject blueWall;
    private GameObject yellowWall;
    //private PlayerState state;
    private bool droneOut = false;
    private float acceleration;
    private float rotation;
    private Vector3 cameraOffset;
    private List<GameObject> enemiesInMeleeRange = new List<GameObject>();
	    
	void Start () {
        //state = this.GetComponent<PlayerState>();
        OVRcamera = playerController.transform.FindChild("OVRCameraRig").gameObject;
        acceleration = playerController.GetComponent<OVRPlayerController>().Acceleration;
        rotation = playerController.GetComponent<OVRPlayerController>().RotationAmount;
        enemiesInMeleeRange = transform.GetComponentInChildren<PlayerMeleeManager>().enemiesInMeleeRange;
        SetHeadTo(false);
    }
		
	void Update () {
        if (Input.GetAxisRaw("RightTrigger") == 1 && !droneOut)
        {
            playerController.GetComponent<OVRPlayerController>().Acceleration = acceleration * 2;
        }
        if (Input.GetAxisRaw("RightTrigger") == 0 && !droneOut)
        {
            playerController.GetComponent<OVRPlayerController>().Acceleration = acceleration;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            MeleeAtack();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Z))
        {
            PaintWall(redSeal);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.X))
        {
            PaintWall(blueSeal);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.C))
        {
            PaintWall(yellowSeal);
        }

        if (!droneOut && (Input.GetAxisRaw("LeftTrigger") == 1 || Input.GetKeyDown(KeyCode.Mouse1)))        
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

    private void MeleeAtack()
    {
        foreach (GameObject enemy in enemiesInMeleeRange)
        {
            Destroy(enemy);
        }
        //float yDegrees = playerController.transform.localRotation.eulerAngles.y;
        ////Vector3 directionForward = Direction.DegreeToVector(yDegrees);
        //RaycastHit hit;
        //if (Physics.Raycast(playerController.transform.position, playerController.transform.forward, out hit, meleeDistance))
        //{
        //    Destroy(hit.transform.gameObject);
        //    //Debug.Log(hit.collider.name);
        //    //if (hit.collider.CompareTag("Enemy"))
        //    //{                
        //    //    Debug.Log("Hello?");
        //    //    Destroy(hit.transform.gameObject);
        //    //}
        //}
    }

    private void CreateDrone()
    {
        droneOut = true;
        OVRcamera.GetComponentInChildren<OVRScreenFade>().OnEnable();
        playerController.GetComponent<OVRPlayerController>().Acceleration = 0;
        playerController.GetComponent<OVRPlayerController>().RotationAmount = 0;

        SetHeadTo(true);        


        float yDegrees = playerController.transform.localRotation.eulerAngles.y;        
        drone = (GameObject)Instantiate(dronePrefab,
            playerController.transform.position + playerController.transform.forward, Quaternion.identity);
        cameraOffset = OVRcamera.transform.position;
        OVRcamera.transform.parent = drone.transform;
        drone.GetComponent<Rigidbody>().AddForce(Direction.DegreeToVector(yDegrees) * 140, 0);        
    }

    private void ReturnPlayer()
    {
        if (droneOut)
        {
            SetHeadTo(false);
            OVRcamera.GetComponentInChildren<OVRScreenFade>().OnEnable();            
            OVRcamera.transform.position = cameraOffset;
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
        Vector3 directionForward = Direction.DegreeToVector(yDegrees);
        RaycastHit hit;
        if (Physics.Raycast(playerController.transform.position, directionForward, out hit, paintRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (MaterialToWall(color) != null)
                {
                    MaterialToWall(color).GetComponent<MeshRenderer>().material = defaultWall;
                }

                hit.collider.gameObject.GetComponent<MeshRenderer>().material = color;
                UpdateWall(color, hit.collider.gameObject);
            }           
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
