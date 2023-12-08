using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletTimer;

    bool hasFired;
    float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        hasFired = false;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses the space key or the left mouse button, fire a bullet!
        if (!hasFired && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            // Create a bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);

            // Aim it forward
            bullet.GetComponent<Bullet>().Direction = transform.forward;

            // Flag that we've just fired a bullet and start a timer to prevent us from firing too often
            hasFired = true;
            timePassed = 0;
        }

        // Add the time since the last frame to the time that has passed
        timePassed += Time.deltaTime;

        // If the time passed is long enough that we can fire another bullet
        // reset the timer
        if (timePassed > bulletTimer)
        {
            timePassed = 0;
            hasFired = false;
        }
    }
}
