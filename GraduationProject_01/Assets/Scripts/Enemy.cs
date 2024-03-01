using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    public Transform bulletSpawnPoint;
    public float attackRadius = 118f; // D��man�n sald�r� yar��ap�
    public float verticalLenght = 100f; // D��man�n dikey uzunlu�u
    public float fireSpeed = 15f; // D��man�n ate� h�z�
    public float rotationSpeed = 2f; // D��man�n d�nme h�z�
    public GameObject builletPrefab;

    private float time_counter = 0f; // Ate� etme zamanlay�c�s�




    void Update()
    {
        // D��man�n oyuncuya olan yatay uzakl���n� ve dikey uzakl���n� hesaplamak i�in:
        float hRoad = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                               new Vector3(player.position.x, 0, player.position.z));

        float vRoad = Mathf.Abs(transform.position.y - player.position.y);

        // E�er oyuncu yatayda ve dikeyde sald�r� yar��ap�n�n i�indeyse
        if (hRoad < attackRadius && vRoad < verticalLenght)
        {

            HeadToPlayer();

            time_counter += Time.deltaTime;

            
            if (time_counter >= 6f)
            {
                AtesEt();
                time_counter = 0.0f; 
            }


        }


        // D��man�n oyuncuya do�ru d�nme i�lemi i�in:
        void HeadToPlayer()
        {
            
            Vector3 direction = player.position - transform.position;
            
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);  //D��man�n d�nece�i y�n� ayarlad�k. Burada hem dikey hem de yatay olarak d�necek
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime); //D�nmesi i�in gerekli aral��� ayarlad�k

        }

        void AtesEt()
        {
            Debug.Log("D��man ate� etti!");

            var bullet = Instantiate(builletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * fireSpeed;
        }
    }

}