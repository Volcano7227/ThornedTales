using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] int playerLayer;

    Room parentRoom;
    Vector3 spawnOffSet;

    public Vector3 SpawnPos => spawnOffSet + transform.position;
    public Door LeadTo { get; private set; }

    private void Awake()
    {
        parentRoom = GetComponentInParent<Room>();
        spawnOffSet = new Vector3(offset.x, offset.y, 0);
    }
    public void ConnectTo(Door door) => LeadTo = door;

    public void GoThrough(GameObject player)
    {
        LeadTo.parentRoom.MoveCamToRoom();
        player.transform.SetPositionAndRotation(LeadTo.SpawnPos, player.transform.rotation);
        /* TO-DO
         *Possibly handle Anim or Effect When going to other Room (Doing)
         */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            GoThrough(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(SpawnPos, new Vector3(1, 1, 0));
    }
}
