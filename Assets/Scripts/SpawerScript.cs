using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //Inform the spawners wether they can spawn or not
    public static bool isSpawnable = true;

    //Spawn boundaries
    [SerializeField] int minZoneSpawn;
    [SerializeField] int maxZoneSpawn;

    //Time until spawn
    [SerializeField] int minTimeToSpawn;
    [SerializeField] int maxTimeToSpawn;
    float timeTillSpawn;

    //GameObject to spawn
    [SerializeField] GameObject objectToSpawn;

    //Random generator
    static System.Random random = new System.Random();

    //Object pool
    [SerializeField] GameObject bulletObjectPool;
    ObjectPool objectPoolScript;

    void Start()
    {
        timeTillSpawn = random.Next(minTimeToSpawn, maxTimeToSpawn);
        objectPoolScript = bulletObjectPool.GetComponent<ObjectPool>();
    }

    void Update()
    {
        if (timeTillSpawn >= 0 || !isSpawnable)
            timeTillSpawn -= Time.deltaTime;
        else
            Spawn();
    }

    /// <summary>
    /// Spawn a GameObject between the two boundaries
    /// </summary>
    void Spawn()
    {
        GameObject obj = objectPoolScript.objectPoolInstance.GetPooledObject(objectToSpawn);
        if (obj != null)
        {
            obj.transform.position = new Vector3(random.Next(minZoneSpawn, maxZoneSpawn), transform.position.y, transform.position.z);
            obj.SetActive(true);
        }
        timeTillSpawn = random.Next(minTimeToSpawn, maxTimeToSpawn);
    }
}