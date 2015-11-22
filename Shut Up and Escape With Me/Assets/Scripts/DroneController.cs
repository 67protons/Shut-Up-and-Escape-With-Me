using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour {
    [HideInInspector]
    public bool collided = false;

    void OnTriggerEnter(Collider hitObject)
    {
        if (hitObject.GetType() == typeof(BoxCollider))
            collided = true;
    }        
}
