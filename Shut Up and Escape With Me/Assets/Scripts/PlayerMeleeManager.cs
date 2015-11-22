using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMeleeManager : MonoBehaviour {
    
    public List<GameObject> enemiesInMeleeRange;

	void FixedUpdate()
    {
        List<GameObject> toDelete = new List<GameObject>();

        foreach (GameObject enemy in enemiesInMeleeRange)
        {
            if (enemy == null)
            {
                toDelete.Add(enemy);
            }
        }

        foreach (GameObject enemy in toDelete)
        {
            enemiesInMeleeRange.Remove(enemy);
        }
    }

    void OnTriggerEnter(Collider hitObject)
    {
        if (hitObject.GetType() == typeof(BoxCollider) && hitObject.CompareTag("Enemy"))
        {
            enemiesInMeleeRange.Add(hitObject.gameObject);
        }
    }

    void OnTriggerExit(Collider hitObject)
    {
        if (hitObject.GetType() == typeof(BoxCollider) && hitObject.CompareTag("Enemy"))
        {
            enemiesInMeleeRange.Remove(hitObject.gameObject);
        }
    }
}
