using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    //void OnCollisionEnter(Collision hitObject)
    //{
    //    Debug.Log(hitObject.collider.name);
    //}

    void OnTriggerEnter(Collider hitObject)
    {
        Debug.Log(hitObject.name);
    }
}
