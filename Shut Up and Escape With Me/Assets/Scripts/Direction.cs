using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Direction : MonoBehaviour {
    public enum direction
    {
        up,
        down,
        left,
        right
    };

    public static void FaceDirection(GameObject humanoid, Direction.direction direction)
    {
        humanoid.transform.rotation = Quaternion.Euler(new Vector3(0, DirectionToDegrees(direction), 0));
    }

    public static Direction.direction DegreeToDirection(float degree)
    {
        if (degree >= 315 || degree < 45)   //Direction.up
        {
            return Direction.direction.up;
        }
        else if (degree >= 45 && degree < 135)  //Direction.right
        {
            return Direction.direction.right;
        }
        else if (degree >= 135 && degree < 225) //Direction.Down
        {
            return Direction.direction.down;
        }
        else if (degree >= 225 && degree < 315) //Direction.Left
        {
            return Direction.direction.left;
        }
        return Direction.direction.up;
    }

    public static float DirectionToDegrees(Direction.direction direction)
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

    public static Vector3 DegreeToVector(float degree)
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
