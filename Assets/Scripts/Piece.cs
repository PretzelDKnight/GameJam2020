using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int ID;
    const float rate = 1.5f;
    const float errorMargin = 0.02f;

    public bool used = false;
    public bool turning = false;
    public static int neighbourCount = 3;
    public LayerMask mask;


    float time;
    float dir;

    List<Piece> nPieces = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
        AddNeighbours();
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
            if (Physics.Raycast(transform.position, dir[i], out hit, 0.6f, mask))
            {
                Piece p = hit.transform.GetComponent<Piece>();
                nPieces.Add(p);

                Debug.Log(this.ID + " " + p.ID); 
            }
        }

        Debug.Log("Temp list : " + nPieces.Count);
    }

    public bool CheckSolve()
    {
        int checks = 0;

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
                for (int j = 0; j < nPieces.Count; j++)
                {
                    if (hit.transform.GetComponent<Piece>() == nPieces[j])
                    {
                        checks++;
                    }
                }
            }
        }

        if (checks == 3)
        {
            return true;
        }
        else
            return false;
    }
}
