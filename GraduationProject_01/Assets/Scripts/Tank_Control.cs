using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tank_Control : MonoBehaviour
{
    public Button again_btn, next_btn;
    public TextMeshProUGUI message, lostMessage;
    public GameObject Eneyms_obj, panel;
    public GameObject heart1, heart2, heart3, heart4, heart5;
    public static int health;

    bool oyunDevamMi = true;

    float timeCounter = 400;
    public Text time;

    public float moveSpeed = 10.5f;       //tank�n hareket h�z�. Varsay�lan olarak 10.5'e ayarland�
    public float rotationSpeed = 120.0f;    //tank�n d�n�� h�z�.
    public float wheelRotationSpeed = 200.0f;   //Hareket halinde tekerleklerin d�n�� h�z�
    
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;

    private Rigidbody _rigidbody;
    private float moveInput,rotationInput;

    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Konsoldaki rigidbody ile de�i�kenimizi e�le�tirdik

        health = 5;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        heart4.gameObject.SetActive(true);
        heart5.gameObject.SetActive(true);

    }


    private void Update()
    {
        int enemyNumber = Eneyms_obj.transform.childCount;

        //Hi� d��man kalmad�ysa kazand� mesaj�n� g�stermek i�in:
        if(enemyNumber == 0)
        {
            message.gameObject.SetActive(true);
            next_btn.gameObject.SetActive(true);
        }

        if(oyunDevamMi)
        {
            //Klavyeden tank� hareket ettirmek i�in:
            moveInput = Input.GetAxis("Vertical");//Klavyedeki yukar� a�a�� oklar�n� kullanmam�z� sa�lar
            rotationInput = Input.GetAxis("Horizontal");//Klavyedeki sa� ve sol ok tu�alr� ile x eksenide hareket sa�lar

            RotationWheels(moveInput, rotationInput);

            //Durdurma paneli g�r�n�r de�ilse zaman akmaya devam eder.
            if(!panel.activeSelf)
            {
                timeCounter -= Time.deltaTime;
                time.text = (int)timeCounter + "";
            }


            switch (health)
            {
                case 1:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    heart4.gameObject.SetActive(false);
                    heart5.gameObject.SetActive(false);
                    break;

                case 2:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(false);
                    heart4.gameObject.SetActive(false);
                    heart5.gameObject.SetActive(false);
                    break;

                case 3:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    heart4.gameObject.SetActive(false);
                    heart5.gameObject.SetActive(false);
                    break;

                case 4:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    heart4.gameObject.SetActive(true);
                    heart5.gameObject.SetActive(false);
                    break;

                case 5:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    heart4.gameObject.SetActive(true);
                    heart5.gameObject.SetActive(true);
                    break;

                default:
                    heart1.gameObject.SetActive(false);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    heart4.gameObject.SetActive(false);
                    heart5.gameObject.SetActive(false);
                    Time.timeScale = 0;

                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;

                    lostMessage.gameObject.SetActive(true);
                    again_btn.gameObject.SetActive(true);
                    break;

            }

        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            if(enemyNumber !=0)
            {
                lostMessage.gameObject.SetActive(true);
                again_btn.gameObject.SetActive(true);

                
            }
        }

        

        if(timeCounter<0)
        {
            oyunDevamMi = false;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Buillet"))
        {
            health -= 1;
            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate() //Oyun ba�lad�ktan sonra yani start()'dan sonra �al��acak fonksiyon
    {
        MoveTankObject(moveInput);
        RotateTank(rotationInput);
    }


    void MoveTankObject(float input)
    {
        Vector3 movementDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;    //Time.fixedDeltaTime, FixedUpdate() fonksiyonun periyodu
        _rigidbody.MovePosition(_rigidbody.position+ movementDirection); //Pozisyonu kordinat sistemine g�re d�zenledik
    }


    //Tank� D�nd�rmek ��in Gerekli Fonksiyon:
    void RotateTank(float input)
    {
        float rotation= input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f); //Soldan sa�a do�ru hareket ettirmek i�in X ve Z eksenini s�f�rlad�k
        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);//y eksenine g�re rotasyonu ayarlad�k
    }


    //Tekerlekleri D�nd�rmek ��in Gerekli Fonksiyon
    void RotationWheels(float moveInput,float rotationInput)
    {
        float wheelRotation = wheelRotationSpeed * moveInput * Time.deltaTime;

        //Sol tekerleklerin hareketi i�in:
        foreach(GameObject wheel in leftWheels)
        {
            if (wheel != null)
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //Sa� tekerleklerin hareketi i�in:
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

    }

   
}
