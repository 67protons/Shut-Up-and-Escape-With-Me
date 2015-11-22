using UnityEngine;
using System.Collections;

public class Monster_Behavior : MonoBehaviour {   
    private int col = 0;
    private int row = 0;
    private Maze maze;
    private GameObject player;

	// Use this for initialization
	void Start () {
        maze = GameObject.Find("Maze").GetComponent<Maze>();
        player = GameObject.Find("Player");
        Orientate(col, row);  
	}	

    void Update ()
    {

        transform.LookAt(player.transform);
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
            Destroy(this.gameObject);
    }

    public void Initialize(int col, int row)
    {
        this.col = col;
        this.row = row;
    }    

    void OnTriggerEnter(Collider hitObject)
    {
        //GetComponent<AudioSource>().Play();
        //transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1);
        //print("Entered!");
        if (hitObject.CompareTag("Player"))
        {            
            GetComponent<Animator>().SetBool("rising", true);
        }
    }

    void OnTriggerStay(Collider hitObject)
    {
        if (hitObject.CompareTag("Player") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            hitObject.GetComponent<PlayerState>().TakeDamage(100f * Time.deltaTime);
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
}
