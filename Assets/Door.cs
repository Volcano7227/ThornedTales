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
        spawnOffSet = new Vector3(offset.x,offset.y,0);
    }
    public void ConnectTo(Door door) => LeadTo = door;

    public void GoThrough(GameObject gameObject)
    {
        Debug.Log($"Going Through door to {LeadTo.name}");
        gameObject.transform.SetPositionAndRotation(LeadTo.SpawnPos, gameObject.transform.rotation);
        parentRoom.DesactivateRoom();
        LeadTo.parentRoom.ActivateRoom();
        /* TO-DO
         *Possibly handle Anim or Effect When going to other Room
         */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"CollisionDoor{name}");
        if (collision.gameObject.layer == 6)
            GoThrough(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(SpawnPos, new Vector3(1, 1, 0));
    }
}
