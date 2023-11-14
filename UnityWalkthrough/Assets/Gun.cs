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
        // If the player presses the space key, fire a bullet!
        if (!hasFired && Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
            bullet.GetComponent<Bullet>().Direction = transform.forward;
            hasFired = true;
            timePassed = 0;
        }
        timePassed += Time.deltaTime;
        if (timePassed > bulletTimer)
        {
            timePassed = 0;
            hasFired = false;
        }
    }
}
