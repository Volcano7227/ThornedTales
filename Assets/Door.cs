using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    enum DoorType { Rigth, Left, Top, Bottom }

    [SerializeField] DoorType doorType = DoorType.Rigth;
    [SerializeField] float offset = 2;
    [SerializeField] LayerMask playerLayer;

    Vector3 spawnOffSet;
    public Vector3 SpawnPos => spawnOffSet + transform.position;
    public Door LeadTo { get; private set; } 

    private void Awake()
    {
        switch (doorType)
        {
            case DoorType.Rigth:
                spawnOffSet = new Vector3(-offset,0,0);
                break;
            case DoorType.Left:
                spawnOffSet = new Vector3(offset,0,0);
                break;
            case DoorType.Top:
                spawnOffSet = new Vector3(0,-offset,0);
                break;
            case DoorType.Bottom:
                spawnOffSet = new Vector3(0,offset,0);
                break;
        }
    }
    public void ConnectTo(Door door) => LeadTo = door;

    public void GoThrough(GameObject gameObject)
    {
        Debug.Log($"Going Through door to {LeadTo.name}");
        /* TO-DO
         *Handle TP
         *Handle Cam mouvement
         *Possibly handle Anim or Effect When going to other Room
         */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
            GoThrough(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(SpawnPos, new Vector3(1, 1, 0));
    }
}
