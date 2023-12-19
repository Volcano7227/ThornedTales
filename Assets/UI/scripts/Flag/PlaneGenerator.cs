using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    [SerializeField] int X; // -1
    [SerializeField] int Y; // -1
    private MeshFilter mf;
    Plane plane;

    private void Awake()
    {
        TryGetComponent(out mf);
    }

    private void Start()
    {
        if (mf)
        {
            plane = new Plane(mf.mesh, X, Y);
        }
    }

    void Update()
    {
        plane.CreateVertices(Time.timeSinceLevelLoad);
        plane.AssignMesh();
    }
}


