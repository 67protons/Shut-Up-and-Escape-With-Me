using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    private bool leftWall = true;
    private bool rightWall = true;
    private bool upWall = true;
    private bool downWall = true;

    public bool WallAt(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                return upWall;
            case Direction.direction.down:
                return downWall;
            case Direction.direction.left:
                return leftWall;
            case Direction.direction.right:
                return rightWall;
            default:
                return false;
        }
    }

    public void AddWall(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                upWall = true;
                break;
            case Direction.direction.down:
                downWall = true;
                break;
            case Direction.direction.left:
                leftWall = true;
                break;
            case Direction.direction.right:
                rightWall = true;
                break;            
        }
    }

    public void RemoveWall(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                upWall = false;
                break;
            case Direction.direction.down:
                downWall = false;
                break;
            case Direction.direction.left:
                leftWall = false;
                break;
            case Direction.direction.right:
                rightWall = false;
                break;
        }
    }
}
