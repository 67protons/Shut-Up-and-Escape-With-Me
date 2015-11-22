using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {	    
    public Maze mazeClass;
    public GameObject cellPrefab;

    public List<List<GameObject>> mazeCells = new List<List<GameObject>>();     //Used SOLELY for player painting

    void Awake(){        
        generateMaze();
        mazeClass.removeWall(mazeClass.width - 1, mazeClass.height - 1, Direction.direction.up);
        buildMaze();        
    }

	private List<int> directionToCartesian(Direction.direction direction){
		switch(direction){
			case Direction.direction.up:
				return new List<int>{0,1};
			case Direction.direction.down:
				return new List<int>{0, -1};
			case Direction.direction.left:
				return new List<int>{-1,0};
			case Direction.direction.right:
				return new List<int>{1, 0};
            default:
                return new List<int>();
		}
	}

	private bool isValidCell(int x, int y, List<List<bool>> visitedCoordinates){
		return (x >= 0 && x < mazeClass.width) && (y >= 0 && y < mazeClass.height) && (!visitedCoordinates[x][y]);
	}

	private List<Direction.direction> checkAllDirections(int x, int y, List<List<bool>> visitedCoordinates){
		List<Direction.direction> result = new List<Direction.direction>();
		if (isValidCell(x, y + 1, visitedCoordinates))
			result.Add(Direction.direction.up);
		if (isValidCell(x, y - 1, visitedCoordinates))
			result.Add(Direction.direction.down);
		if (isValidCell(x - 1, y, visitedCoordinates))
			result.Add(Direction.direction.left);
		if (isValidCell(x + 1, y, visitedCoordinates))
			result.Add(Direction.direction.right);
		return result;
	}

    private void digMaze(int x, int y, List<List<bool>> visitedCoordinates){
        visitedCoordinates[x][y] = true;
        List<Direction.direction> validDirections = checkAllDirections(x, y, visitedCoordinates);
		
        if (validDirections.Count > 0){			            
            int randomNumber = Random.Range(0, validDirections.Count);
            Direction.direction headedDirection = validDirections[randomNumber];

            mazeClass.removeWall(x, y, headedDirection);
            digMaze(x + directionToCartesian(headedDirection)[0], y + directionToCartesian(headedDirection)[1], visitedCoordinates);
            digMaze(x, y, visitedCoordinates);
        }
        else
            return;	
    }

    private void generateMaze(){
        mazeClass.addAllWalls();

        List<List<bool>> visitedCoordinates = new List<List<bool>>();
        for (int col = 0; col < mazeClass.width; col++){
            visitedCoordinates.Add(new List<bool>());
            for (int row = 0; row < mazeClass.height; row++){
                visitedCoordinates[col].Add(false);
            }
        }
        digMaze(0,0, visitedCoordinates);
    }

    private void buildMaze()
    {
        for (int col = 0; col < mazeClass.width; col++)
        {
            mazeCells.Add(new List<GameObject>());
            for (int row = 0; row < mazeClass.height; row++)
            {
                Vector3 cellLocation = new Vector3(col * 3, 0, row * 3);
                GameObject newCell = (GameObject)Instantiate(cellPrefab, cellLocation, Quaternion.identity);
                if (mazeClass.wallExists(col, row, Direction.direction.left))
                    newCell.transform.FindChild("LeftWall").gameObject.SetActive(true);
                if (mazeClass.wallExists(col, row, Direction.direction.right))
                    newCell.transform.FindChild("RightWall").gameObject.SetActive(true);
                if (mazeClass.wallExists(col, row, Direction.direction.up))
                    newCell.transform.FindChild("UpWall").gameObject.SetActive(true);
                if (mazeClass.wallExists(col, row, Direction.direction.down))
                    newCell.transform.FindChild("DownWall").gameObject.SetActive(true);
                newCell.transform.parent = mazeClass.transform;
                mazeCells[col].Add(newCell);
            }
        }
    }

    public GameObject getWall(int x, int y, Direction.direction direction) {
        if (mazeClass.wallExists(x, y, direction))
        {
            string wall = "";
            switch (direction)
            {
                case Direction.direction.left:
                    wall = "LeftWall";
                    break;
                case Direction.direction.right:
                    wall = "RightWall";
                    break;
                case Direction.direction.up:
                    wall = "UpWall";
                    break;
                case Direction.direction.down:
                    wall = "DownWall";
                    break;
            }
            return mazeCells[x][y].transform.FindChild(wall).gameObject;
        }
        else
            return null;
    }
}
