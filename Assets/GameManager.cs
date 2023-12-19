using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void WinLVL()
    {
        SceneManager.LoadScene(2);
    }
    public void LoseLVL()
    {
        SceneManager.LoadScene(3);
    }
}
