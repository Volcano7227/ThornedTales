using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int HitCount;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D coll;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement movement;
    [SerializeField] Heart_Spawner hearts;
    GameManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 13)
        {
            HitCount -= 1;
            hearts.PopHeart();
            coll.enabled = false;
            if (HitCount <= 0)
            {
                HitCount = 0;
                animator.SetTrigger("Death");
            }
            else if (HitCount > 0)
            {
                animator.SetTrigger("Hit");
            }
        }
    }
    public void StartIFrames()
    {
        StartCoroutine(InvulnerabilityTime());
    }

    public void OnPlayerDeath()
    {
        rb.velocity = Vector3.zero;
        movement.enabled = false;
        manager.LoseLVL();
    }
    IEnumerator InvulnerabilityTime()
    {
        yield return new WaitForSeconds(2);
        coll.enabled = true;
    }
}
