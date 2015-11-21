using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    void Update()
    {
        float MoveForward = Input.GetAxis("Vertical") * 10 * Time.deltaTime;
        float MoveRotate = Input.GetAxis("Horizontal") * 100 * Time.deltaTime;

        transform.Translate(Vector3.forward * MoveForward);
        transform.Rotate(Vector3.up * MoveRotate);

    }

}