using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuelletHandler : MonoBehaviour
{
    public float startSpeed = 75.0f;
    public GameObject objectPrefab;

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            SpawnObject();
            
        }
        
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        Vector3 local_XDirection = transform.TransformDirection(Vector3.forward);
        Vector3 velocity = local_XDirection * startSpeed;

        GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);

        Rigidbody _rigidbody=newObject.GetComponent<Rigidbody>();
        _rigidbody.velocity = velocity;

    }

    
}
