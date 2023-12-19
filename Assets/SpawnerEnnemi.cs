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
    public void SpawnEnnemi( int numberOfEnnemy = 1,bool heavyOnly = false, bool MeleeOnly = false, bool RangeOnly = false)
    {
        int enemmiType;
        int i = 0;
        foreach (Transform transform in spawningPos)
        {
            if (i > numberOfEnnemy) return;

            //1 - Melee, 2 - Tank, 3 - Range
            if (MeleeOnly)
                enemmiType = 0;
            else if (heavyOnly)
                enemmiType = 1;
            else if (RangeOnly)
                enemmiType = 2;
            else
                enemmiType = Random.Next(ennemies.Count-1);

            Instantiate(ennemies[enemmiType], transform.position,Quaternion.Euler(270,0,0),this.transform);
            i++;
        }
        return;
    }
}
