using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralShape
{
    // -------------------------------------------------------------------
    // VIDEO : https://www.youtube.com/watch?v=kMr2HiU7DlE
    // -------------------------------------------------------------------

    protected Mesh mesh;
    protected Vector3[] vertices;
    protected int[] triangles;
    protected Vector2[] uvs;

    public ProceduralShape(Mesh mesh)
    {
        this.mesh = mesh;
    }
}

public class Plane : ProceduralShape
{
    private int sizeX;
    private int sizeY;

    public Plane(Mesh mesh, int sizeX, int sizeY) : base(mesh)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        CreateMesh();
    }

    private void CreateMesh()
    {
        CreateVertices(0f);
        CreateTriangles();
        CreateUvs();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
    public void CreateVertices(float time)
    {
        vertices = new Vector3[(sizeX * sizeY)];
        for (int z = 0; z < sizeY; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                float y = Mathf.Sin(time + x);
                vertices[x + z * sizeX] = new Vector3(x, y, z);
            }
        }
    }
    private void CreateTriangles()
    {
        triangles = new int[3 * 2 * (sizeX * sizeY - sizeX - sizeY + 1)];
        int triangleVertexCount = 0;
        for (int vertex = 0; vertex < sizeX * sizeY - sizeX; vertex++)
        {
            if (vertex % sizeX != (sizeX - 1))
            {
                // First triangle
                int A = vertex;
                int B = A + sizeX;
                int C = B + 1;
                triangles[triangleVertexCount] = A;
                triangles[triangleVertexCount + 1] = B;
                triangles[triangleVertexCount + 2] = C;

                // Second triangle
                B += 1;
                C = A + 1;
                triangles[triangleVertexCount + 3] = A;
                triangles[triangleVertexCount + 4] = B;
                triangles[triangleVertexCount + 5] = C;

                triangleVertexCount += 6;
            }
        }
    }
    private void CreateUvs()
    {
        uvs = new Vector2[sizeX * sizeY];
        int uvIndexCounter = 0;
        foreach (Vector3 vertex in vertices)
        {
            uvs[uvIndexCounter] = new Vector2(vertex.x, vertex.z);
            uvIndexCounter++;
        }
    }

    public void AssignMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
    }
}