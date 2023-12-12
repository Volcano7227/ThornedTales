using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class GenerateSprite : MonoBehaviour
{
    [SerializeField] int nbImage = 1;
    [SerializeField] float cooldown = 0.1f;
    MeshFilter m_Filter;
    Mesh mesh;
    int frameUV = 0;
    int frameTotal = 0;
    float elapsedTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new();
        mesh.vertices = GenerateVertices(0);
        mesh.triangles = GenerateTriangles();
        mesh.uv = GenerateUV(0);
        mesh.RecalculateNormals();
        m_Filter = GetComponent<MeshFilter>();
        m_Filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > cooldown)
        {
            mesh.vertices = GenerateVertices(frameTotal);
            mesh.uv = GenerateUV(frameUV);
            ++frameTotal;
            frameUV = (frameUV + 1) % nbImage;
            elapsedTime -= cooldown;
        }
        
    }

    private Vector2[] GenerateUV(int frame)
    {
        float delta = 1f / nbImage;
        return new Vector2[]
        {
            new Vector2(frame * delta,0),
            new Vector2(frame * delta,1),
            new Vector2((frame + 1) * delta,1),
            new Vector2((frame + 1) * delta,0),
        };
    }

    private int[] GenerateTriangles()
    {
        return new int[]
        {
            0,1,2,
            0,2,3
        };
    }

    private Vector3[] GenerateVertices(int frame)
    {
        float delta = 0.1f;
        return new Vector3[]
        {
            new Vector3(-frame * delta,0),
            new Vector3(-frame * delta,1),
            new Vector3(-frame * delta + 1,1),
            new Vector3(-frame * delta + 1,0)
        };
    }
}
