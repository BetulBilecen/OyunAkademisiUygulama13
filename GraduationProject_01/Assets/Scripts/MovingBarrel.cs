using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrel : MonoBehaviour
{
    public Transform namlu; 

    public float rotationSpeed = 5f;
    public float maxAngle = -30f;
    public float minAngle = 0; 

    void Update()
    {
        // N tuþuna basýlý olduðu sürece namluyu hareket ettir
        if (Input.GetKey(KeyCode.N))
        {
            float rotationInput = Input.GetAxis("Vertical");
            RotateNamlu(rotationInput);
        }
    }

    void RotateNamlu(float input)
    {
        
        float currentAngle = namlu.localRotation.eulerAngles.z;
        float newAngle = currentAngle - input * rotationSpeed * Time.deltaTime;

        newAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);
        namlu.localRotation = Quaternion.Euler(newAngle, 0f, 0f);
    }
}
