using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float xRotation = 0.0f;

    public float mouseSensivity = 100f;     //Kamera hassasiyeti
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Mouse hareketleri için:
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;

        xRotation += mouseY; //Mouse yukarý hareket ettirdikçe ekraný x ekseninde döndürür.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation= Quaternion.Euler(xRotation, 0.0f, 0.0f);
        player.transform.Rotate(Vector3.up * mouseX); 

    }
}
