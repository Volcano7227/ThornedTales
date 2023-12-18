using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShoot : MonoBehaviour
{
    //Time until shot
    [SerializeField] float shotCooldown;
    float timeTillShot;

    //GameObject to spawn
    [SerializeField] GameObject objectToSpawn;

    //Object pool
    ObjectPool objectPoolScript;

    void Awake()
    {
        timeTillShot = shotCooldown;
        GameObject objectPool = GameObject.FindGameObjectWithTag("MageBulletPool");
        objectPoolScript = objectPool.GetComponent<ObjectPool>();
    }

    void Update()
    {
            if (timeTillShot >= 0)
                timeTillShot -= Time.deltaTime;
            else
                Shoot();
    }

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    void Shoot()
    {
        GameObject obj = objectPoolScript.objectPoolInstance.GetPooledObject(objectToSpawn);
        if (obj != null)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            obj.SetActive(true);
        }
        else
            print("No bullet found.");
        timeTillShot = shotCooldown;
    }
}
