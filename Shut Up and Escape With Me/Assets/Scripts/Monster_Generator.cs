using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster_Generator : MonoBehaviour {

    public MazeGenerator maze;
    public GameObject monsterPrefab;
    public int MonsterCount;
    public List<List<int>> monsterPlacement = new List<List<int>>();


    // Use this for initialization
    void Start ()
    {

        for (int x = 0; x < maze.mazeClass.width; x++)
        {
            monsterPlacement.Add(new List<int>());
            for (int y = 0; y < maze.mazeClass.height; y++)
            {
                monsterPlacement[x].Add(0);
            } 
        }
        int row = 0;
        int col = 0;
        Vector3 cellLocation;
        for (int i = 0; i < MonsterCount; i++)
        {
            row = Random.Range(1, maze.mazeClass.height-1);
            col = Random.Range(1, maze.mazeClass.width-1);
            if (monsterPlacement[col][row] != 1)
                monsterPlacement[col][row] = 1;
            else
            {
                i--;
                continue;
            }
            float floorOffset = .3f;
            cellLocation = new Vector3(col * 3, 0.1f, row * 3);
            GameObject monster = (GameObject)Instantiate(monsterPrefab, cellLocation, Quaternion.identity);
            monster.GetComponent<Monster_Behavior>().Initialize(col, row);
        }
	}	
}
