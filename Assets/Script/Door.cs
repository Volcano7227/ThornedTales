using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] int playerLayer;
    [SerializeField] GameObject bossCounterPart;
    [SerializeField] GameObject bossCounterPartLocked;
    [SerializeField] GameObject lockedCounterPart;
    [SerializeField] GameObject ColliderInvisible;
    public GameObject LockedCounterPart => lockedCounterPart;
    public GameObject BossCounterPart => bossCounterPart;
    public GameObject BossCounterPartLocked => bossCounterPartLocked;
    Room parentRoom;
    Vector3 spawnOffSet;
    public bool Active { get; private set; }
    public Vector3 SpawnPos => spawnOffSet + transform.position;
    public Door LeadTo { get; private set; }

    private void Awake()
    {
        parentRoom = GetComponentInParent<Room>();
        spawnOffSet = new Vector3(offset.x, offset.y, 0);
    }
    private void OnEnable()
    {
        ColliderInvisible.SetActive(false);
    }
    public void ConnectTo(Door door) => LeadTo = door;

    public void GoThrough(GameObject player)
    {
        LeadTo.parentRoom.EnterRoom();
        player.transform.SetPositionAndRotation(LeadTo.SpawnPos, player.transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            GoThrough(collision.gameObject);
    }
    public void Activate(bool bossDoor = false)
    {
        if (bossDoor)
            bossCounterPart.SetActive(true);
        else
            gameObject.SetActive(true);
        Active = true;
    }
    public void LockDoor(bool bossDoor = false)
    {
        if (bossDoor)
        {
            BossCounterPartLocked.SetActive(true);
            BossCounterPart.SetActive(false);
        }
        else
        {
            LockedCounterPart.SetActive(true);
            gameObject.SetActive(false);
        }
        ColliderInvisible.SetActive(true);
    }
    public void UnlockDoor(bool bossDoor = false)
    {
        if (bossDoor)
        {
            BossCounterPartLocked.SetActive(false);
            BossCounterPart.SetActive(true);
        }
        else
        {
            LockedCounterPart.SetActive(false);
            gameObject.SetActive(true);
        }
        ColliderInvisible.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(SpawnPos, new Vector3(1, 1, 0));
    }
}
