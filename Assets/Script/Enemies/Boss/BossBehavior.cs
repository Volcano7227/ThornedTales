using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] float cooldown = 2;
    [SerializeField] string poolName;
    [SerializeField] GameObject projectilePrefab;
    GameObject player;
    Transform agent;
    Transform barrel;
    Vector3 leftBarrelPosition;
    Vector3 rightBarrelPosition;
    Node root;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        barrel = transform.Find("Barrel");
        leftBarrelPosition = new Vector3(-barrel.localPosition.x, barrel.localPosition.y, barrel.localPosition.z);
        rightBarrelPosition = barrel.transform.localPosition;
        SetupTree();
    }

    private void SetupTree()
    {
        root = new Sequence(new List<Node>()
        {
                new IsCooldownOver(cooldown),
                new Charge(player.transform, agent),
                new Shoot(player.transform, barrel, projectilePrefab, poolName)
        });
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
        if (player.transform.position.x - transform.position.x < 0)
            barrel.localPosition = leftBarrelPosition;
        else
            barrel.localPosition = rightBarrelPosition;
    }
}
