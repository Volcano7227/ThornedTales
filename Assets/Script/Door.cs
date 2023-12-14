using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] int playerLayer;
    [SerializeField] GameObject lockedCounterPart;
    [SerializeField] GameObject ColliderInvisible;
    public GameObject LockedConterPart => lockedCounterPart;
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
    public void Activate()
    {
        gameObject.SetActive(true);
        Active = true;
    }
    public void LockDoor()
    {
        LockedConterPart.SetActive(true);
        gameObject.SetActive(false);
    }
    public void UnlockDoor()
    {
        LockedConterPart.SetActive(false);
        gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(SpawnPos, new Vector3(1, 1, 0));
    }
}
