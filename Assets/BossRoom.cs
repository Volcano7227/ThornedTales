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

    public override void LockRoom()
    {
        foreach (Door door in DoorInTheRoom)
        {
            if (door.Active)
                door.LockDoor();
        }
        boss.SetActive(true);
    }
    public override void ClearRoom()
    {
        base.ClearRoom();
        gameManager.WinLVL();
    }
    public override void PlaceDoor(Vector2Int direction, Door fromDoor)
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
}
