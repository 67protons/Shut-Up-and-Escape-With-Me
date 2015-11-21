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
            playerController.transform.position + playerController.transform.forward, Quaternion.identity);//playerController.transform.rotation);
        //Physics.IgnoreCollision(drone.GetComponent<Collider>(), this.GetComponent<Collider>());
        playerController.transform.parent = drone.transform;
        drone.GetComponent<Rigidbody>().AddForce(-playerController.transform.forward * 50, 0);
    }    
}
