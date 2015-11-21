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
            FaceDirection(Direction.direction.up);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.right))
        {           
            FaceDirection(Direction.direction.right);
            return;
        }

    }

    public void FaceDirection(Direction.direction direction)
    {
        playerController.transform.rotation = Quaternion.Euler(new Vector3(0, DirectionToRotation(direction), 0));
    }

    public float DirectionToRotation(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                return 0f;
            case Direction.direction.right:
                return 90f;
            case Direction.direction.down:
                return 180f;
            case Direction.direction.left:
                return 270f;
            default:
                return 0f;
        }
    }

    public Vector3 RotationToVector(float degree)
    {
        if (degree >= 315 || degree < 45)   //Direction.up
        {
            return new Vector3(0, 0, 1);
        }
        else if (degree >= 45 && degree < 135)  //Direction.right
        {
            return new Vector3(1, 0, 0);
        }
        else if (degree >= 135 && degree < 225) //Direction.Down
        {
            return new Vector3(0, 0, -1);
        }
        else if (degree >= 225 && degree < 315) //Direction.Left
        {
            return new Vector3(-1, 0, 0);
        }
        return new Vector3(0, 0, 0);
    }   
}
