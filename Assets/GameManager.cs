using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RoomManager roomManager;
    private void Awake()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }
    private void Start()
    {
        LoadLVL();
    }
    public void  LoadLVL()
    {
        roomManager.StartGeneration();
    }
}
