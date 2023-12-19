using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;

    public Transform LeftSide => leftSide;
    public Transform RightSide => rightSide;
}
