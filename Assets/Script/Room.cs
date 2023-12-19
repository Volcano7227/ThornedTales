using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//From / Inspired From : https://www.youtube.com/watch?v=eK2SlZxNjiU
[RequireComponent(typeof(SpawnerEnnemi))]
public class Room : MonoBehaviour
{
    [SerializeField] Door topDoor;
    [SerializeField] Door bottomDoor;
    [SerializeField] Door leftDoor;
    [SerializeField] Door rigthDoor;
    [SerializeField] Transform AnchorCam;
    public RoomType RoomType;

    public int Difficulty = 1;

    SpawnerEnnemi spawnerEnnemi;
    public float timeForTransitionCam { get; private set; } = 1.5f;
    public Vector2Int RoomIndex { get; set; }
    public Door TopDoor => topDoor;
    public Door BottomDoor => bottomDoor;
    public Door LeftDoor => leftDoor;
    public Door RigthDoor => rigthDoor;

    Camera mainCamera;
    PlayerMovement playerMovement;
    Door[] DoorInTheRoom;
    bool Cleared;
    private void Awake()
    {
        spawnerEnnemi = GetComponent<SpawnerEnnemi>();  
        DoorInTheRoom = new Door[] {TopDoor,BottomDoor,LeftDoor,RigthDoor};
        playerMovement = FindObjectOfType<PlayerMovement>();
        mainCamera = Camera.main;
    }
    public void PlaceDoor(Vector2Int direction, Door fromDoor)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.Activate();
            topDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.Activate();
            bottomDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.Activate();
            leftDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.right)
        {
            rigthDoor.Activate();
            rigthDoor.ConnectTo(fromDoor);
        }
    }
    [ContextMenu("LockRoom")]
    public void LockRoom()
    {
        foreach (Door door in DoorInTheRoom)
        {
            if (door.Active)
                door.LockDoor();
        }
        switch (RoomType)
        {
            case RoomType.Range:
                spawnerEnnemi.SpawnEnnemi(Difficulty, false, false,true);
                break;
            case RoomType.Melee:
                spawnerEnnemi.SpawnEnnemi(Difficulty,false,true);
                break;
            case RoomType.Tank:
                spawnerEnnemi.SpawnEnnemi(Difficulty,true);
                break;
            case RoomType.Mix:
                spawnerEnnemi.SpawnEnnemi(Difficulty);
                break;
        }
    }
    [ContextMenu("ClearRoom")]
    public void ClearRoom()
    {
        Cleared = true;
        foreach (Door door in DoorInTheRoom)
        {
            if(door.Active)
                door.UnlockDoor();
        }
    }
    public void EnterRoom() => StartCoroutine(MoveToRoom());
    IEnumerator MoveToRoom()
    {
        playerMovement.FreezeMovement();
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(mainCamera.transform.position, AnchorCam.position);
        float speed = journeyLength / timeForTransitionCam;

        while (mainCamera.transform.position != AnchorCam.position)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, AnchorCam.position, fractionOfJourney);
            yield return null;
        }
        playerMovement.UnFreezeMovement();
        
        if(!Cleared)
            LockRoom();
        Debug.Log("Arrived At Destination");
    }

}
public enum RoomType { Melee,Range,Tank,Mix}
