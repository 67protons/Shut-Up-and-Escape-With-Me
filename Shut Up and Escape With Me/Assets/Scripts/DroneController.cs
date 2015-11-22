using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour {
    [HideInInspector]
    public bool collided = false;

    public GameObject purticals;        //I KNOW HOW TO SPELL ITS JUST 4:43 AM

    private AudioSource growl;
    
    void Start()
    {
        growl = this.GetComponent<AudioSource>();
    }        

    void OnTriggerEnter(Collider hitObject)
    {
        if (hitObject.CompareTag("Wall"))
        {
            collided = true;
        }        
        if (hitObject.CompareTag("Enemy"))
        {
            if (hitObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                growl.PlayOneShot(growl.clip);
                Instantiate(purticals, this.transform.position, Quaternion.identity);
            }
            else
            {
                collided = true;
            }           
        }       
    }    
}
