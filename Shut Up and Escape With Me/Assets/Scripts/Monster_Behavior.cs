using UnityEngine;
using System.Collections;

public class Monster_Behavior : MonoBehaviour {

    private int col = 0;
    private int row = 0;
    private Maze maze;

	// Use this for initialization
	void Start () {
        maze = GameObject.Find("Maze").GetComponent<Maze>();
        Orientate(col, row);  
	}	

    void Update ()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
            Destroy(this.gameObject);
    }

    public void Initialize(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    void OnTriggerStay(Collider hitObject)
    {
        //GetComponent<AudioSource>().Play();
        //transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1);
        //print("Entered!");
        if (hitObject.CompareTag("Player"))
        {
            //Debug.Log("SHUTUP AND....");
            //Debug.Log(Vector3.MoveTowards(transform.position, hitObject.transform.position, Time.deltaTime / 2));
        }
    }

    public void Orientate(int x, int y)
    {
        if (!maze.wallExists(x, y, Direction.direction.up))
        {
            Direction.FaceDirection(this.gameObject, Direction.direction.up);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.down))
        {
            Direction.FaceDirection(this.gameObject, Direction.direction.down);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.right))
        {
            Direction.FaceDirection(this.gameObject, Direction.direction.right);
            return;
        }
        else if (!maze.wallExists(x, y, Direction.direction.left))
        {
            Direction.FaceDirection(this.gameObject, Direction.direction.left);
            return;
        }
    }

    private void Patrol()
    {

    }
}
