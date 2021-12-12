using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    public int curveSegmentCount;
    public int parachuteSegmentCount;
    public float angle = -90f;
    public float arcLenght;
    public float radius;
    public Material parachuteMaterial;
    private Mesh mesh;
    MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3[] normals;
    private int[] triangles;

    //private void Awake()
    //{
    //    //meshFilter = GetComponent<MeshFilter>();
    //    //mesh = new Mesh();
    //    //mesh.name = "Parachute";
    //    //vertices = new Vector3[curveSegmentCount * parachuteSegmentCount];
    //    //normals = new Vector3[curveSegmentCount * parachuteSegmentCount];
    //    //triangles = new int[12 * (parachuteSegmentCount - 1)];
    //    //CreatePoints();
    //}

    public void GenerateMeshFromPoints(List<Vector3> pointList)
    {
        if ( transform.childCount > 0 )
        {
            for ( int i = 0; i < transform.childCount; i++ )
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        foreach ( var center in pointList )
        {
            GenerateMesh(center);
        }
    }

    private void GenerateMesh(Vector3 center)
    {
        mesh = new Mesh();
        mesh.name = "Parachute" + center.ToString();
        GameObject parachuteSegment = new GameObject(center.ToString(), typeof(MeshFilter), typeof(MeshRenderer));
        parachuteSegment.transform.parent = transform;
        meshFilter = parachuteSegment.GetComponent<MeshFilter>();
        parachuteSegment.GetComponent<MeshRenderer>().material = parachuteMaterial;
        parachuteSegment.AddComponent<MeshCollider>().sharedMesh = mesh;

        vertices = new Vector3[curveSegmentCount * parachuteSegmentCount];
        normals = new Vector3[curveSegmentCount * parachuteSegmentCount];
        triangles = new int[12 * (parachuteSegmentCount - 1)];
        CreatePoints(center);
    }
    private void CreatePoints(Vector3 center)
    {
        center += transform.position;
        List<Vector3> points = new List<Vector3>();
        for ( int z = curveSegmentCount - 1; z >= 0; z-- )
        {
            angle = -90f;
            for ( int i = 0; i < parachuteSegmentCount; i++ )
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                points.Add(new Vector3(x, Mathf.Abs(y), z) + center);
                angle += (arcLenght / parachuteSegmentCount);
            }
        }
        SetVertices(points);
    }

    private void SetVertices(List<Vector3> points)
    {
        for ( int i = 0; i < vertices.Length; i++ )
        {
            vertices[i] = points[i];
            normals[i] = points[i];
            normals[i].y = 0;
            normals[i].Normalize();
        }
        SetTriangles(vertices);
    }

    private void SetTriangles(Vector3[] verts)
    {
        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 11;
        //triangles[3] = 0;
        //triangles[4] = 11;
        //triangles[5] = 10;
        int i = 0;
        for ( int j = 0; j < parachuteSegmentCount - 1; j++ )
        {
            triangles[i] = j;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 11;
            triangles[i + 3] = j + 0;
            triangles[i + 4] = j + 11;
            triangles[i + 5] = j + 10;
            triangles[i + 6] = j + 10;
            triangles[i + 7] = j + 11;
            triangles[i + 8] = j;
            triangles[i + 9] = j + 11;
            triangles[i + 10] = j + 1;
            triangles[i + 11] = j;
            i += 12;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.RecalculateNormals();
        mesh.normals = normals;
        meshFilter.mesh = mesh;
        //gameObject.AddComponent<MeshCollider>();
    }

}
