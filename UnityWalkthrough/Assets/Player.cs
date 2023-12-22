using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Debug.Log(horizontalInput + " " + verticalInput);
        transform.Translate(new Vector3(horizontalInput * Time.deltaTime * speed, 0,
                                        verticalInput * Time.deltaTime * speed));

        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        //Debug.Log(mouseWorldPoint);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(gameObject);
        }
    }
}
