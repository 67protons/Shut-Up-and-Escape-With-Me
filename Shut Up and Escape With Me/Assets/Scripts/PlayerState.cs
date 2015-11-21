using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public Maze maze;

    private GameObject playerController;

	// Use this for initialization
	void Start () {
        playerController = this.transform.FindChild("OVRPlayerController").gameObject;
        Orientate(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Orientate(int x, int y)
    {
        if (!maze.wallExists(x, y, Direction.direction.up))
        {
            Debug.Log("No Wall Up");
            FaceDirection(Direction.direction.up);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.right))
        {
            Debug.Log("No Wall Right");
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

    public float SnapDegree(float degree)
    {
        if (degree >= 315 || degree < 45)
        {
            return 0f;
        }
        else if (degree >= 45 && degree < 135)
        {
            return 90f;
        }
        else if (degree >= 135 && degree < 225)
        {
            degree = 180f;
        }
        else if (degree >= 225 && degree < 315)
        {
            degree = 270;
        }

        return 0f;
    }   
}
