using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GenerateCube : MonoBehaviour
{
    void Awake()
    {
        Mesh mesh = new();
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.uv = GenerateUV();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }


    // Update is called once per frame
    void Update()
    {

    }
 
    private Vector3[] GenerateVertices()
    {
        return new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,1,0),
            new Vector3(0,1,0),
        };
    }

    private int[] GenerateTriangles()
    {
        return new int[]
        {
            1, 0, 3,
            3, 2, 1
        };
    }

    private Vector2[] GenerateUV() 
    {
        return new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1),
        };
    }
}
