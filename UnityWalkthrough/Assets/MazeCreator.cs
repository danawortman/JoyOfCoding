using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    [SerializeField] public GameObject quad;
    [SerializeField] public Material whiteMat;
    [SerializeField] public Material blackMat;

    GameObject[,] maze = new GameObject[10,10];

    // Start is called before the first frame update
    void Start()
    {
        // For each row in the maze
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            // for each column in the row
            for(int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i,j] = Instantiate(quad);
                maze[i,j].transform.position = new Vector3(i, 0, j);
                maze[i,j].GetComponent<MeshRenderer>().material = whiteMat;
            }
        }

        int startRow = UnityEngine.Random.Range(0, maze.GetLength(0));
        int startCol = UnityEngine.Random.Range(0, maze.GetLength(1));

        maze[startRow,startCol].GetComponent<MeshRenderer>().material = blackMat;
        maze[startRow,startCol].GetComponent<Square>().isWall = false;

        if (CheckNeighborsForWalls(startRow - 1, startCol, Direction.North) == 0)
        {
            maze[startRow -1,startCol].GetComponent<MeshRenderer>().material = blackMat;
            maze[startRow -1,startCol].GetComponent<Square>().isWall = false;
        }
    }

    int CheckNeighborsForWalls(int row, int col, Direction dir)
    {
        int countNbrs = 0;

        switch(dir)
        {
            case Direction.North:
                // check top row and middle row
                for (int j = col - 1; j >= col + 1; j++)
                {
                    if ((j >= 0) && (row - 1) >= 0 
                        && maze[row - 1,j].GetComponent<Square>().isWall)
                    {
                        countNbrs++;
                    }
                    if ((j >= 0) && maze[row,j].GetComponent<Square>().isWall)
                    {
                        countNbrs++;
                    }
                }
                break;
            case Direction.East:
                // check left column and middle col
                break;

            case Direction.South:
                // check bottom row and middle row
                break;

            case Direction.West:
                // check right col and middle col
                break;
        }

        return countNbrs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
