using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpin : MonoBehaviour
{
    public float spinSpeed = 90.0f;

    void Update()
    {
        //Saat y�n�nde d�nd�rmek i�in
        if(Input.GetKey(KeyCode.R))
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        //Saat y�n�n�n tersine d�nd�rmek i�in
        if (Input.GetKey(KeyCode.F))
            transform.Rotate(Vector3.up, -spinSpeed * Time.deltaTime);

    }
}
