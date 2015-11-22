using UnityEngine;
using System.Collections;

public class Monster_Behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        GetComponent<AudioSource>().Play();
        transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1);
        print("Entered!");
    }
}
