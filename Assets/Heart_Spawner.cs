using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Spawner : MonoBehaviour
{
    [SerializeField] GameObject Heart;
    public Player_Controller controller;
    int nbHearts;
    Stack<GameObject> hearts;

    void Awake()
    {
        nbHearts = controller.HitCount;
        hearts = new Stack<GameObject>();

        for (int h = 0; h < nbHearts; ++h)
        {
            GameObject heart = Instantiate(Heart, transform.position + new Vector3(h, 0, 0), transform.rotation);
            heart.transform.parent = transform;
            hearts.Push(heart);
        }
    }

    public void PopHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts.Pop();
            heart.GetComponent<Heart_Controller>().Pop();
        }
    }
}
