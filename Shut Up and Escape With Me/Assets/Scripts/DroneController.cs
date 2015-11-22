using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour {
    [HideInInspector]
    public bool collided = false;

    void OnTriggerEnter(Collider hitObject)
    {
        collided = true;
    }

    //public void Initialize(PlayerAbilities pa)
    //{
    //    this.pa = pa;
    //}
}
