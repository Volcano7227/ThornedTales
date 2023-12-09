using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShoot : MonoBehaviour
{
    //Inform the spawners wether they can spawn or not
    public bool canShoot = false;

    //Time until shot
    [SerializeField] float shotCooldown;
    float timeTillShot;

    //GameObject to spawn
    [SerializeField] GameObject objectToSpawn;

    void Start()
    {
        timeTillShot = shotCooldown;
    }

    void Update()
    {
        if (canShoot)
        {
            if (timeTillShot >= 0)
                timeTillShot -= Time.deltaTime;
            else
                Shoot();
        }

    }

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    void Shoot()
    {
        GameObject obj = ObjectPool.objectPoolInstance.GetPooledObject(objectToSpawn);
        if (obj != null)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            obj.SetActive(true);
        }
        timeTillShot = shotCooldown;
    }
}
