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

    // Stack of squares we've already visited!
    List<Square> squareStack = new List<Square>();

    bool isInitialized = false;
    int size = 10;

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

    int CheckNeighborsForWalls(Square square, int row, int col, Direction dir)
    {
        Debug.Log("At: " + row + " " + col + " Heading: " + dir);

        int countNotWalls = 0;

        int rowDir = row;           // Row in direction
        int colDir = col;           // Col in direction
        int farRowDir = row;        // Far row
        int farColDir = col;        // Far col

        // Set the row and column for the direction we're moving
        switch (dir)
        {
            case Direction.North:
                rowDir = row - 1;
                farRowDir = rowDir - 1;
                square.GetComponent<Square>().directions[(int)Direction.South] = true;
                break;
            case Direction.East:
                colDir = col + 1;
                square.GetComponent<Square>().directions[(int)Direction.West] = true;
                break;
            case Direction.South:
                rowDir = row + 1;
                farRowDir = rowDir + 1;
                square.GetComponent<Square>().directions[(int)Direction.North] = true;
                break;
            case Direction.West:
                colDir = col - 1;
                square.GetComponent<Square>().directions[(int)Direction.East] = true;
                break;
        }

        // If we're heading "off" the maze, then return an error
        // TODO: check that we're not off the bottom or right side of the maze
        if (rowDir < 1 || colDir < 1 || rowDir >= maze.GetLength(0) - 1 || colDir >= maze.GetLength(1) - 1)
            return -1;

          // If we're heading north or south
        if (dir == Direction.North || dir == Direction.South)
        {
            countNotWalls = CheckRow(countNotWalls, colDir, rowDir);
            countNotWalls = CheckRow(countNotWalls, colDir, farRowDir);
        }

        // If we're heading east or west
        else // if (dir == Direction.East || dir == Direction.West)
        {
            countNotWalls = CheckColumn(countNotWalls, rowDir, farColDir);
            countNotWalls = CheckColumn(countNotWalls, rowDir, colDir);
        }

        Debug.Log(countNotWalls);
        return countNotWalls;
    }

    // Check the column for walls
    private int CheckColumn(int countNotWalls, int rowDir, int farColDir)
    {
        // Check the "far col"
        for (int j = rowDir - 1; j <= rowDir + 1; j++)
        {
            if ((j >= 0) && (farColDir) >= 0
                && !maze[farColDir, j].GetComponent<Square>().isWall)
            {
                countNotWalls++;
            }
        }

        return countNotWalls;
    }

    // Check the row for walls
    private int CheckRow(int countNotWalls, int colDir, int farRowDir)
    {
        // Check the "far row"
        for (int j = colDir - 1; j <= colDir + 1; j++)
        {
            if ((j >= 0) && (farRowDir) >= 0
                && !maze[farRowDir, j].GetComponent<Square>().isWall)
            {
                countNotWalls++;
            }
        }

        return countNotWalls;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {
            // Grab a random row and col for our starting cell
            int startRow = UnityEngine.Random.Range(1, maze.GetLength(0) - 1);
            int startCol = UnityEngine.Random.Range(1, maze.GetLength(1) - 1);

            maze[startRow, startCol].GetComponent<MeshRenderer>().material = blackMat;
            maze[startRow, startCol].GetComponent<Square>().isWall = false;

            squareStack.Add(maze[startRow, startCol].GetComponent<Square>());

            // While there are still other choices in the maze
            while (squareStack.Count > 0)
            {
                // Pull off the last square
                Square square = squareStack[squareStack.Count - 1];
                squareStack.RemoveAt(squareStack.Count - 1);

                // Check if we're in a deadend
                if (!(square.GetComponent<Square>().directions[(int)Direction.North]
                    && square.GetComponent<Square>().directions[(int)Direction.South]
                    && square.GetComponent<Square>().directions[(int)Direction.East]
                    && square.GetComponent<Square>().directions[(int)Direction.West]))
                {
                    break;
                }

                //if (squareStack.Count == 0)
                //    break;

                // Randomly pick a direction
                Direction dir = (Direction)UnityEngine.Random.Range(0, 3);
                Debug.Log(dir);

                // For each of the 3 directions
                for (int i = 0; i < 4; i++)
                {


                    // Repeat until we find a direction we haven't tried
                    //do
                    //{
                    //dir = (Direction)UnityEngine.Random.Range(0, 3);
                    //} while (square.GetComponent<Square>().directions[(int)dir]);
                    dir = (Direction)i;
                
                    // Indicate we've already checked that direction
                    square.GetComponent<Square>().directions[(int)dir] = true;


                    // If that direction is clear, head that way
                    if (CheckNeighborsForWalls(square, startRow, startCol, dir) == 0)
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
                        square.GetComponent<MeshRenderer>().material = blackMat;
                        square.GetComponent<Square>().isWall = false;
                        
                        squareStack.Add(maze[startRow, startCol].GetComponent<Square>());

                        break;
                    }                
                }
            }

            // At the end of generating the maze, indicate we're done!
            isInitialized = true;
        }
    }
}
