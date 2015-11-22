using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour
{
    public Maze maze;
    public GameObject playerController;
    public float health = 100f;
    public Text healthText;

    private float yHeight = 0f;

    // Use this for initialization
    void Start()
    {
        //playerController = this.transform.FindChild("OVRPlayerController").gameObject;
        Orientate(0, 0);
        yHeight = playerController.transform.position.y;
    }

    void Update()
    {
        healthText.text = "Health: " + health.ToString();
        if (health <= 0f)
        {
            Application.LoadLevel("Game");
        }        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentPosition = new Vector3(playerController.transform.position.x, yHeight, playerController.transform.position.z);
        playerController.transform.position = currentPosition;
        this.transform.rotation = playerController.transform.rotation;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public List<int> positionToCell()
    {
        int x = (int)(transform.position.x + 1.5)/ 3;
        int z = (int)(transform.position.z + 1.5)/ 3;
        List<int> result = new List<int>();
        result.Add(x);
        result.Add(z);
        return result;
    }
}