using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    const float rate = 1.5f;
    const float errorMargin = 0.02f;

    public bool used = false;
    public bool turning = false;
    public static int neighbourCount = 3;

    float[] distances = new float[neighbourCount];

    float time;
    float dir;

    List<Piece> nPieces = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        AddNeighbours();
        for (int i = 0; i < neighbourCount; i++)
        {
            distances[i] = Vector3.Distance(transform.position, nPieces[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate(Vector3 point, Vector3 axis, float val)
    {
        if (used)
        {
            transform.RotateAround(point, axis, val);
        }
    }

    public bool CheckSolve()
    {
        int n = 0;
        float temp;

        for (int i = 0; i < neighbourCount; i++)
        {
            temp = Vector3.Distance(transform.position, nPieces[i].transform.position);

            if (Mathf.Abs(temp - distances[i]) > errorMargin)
            {
                n++;
            }
        }

        if (n == 0)
            return true;
        else
            return false;
    }

    void AddNeighbours()
    {
        Vector3[] dir = new Vector3[6];
        dir[0] =  transform.forward;
        dir[1] =  -transform.forward;
        dir[2] =  transform.right;
        dir[3] =  -transform.right;
        dir[4] =  transform.up;
        dir[5] =  -transform.up;

        RaycastHit hit = new RaycastHit();

        for (int i = 0; i < dir.Length; i++)
        {
            if (Physics.Raycast(transform.position, dir[i], out hit, 0.6f))
            {
                nPieces.Add(hit.transform.GetComponent<Piece>());
            }
        }

        Debug.Log("Temp list : " + nPieces.Count);
    }
}
