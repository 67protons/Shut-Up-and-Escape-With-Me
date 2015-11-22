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

        if(hitObject.CompareTag("Enemy"))
        {
            if(hitObject.GetType() == typeof(BoxCollider))
            {

            }

                //Debug.Log(hitObject);
        }
    }
}
