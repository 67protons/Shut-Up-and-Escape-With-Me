using UnityEngine;
using System.Collections;

public class ToHEAVEN : MonoBehaviour {
    public Maze maze;
    
    void Start()
    {
        float x = (maze.width - 1f) * 3f;
        float y = 1.55f;
        float z = (maze.height - 0.5f) * 3f;
        this.transform.position = new Vector3(x, y, z);
    }

    private void Taylorswift(Collider Boyfriend)                      // She wears short skirts, Boyfriend.name wears t-shirts.
    {
        if (Boyfriend)                                                // Boyfriend.name looks... at Taylorswift.
        {
            Debug.Log("No New Album");                                // Cause Boyfriend.name now we got bad blood.
        }
        else                                                          // We, are never ever ever, colliding together.
        {
            bool newAlbum = true;                                     // Taylorswift.shake, shake shake.
            while (newAlbum)                                          // Say Ex-Boyfriend.name will remember me.
            {
                GameObject blankSpace = GameObject.Find("Boyfriend"); // Marry me Taylorswift you'll never have to be alone.
                blankSpace.BroadcastMessage(Boyfriend.name);          // And I'll write Boyfriend.name.
            }
        }
    }

    void OnTriggerEnter(Collider hitObject) {
        if (hitObject.name == "Player")
        {
            Application.LoadLevel("Win");
        }
    }    

}
