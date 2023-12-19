using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] GameObject boss;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;

    public Transform LeftSide => leftSide;
    public Transform RightSide => rightSide;

    public override void ClearRoom()
    {
        base.ClearRoom();
        gameManager.WinLVL();
    }
    public override void PlaceDoor(Vector2Int direction, Door fromDoor, bool bossRoom = false)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.Activate(false);
            topDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.Activate(false);
            bottomDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.Activate(false);
            leftDoor.ConnectTo(fromDoor);
        }

        if (direction == Vector2Int.right)
        {
            rigthDoor.Activate(false);
            rigthDoor.ConnectTo(fromDoor);
        }
    }
    [ContextMenu("LockRoom")]
    public override void LockRoom()
    {
        foreach (Door door in DoorInTheRoom)
        {
            if (door.Active)
                door.LockDoor();
        }
        boss.SetActive(true);
    }
    public override void EnterRoom() => StartCoroutine(MoveToRoom());
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

        if (!Cleared)
            LockRoom();
        Debug.Log("Arrived At Destination");
    }
}
