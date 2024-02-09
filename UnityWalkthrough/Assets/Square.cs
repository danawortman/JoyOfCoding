using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] public bool isWall = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of Square");
        isWall = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
