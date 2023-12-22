using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;

    // Angular speed in radians per sec.
    public float speed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        // Compute the direction to face the player
 /*       Vector3 targetVec = (transform.position - player.transform.position).normalized;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Calculate a rotation that aims us toward the opponent
        Quaternion rotation = Quaternion.LookRotation(-targetVec);

        // Rotate toward the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50 * singleStep);

        // Calculate the distance to the opponent
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move the enemy away from the player if he's too close
        if (distance < 6)
        {
            transform.Translate(-transform.forward * Time.deltaTime * speed, Space.World);
        }
        // Move the enemy toward the player if he's too far
        else if (distance > 7)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }
        // Move right to "circle" the player if he's at a good distance
        else
        {
            transform.Translate(transform.right * Time.deltaTime * speed, Space.World);
        }
 */
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(gameObject);
            FindObjectOfType<Score>().playerScore++;
            Destroy(collision.gameObject);
        }
    }
}
