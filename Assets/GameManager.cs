using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    RoomManager RoomManager;
    [SerializeField] BossHealthComponent BossHealthComponent;
    private void Awake()
    {
        RoomManager = FindObjectOfType<RoomManager>();
    }
    private void Start()
    {
        LoadLVL();
    }
    public void StartBoss(int nbPV)
    {
        BossHealthComponent.Show();
        BossHealthComponent.SetMaxHealth(nbPV);
    }
    public void  LoadLVL()
    {
        RoomManager.StartGeneration();
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
