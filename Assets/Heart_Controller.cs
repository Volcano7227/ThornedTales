using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Controller : MonoBehaviour
{
    public Animator anim;

    public void Pop()
    {
        anim.SetTrigger("Pop");
    }

    public void DestroyHeart()
    {
        Destroy(gameObject);
    }
}
