using UnityEngine;
using System.Collections;

public class ToHEAVEN : MonoBehaviour {

    void OnTriggerEnter(Collider hitObject) {
        if (hitObject.name == "Player")
        {
            Application.LoadLevel("Win");
        }
    }    
}
