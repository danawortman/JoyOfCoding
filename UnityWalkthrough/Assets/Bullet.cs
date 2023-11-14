using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float speed = 3.0f;

    private Vector3 direction;

    public Vector3 Direction { get => direction; set => direction = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   // new Vector3(x, y, z)
        //transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
        transform.Translate(Time.deltaTime * speed * Direction);
    }
}
