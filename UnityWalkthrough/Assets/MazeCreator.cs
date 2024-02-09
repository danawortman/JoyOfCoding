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

        int rowDir = row;
        int colDir = col;

        switch (dir)
        {
            case Direction.North:
                rowDir = row - 1;
                if (rowDir < 0)
                    return 0;

                // check top row and middle row
                for (int j = colDir - 1; j <= colDir + 1; j++)
                {
                    Debug.Log(j);
                    if ((j >= 0) && (rowDir - 1) >= 0
                        && !maze[rowDir - 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && !maze[rowDir, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && (rowDir + 1) >= 0
                        && !maze[rowDir + 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }
                }
                Debug.Log(countNotWalls);
                break;
            case Direction.East:
                // check left column and middle col
                colDir = col + 1;
                // check top row and middle row
                for (int j = colDir - 1; j <= colDir + 1; j++)
                {
                    Debug.Log(j);
                    if ((j >= 0) && (rowDir - 1) >= 0
                        && !maze[rowDir - 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && !maze[rowDir, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && (rowDir + 1) >= 0
                        && !maze[rowDir + 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }
                }
                Debug.Log(countNotWalls);
                break;
            case Direction.South:
                // check bottom row and middle row
                rowDir = row + 1;
                // check top row and middle row
                for (int j = colDir - 1; j <= colDir + 1; j++)
                {
                    Debug.Log(j);
                    if ((j >= 0) && (rowDir - 1) >= 0
                        && !maze[rowDir - 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && !maze[rowDir, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && (rowDir + 1) >= 0
                        && !maze[rowDir + 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }
                }
                Debug.Log(countNotWalls);
                break;
            case Direction.West:
                // check right col and middle col
                colDir = col - 1;
                // check top row and middle row
                for (int j = colDir - 1; j <= colDir + 1; j++)
                {
                    Debug.Log(j);
                    if ((j >= 0) && (rowDir - 1) >= 0
                        && !maze[rowDir - 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && !maze[rowDir, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }

                    if ((j >= 0) && (rowDir + 1) >= 0
                        && !maze[rowDir + 1, j].GetComponent<Square>().isWall)
                    {
                        countNotWalls++;
                    }
                }
                Debug.Log(countNotWalls);
                break;
        }

        maze[rowDir, colDir].GetComponent<MeshRenderer>().material = blackMat;
        maze[rowDir, colDir].GetComponent<Square>().isWall = false;

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
            if (CheckNeighborsForWalls(startRow, startCol, Direction.North) == 1)
            {

            }

            isInitialized = true;
        }
    }
}
