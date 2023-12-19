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
    [SerializeField] int nbCharge = 2;
    [SerializeField] BaseEnemyBehavior bossHealth;
    BossRoom bossRoom;
    GameObject player;
    Transform barrel;
    Vector3 leftBarrelPosition;
    Vector3 rightBarrelPosition;
    Node root;
    GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossRoom = GetComponentInParent<BossRoom>();
        barrel = transform.Find("Barrel");
        leftBarrelPosition = new Vector3(-barrel.localPosition.x, barrel.localPosition.y, barrel.localPosition.z);
        rightBarrelPosition = barrel.transform.localPosition;
        SetupTree();
    }

    private void OnEnable()
    {
        gameManager.StartBoss(bossHealth.Hp);
    }
    private void SetupTree()
    {
        root = new Sequence(new List<Node>()
        {
                new WaitCooldown(cooldown),
                new Charge(player.transform, gameObject, movementSpeed, bossRoom, nbCharge),
                new Shoot(barrel, projectilePrefab, poolName)
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
