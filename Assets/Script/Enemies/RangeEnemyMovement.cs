using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyMovement : MonoBehaviour
{
    [SerializeField] float minDistance = 2.5f;

    NavMeshAgent agent;
    Transform player;
    OldEnemyShoot shootScript;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        shootScript = GetComponent<OldEnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        if (Vector3.Magnitude(agent.transform.position - player.position) < minDistance)
        {
            agent.destination = agent.transform.position;
            shootScript.canShoot = true;
        } else
        {
            shootScript.canShoot = false;
        }
    }
}