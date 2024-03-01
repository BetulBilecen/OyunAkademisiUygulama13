using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Buillet : MonoBehaviour
{
    public float startSpeed = 75.0f;
    public GameObject objectPrefab;

    private float time_counter = 0f;

    // Update is called once per frame
    void Update()
    {
        time_counter += Time.deltaTime;

        // Belirli bir sürede bir ateþ et
        if (time_counter >= 360f)
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.identity;

            Vector3 local_XDirection = transform.TransformDirection(Vector3.forward);
            Vector3 velocity = local_XDirection * startSpeed;

            GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);

            Rigidbody _rigidbody = newObject.GetComponent<Rigidbody>();
            _rigidbody.velocity = velocity;

            time_counter = 0f;
        }
            
    }
}
