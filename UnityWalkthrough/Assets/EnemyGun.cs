using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

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
        if (!hasFired)
        { 
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
            bullet.GetComponent<Bullet>().Direction = transform.forward;
            hasFired = true;
            timePassed = 0;
        }

        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            timePassed = 0;
            hasFired = false;
        }
    }
}
