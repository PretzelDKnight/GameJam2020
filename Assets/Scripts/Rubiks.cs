﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rubiks : MonoBehaviour
{
    bool working;
    public Vector3 camDir = Vector3.forward;
    float mouseX;
    float mouseY;

    public float slowDownRate = 1;
    public float reverseVelocityRate = 1;

    Rigidbody rb;
    Vector3 currentAV;
    float time;

    Vector3 direction;

    public List<Piece> pieces;

    public bool turnable = false;
    public float turningDist = -0.25f;

    Vector3 rotAxis;

    Vector3[] directionList;

    float rotationAmt;

    [SerializeField] GameObject endUp;
    [SerializeField] AudioSource snapSource;

    // Start is called before the first frame update
    void Start()
    {
        directionList = new Vector3[6];
        CheckDir();
        working = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDir();
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        direction = new Vector3(mouseY, -mouseX, 0).normalized;

        if (Turnable())
        {
            turnable = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CreatePiece();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                DestroyPiece();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Rotate();
                time = 0;
            }
        }
        else
        {
            turnable = false;
        }

        if (CheckSolve())
        {
            Debug.Log("<color=red> Im Solved!!!! </color>");
            endUp.SetActive(true);
            StartCoroutine(Exit());
        }
    }

    void Rotate()
    {
        if (turnable)
        {
            foreach (var item in pieces)
            {
                item.Rotate(transform.position, rotAxis, -mouseX + mouseY);
            }

            rotationAmt += -mouseX + mouseY;
        }
    }

    void Rotate(float val)
    {
        if (turnable)
        {
            foreach (var item in pieces)
            {
                if (item.used)
                {
                    item.transform.RotateAround(transform.position, rotAxis, val);
                }
            }
        }
    }

    void DestroyPiece()
    {
        SetPiece();

        Debug.Log("Destroying Piece");
        foreach (var item in pieces)
        {
            if (item.used)
            {
                item.used = false;
            }
        }
        working = false;
    }

    void CreatePiece()
    {
        working = true;
        Debug.Log("Creating Piece");
        foreach (var item in pieces)
        {
            if (item.transform.position.z > 0.15f)
            {
                item.used = true;
            }
        }

        SetAxis();
    }

    bool Turnable()
    {
        int i = 0;

        foreach (var item in pieces)
        {
            if (item.transform.position.z < turningDist)
            {
                i++;
            }
        }

        if (i == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetAxis()
    {
        rotAxis = Vector3.zero;

        float dot = 0;
        float highestDot = 0;

        foreach (var axisDir in directionList)
        {
            dot = Vector3.Dot(axisDir, camDir);

            if (dot > 0 && dot > highestDot)
            {
                rotAxis = axisDir;
                highestDot = dot;
            }
        }

        rotationAmt = 0;
    }

    void CheckDir()
    {
        directionList[0] = transform.forward;
        directionList[1] = -transform.forward;
        directionList[2] = transform.right;
        directionList[3] = -transform.right;
        directionList[4] = transform.up;
        directionList[5] = -transform.up;
    }

    // Need to change
    void SetPiece()
    {
        snapSource.Play();
        float mod = rotationAmt % 90;

        if (mod >= 45 )
        {
            Rotate(90 - mod);
        }
        else if (mod < 45)
        {
            Rotate(- mod);
        }
    }

    public bool CheckSolve()
    {
        int i = 0;
        foreach (var item in pieces)
        {
            if (!item.CheckSolve())
            {
                i++;
            }
        }

        if (i != 0)
            return false;
        else
            return true;
    }


    IEnumerator Exit()
    {
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
