using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] float cooldown = 2;
    [SerializeField] string poolName;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float movementSpeed;
    GameObject player;
    Transform barrel;
    Vector3 leftBarrelPosition;
    Vector3 rightBarrelPosition;
    Node root;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        barrel = transform.Find("Barrel");
        leftBarrelPosition = new Vector3(-barrel.localPosition.x, barrel.localPosition.y, barrel.localPosition.z);
        rightBarrelPosition = barrel.transform.localPosition;
        SetupTree();
    }

    private void SetupTree()
    {
        /*root = new Sequence(new List<Node>()
        {
            new Sequence(new List<Node>()
            {
                    new IsCooldownOver(cooldown),
                    new Charge(player.transform, gameObject)
            }),
            new Sequence(new List<Node>()
            {
                    new IsCooldownOver(cooldown),
                    new Shoot(player.transform, barrel, projectilePrefab, poolName)
            })
        });*/


        root = new Sequence(new List<Node>()
        {
                new IsCooldownOver(cooldown),
                new Charge(player.transform, gameObject, movementSpeed),
                new Shoot(player.transform, barrel, projectilePrefab, poolName)
        });
        root.SetData("isCharging", false);
    }

    void Update()
    {
        root.Evaluate();
        if (player.transform.position.x - transform.position.x < 0)
            barrel.localPosition = leftBarrelPosition;
        else
            barrel.localPosition = rightBarrelPosition;
    }
}
