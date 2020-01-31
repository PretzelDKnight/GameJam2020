using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCutting : MonoBehaviour
{
    Mesh mesh;

    Transform cutplane;

    Vector3[] vertices;
    Vector3[] normals;

    Vector3 p1, p2, p3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Prototype Code to figure out
    public void SliceIt()
    {
        Vector3[] vertices = mesh.vertices;

        Transform clone = clone = ((Transform)Instantiate(transform, transform.position, transform.rotation));

        Mesh meshSlice = clone.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] verticesSlice = meshSlice.vertices;

        List<Vector3> verticesSlice2 = new List<Vector3>();

        Mesh cutplanemesh = cutplane.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] cutplanevertices = cutplanemesh.vertices;

        p1 = cutplane.TransformPoint(cutplanevertices[40]);
        p2 = cutplane.TransformPoint(cutplanevertices[20]);
        p3 = cutplane.TransformPoint(cutplanevertices[0]);
        Plane myplane = new Plane(p1, p2, p3);

        for (var i = 0; i < vertices.Length; i++)
        {
            var tmpverts = transform.TransformPoint(vertices[i]); // original object vertices

            if (myplane.GetSide(tmpverts))
            {
                vertices[i] = transform.InverseTransformPoint(new Vector3(tmpverts.x, tmpverts.y - (myplane.GetDistanceToPoint(tmpverts)), tmpverts.z));

                verticesSlice[i] = transform.InverseTransformPoint(new Vector3(tmpverts.x, tmpverts.y, tmpverts.z));
                var v = transform.InverseTransformPoint(new Vector3(tmpverts.x, tmpverts.y, tmpverts.z));
                verticesSlice2.Add(v);
            }
            else
            {
                var v = transform.InverseTransformPoint(new Vector3(tmpverts.x, tmpverts.y - (myplane.GetDistanceToPoint(tmpverts)), tmpverts.z));
                verticesSlice2.Add(v);
            }
        }

        mesh.vertices = verticesSlice;
        mesh.RecalculateBounds();

        meshSlice.vertices = verticesSlice2.ToArray();
        meshSlice.RecalculateBounds();
    }
}
