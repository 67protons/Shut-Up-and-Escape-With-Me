using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {    
    public int width = 10;
    public int height = 10;

    public List<List<Cell>> maze = new List<List<Cell>>();    

    void Awake(){
        int i = 0;
        for (int col = 0; col < width; col++){
            maze.Add(new List<Cell>());            
            for (int row = 0; row < height; row++){            
                maze[col].Add(new Cell());                
                i++;
            }
        }        
    }

    // wallExists() takes a cell's (x, y) coordinate and a Direction,
    // returning true if you would see a wall when standing in the cell
    // (x, y) and looking in the given direction, false otherwise.
    // For example, if you had a Maze m and you wanted to know whether
    // there was a wall to the left of cell (3, 4), you would do this:
    //
    //     m.wallExists(3, 4, Direction::left)
    public bool wallExists(int x, int y, Direction.direction direction){
        return maze[x][y].WallAt(direction);
    }


    // addWall() takes a cell's (x, y) coordinate and a Direction and
    // adds a wall to the maze above, below, to the left, or to the
    // right of the given cell.  If there is already a wall there, this
    // function has no effect.  For example, if you wanted to add a wall
    // to the left of cell (4, 2) in a Maze m, you would do this:
    //
    //     m.addWall(4, 2, Direction::left)
    public void addWall(int x, int y, Direction.direction direction){
        maze[x][y].AddWall(direction);
    }


    // removeWall() takes a cell's (x, y) coordinate and a Direction
    // and removes a wall from the maze above, below, to the left, or
    // to the right of the given cell.  If there is no wall there
    // already, this function has no effect.  For example, if you wanted
    // to remove a wall to the right of cell (2, 9) in a Maze m, you
    // would do this:
    //
    //     m.removeWall(2, 9, Direction::right)
    public void removeWall(int x, int y, Direction.direction direction)
    {
        maze[x][y].RemoveWall(direction);
        int xDelta = directionToCartesian(direction)[0];
        int yDelta = directionToCartesian(direction)[1];
        if (isValidCell(x + xDelta, y + yDelta))
        {
            maze[x+xDelta][y+yDelta].RemoveWall(oppositeDirection(direction));
        }        
    }


    // addAllWalls() adds all of the possible walls to a maze, so it
    // has walls surrounding every cell.
    public void addAllWalls(){
        for (int col = 0; col < width; col++){            
            for (int row = 0; row < height; row++){
                maze[col][row].AddWall(Direction.direction.up);
                maze[col][row].AddWall(Direction.direction.down);
                maze[col][row].AddWall(Direction.direction.left);
                maze[col][row].AddWall(Direction.direction.right);
            }
        }
    }


    // removeAllWalls() removes all of the possible walls from a maze,
    // so it has no walls around any of its cells.
    public void removeAllWalls(){
        for (int col = 0; col < width; col++){            
            for (int row = 0; row < height; row++){
                maze[col][row].RemoveWall(Direction.direction.up);
                maze[col][row].RemoveWall(Direction.direction.down);
                maze[col][row].RemoveWall(Direction.direction.left);
                maze[col][row].RemoveWall(Direction.direction.right);
            }
        }
    }

    private bool isValidCell(int x, int y)
    {
        return (x >= 0 && x < width) && (y >= 0 && y < height);
    }

    private List<int> directionToCartesian(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                return new List<int> { 0, 1 };
            case Direction.direction.down:
                return new List<int> { 0, -1 };
            case Direction.direction.left:
                return new List<int> { -1, 0 };
            case Direction.direction.right:
                return new List<int> { 1, 0 };
            default:
                return new List<int>();
        }
    }

    private Direction.direction oppositeDirection(Direction.direction direction)
    {
        switch (direction)
        {
            case Direction.direction.up:
                return Direction.direction.down;
            case Direction.direction.down:
                return Direction.direction.up;
            case Direction.direction.left:
                return Direction.direction.right;
            case Direction.direction.right:
                return Direction.direction.left;
            default:
                return Direction.direction.up;    //should never happen
        }
    }
}
