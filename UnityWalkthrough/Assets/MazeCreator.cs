using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    [SerializeField] public GameObject quad;
    [SerializeField] public Material whiteMat;
    [SerializeField] public Material blackMat;

    GameObject[,] maze = new GameObject[10, 10];

    bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        // For each row in the maze
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            // for each column in the row
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = Instantiate(quad);
                maze[i, j].transform.position = new Vector3(i, 0, j);
                maze[i, j].GetComponent<MeshRenderer>().material = whiteMat;
            }
        }
    }

    int CheckNeighborsForWalls(int row, int col, Direction dir)
    {
        int countNotWalls = 0;

        int rowDir = row;           // Row in direction
        int colDir = col;           // Col in direction

        // Set the row and column for the direction we're moving
        switch (dir)
        {
            case Direction.North:
                rowDir = row - 1;
                break;
            case Direction.East:
                colDir = col + 1;
                break;
            case Direction.South:
                rowDir = row + 1;
                break;
            case Direction.West:
                colDir = col - 1;
                break;
        }

        // If we're heading "off" the maze, then return an error
        // TODO: check that we're not off the bottom or right side of the maze
        if (rowDir < 0 || colDir < 0)
            return 0;

        // check top row and middle row
        for (int j = colDir - 1; j <= colDir + 1; j++)
        {
            Debug.Log(j);
            // If the column is "in" the maze and the row is in the maze
            // Explore the top row of neighbors
            if ((j >= 0) && (rowDir - 1) >= 0
                && !maze[rowDir - 1, j].GetComponent<Square>().isWall)
            {
                countNotWalls++;
            }

            // Explore the center row of neighbors
            if ((j >= 0) && !maze[rowDir, j].GetComponent<Square>().isWall)
            {
                countNotWalls++;
            }

            // Explore the bottom row of neighbors
            if ((j >= 0) && (rowDir + 1) >= 0
                && !maze[rowDir + 1, j].GetComponent<Square>().isWall)
            {
                countNotWalls++;
            }
        }

        // If we only have one "opening" (floor) in the set of neighbors
        // Make this cell a floor (break it down!!)
        if (countNotWalls == 1)
        {
            maze[rowDir, colDir].GetComponent<MeshRenderer>().material = blackMat;
            maze[rowDir, colDir].GetComponent<Square>().isWall = false;
        }

        return countNotWalls;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {
            // Grab a random row and col for our starting cell
            int startRow = UnityEngine.Random.Range(0, maze.GetLength(0));
            int startCol = UnityEngine.Random.Range(0, maze.GetLength(1));

            maze[startRow, startCol].GetComponent<MeshRenderer>().material = blackMat;
            maze[startRow, startCol].GetComponent<Square>().isWall = false;

            // Randomly pick a direction
            Direction dir = (Direction)UnityEngine.Random.Range(0, 3);
            Debug.Log(dir);

            // Head north
            for (int i = 0; i < 5; i++)
            {
                dir = (Direction)UnityEngine.Random.Range(0, 3);
                if (CheckNeighborsForWalls(startRow, startCol, dir) == 1)
                {
                    // Set the row and column for the direction we're moving
                    switch (dir)
                    {
                        case Direction.North:
                            startRow--;
                            break;
                        case Direction.East:
                            startCol++;
                            break;
                        case Direction.South:
                            startRow++;
                            break;
                        case Direction.West:
                            startCol--;
                            break;
                    }
                }
            }

            isInitialized = true;
        }
    }
}
