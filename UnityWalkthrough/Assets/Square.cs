using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] public bool isWall = true;
    public int row;
    public int col;

    // Have we checked this direction?
    public bool[] directions = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        isWall = true;
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
