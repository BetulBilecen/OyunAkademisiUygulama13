using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    public Transform bulletSpawnPoint;
    public float attackRadius = 118f; // Düþmanýn saldýrý yarýçapý
    public float verticalLenght = 100f; // Düþmanýn dikey uzunluðu
    public float fireSpeed = 15f; // Düþmanýn ateþ hýzý
    public float rotationSpeed = 2f; // Düþmanýn dönme hýzý
    public GameObject builletPrefab;

    private float time_counter = 0f; // Ateþ etme zamanlayýcýsý




    void Update()
    {
        // Düþmanýn oyuncuya olan yatay uzaklýðýný ve dikey uzaklýðýný hesaplamak için:
        float hRoad = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                               new Vector3(player.position.x, 0, player.position.z));

        float vRoad = Mathf.Abs(transform.position.y - player.position.y);

        // Eðer oyuncu yatayda ve dikeyde saldýrý yarýçapýnýn içindeyse
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


        // Düþmanýn oyuncuya doðru dönme iþlemi için:
        void HeadToPlayer()
        {
            
            Vector3 direction = player.position - transform.position;
            
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);  //Düþmanýn döneceði yönü ayarladýk. Burada hem dikey hem de yatay olarak dönecek
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime); //Dönmesi için gerekli aralýðý ayarladýk

        }

        void AtesEt()
        {
            Debug.Log("Düþman ateþ etti!");

            var bullet = Instantiate(builletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * fireSpeed;
        }
    }

}