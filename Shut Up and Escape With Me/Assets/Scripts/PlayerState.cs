using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public Maze maze;
    public GameObject playerController;

	// Use this for initialization
	void Start () {
        //playerController = this.transform.FindChild("OVRPlayerController").gameObject;
        Orientate(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Orientate(int x, int y)
    {
        if (!maze.wallExists(x, y, Direction.direction.up))
        {            
            Direction.FaceDirection(playerController, Direction.direction.up);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.right))
        {           
            Direction.FaceDirection(playerController, Direction.direction.right);
            return;
        }

    }    
}
