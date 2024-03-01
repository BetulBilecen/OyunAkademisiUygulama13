using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TargetDestruction : MonoBehaviour
{
    public string objTagi = "Enemy";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(objTagi))
        {
            Destroy(collision.gameObject); //Nesneyi yok eder
            Destroy(gameObject);    //Mermiyi yok eder
        }
    }
}
