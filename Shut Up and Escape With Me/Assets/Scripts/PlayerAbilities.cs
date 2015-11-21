using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

    public GameObject dronePrefab;

    private GameObject playerController;

	// Use this for initialization
	void Start () {
        playerController = this.transform.FindChild("OVRPlayerController").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreateDrone();
        }
	}

    private void CreateDrone()
    {
        playerController.GetComponentInChildren<OVRScreenFade>().OnEnable();
        GameObject drone = (GameObject)Instantiate(dronePrefab,
            playerController.transform.position + playerController.transform.forward, snappedRotation());//playerController.transform.rotation);
        //Physics.IgnoreCollision(drone.GetComponent<Collider>(), this.GetComponent<Collider>());
        playerController.transform.parent = drone.transform;
        drone.GetComponent<Rigidbody>().AddForce(this.transform.forward * 50, 0);
    }

    private Quaternion snappedRotation()
    {        
        float x = playerController.transform.rotation.x%360;
        float y = playerController.transform.rotation.y%360;
        float z = playerController.transform.rotation.z%360;        
        if (y >= 315 || y < 45)
        {
            y = 0;
        }
        else if (y >= 45 && y < 135)
        {
            y = 90;
        }
        else if (y >= 135 && y < 225)
        {
            y = 180;
        }
        else if (y >= 225 && y < 315)
        {
            y = 270;
        }
        return Quaternion.Euler(new Vector3(x, y, z));
    }
}
