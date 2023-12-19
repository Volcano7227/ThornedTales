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
    }
    public override void ClearRoom()
    {
        base.ClearRoom();
        gameManager.WinLVL();
    }
}
