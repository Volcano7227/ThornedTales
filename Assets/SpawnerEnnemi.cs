using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerEnnemi : MonoBehaviour
{
    [SerializeField] GameObject ennemiMelee;
    [SerializeField] GameObject ennemiTank;
    [SerializeField] GameObject ennemiRange;
    [SerializeField] Transform[] spawningPos;
    System.Random Random = new();
    List<GameObject> ennemies = new();
    private void Awake()
    {
        ennemies.Add(ennemiMelee);
        ennemies.Add(ennemiTank);
        ennemies.Add(ennemiRange);
    }
    [ContextMenu("SpawnEnnemi")]
    public void SpawnEnnemi()
    {
        int random = Random.Next(ennemies.Count);
        foreach (Transform transform in spawningPos)
        {
            Instantiate(ennemies[random], transform.position,transform.rotation);
        }
    }
}
