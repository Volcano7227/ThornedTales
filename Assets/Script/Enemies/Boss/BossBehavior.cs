using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] float cooldown = 2;
    [SerializeField] string poolName;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float movementSpeed;
    [SerializeField] int nbCharge = 2;
    [SerializeField] BaseEnemyBehavior bossHealth;

    GameObject player;
    Transform barrel;
    BossRoom bossRoom;
    Animator animator;
    Vector3 leftBarrelPosition;
    Vector3 rightBarrelPosition;
    Node root;
    GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        barrel = transform.Find("Barrel");
        bossRoom = GetComponentInParent<BossRoom>();
        animator = GetComponent<Animator>();
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
                new Charge(player.transform, gameObject, movementSpeed, bossRoom, nbCharge, animator),
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
