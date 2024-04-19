using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    [SerializeField] public GameObject quad;
    [SerializeField] public Material whiteMat;
    [SerializeField] public Material blackMat;

    const int size = 50;

    GameObject[,] maze = new GameObject[size, size];

    // Stack of squares we've already visited!
    List<Square> squareStack = new List<Square>();

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
                maze[i, j].GetComponent<Square>().row = i;
                maze[i, j].GetComponent<Square>().col = j;
            }
        }
    }

    bool CanBreakWall(Square square)
    {
        int wallCount = 0;

        // If one or fewer neighbors are a wall, break it down
        if (square.row + 1 >= maze.GetLength(0) || !maze[square.row + 1, square.col].GetComponent<Square>().isWall)
            wallCount++;

        if (square.row - 1 < 0 || !maze[square.row - 1, square.col].GetComponent<Square>().isWall)
            wallCount++;

        if (square.col + 1 >= maze.GetLength(1) || !maze[square.row, square.col + 1].GetComponent<Square>().isWall)
            wallCount++;

        if (square.col - 1 < 0 || !maze[square.row, square.col - 1].GetComponent<Square>().isWall)
            wallCount++;

        return wallCount <= 1;
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

    void AddNeighborsToStack(Square square)
    {
        List<Square> neighbors = new List<Square>();

        // Add all the neighbors to the stack
        // If the neighbor is in the maze and a wall, add it
        if (square.row + 1 < maze.GetLength(0) && maze[square.row + 1, square.col].GetComponent<Square>().isWall)
            neighbors.Add(maze[square.row + 1, square.col].GetComponent<Square>());

        if (square.row - 1 >= 0 && maze[square.row - 1, square.col].GetComponent<Square>().isWall)
            neighbors.Add(maze[square.row - 1, square.col].GetComponent<Square>());

        if (square.col + 1 < maze.GetLength(1) && maze[square.row, square.col + 1].GetComponent<Square>().isWall)
            neighbors.Add(maze[square.row, square.col + 1].GetComponent<Square>());

        if (square.col - 1 >= 0 && maze[square.row, square.col - 1].GetComponent<Square>().isWall)
            neighbors.Add(maze[square.row, square.col - 1].GetComponent<Square>());

        while (neighbors.Count > 0)
        {
            int randomSquareIndex = UnityEngine.Random.Range(0, neighbors.Count);
            squareStack.Add(neighbors[0]);
            neighbors.RemoveAt(0);
        }
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

            // Prime the stack by adding the starting square's neighbors
            AddNeighborsToStack(maze[startRow, startCol].GetComponent<Square>());

            // While there are still other choices in the maze
            while (squareStack.Count > 0)
            {
                // Pull off the last square (most recently added)
                int randomSquare = UnityEngine.Random.Range(0, squareStack.Count);

                //Square square = squareStack[randomSquare];
                //squareStack.RemoveAt(randomSquare);

                Square square = squareStack[squareStack.Count - 1];
                squareStack.RemoveAt(squareStack.Count - 1);

                // If this square is not a wall, go to the next one
                if (!square.isWall)
                    continue;

                // If this wall can be broken through, do it
                if (CanBreakWall(square))
                {
                    maze[square.row, square.col].GetComponent<MeshRenderer>().material = blackMat;
                    maze[square.row, square.col].GetComponent<Square>().isWall = false;

                    AddNeighborsToStack(square);
                }
            }

            // At the end of generating the maze, indicate we're done!
            isInitialized = true;
        }
    }
}
