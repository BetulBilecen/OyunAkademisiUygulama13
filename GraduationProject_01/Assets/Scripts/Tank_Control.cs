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

    public float moveSpeed = 10.5f;       //tankýn hareket hýzý. Varsayýlan olarak 10.5'e ayarlandý
    public float rotationSpeed = 120.0f;    //tankýn dönüþ hýzý.
    public float wheelRotationSpeed = 200.0f;   //Hareket halinde tekerleklerin dönüþ hýzý
    
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;

    private Rigidbody _rigidbody;
    private float moveInput,rotationInput;

    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Konsoldaki rigidbody ile deðiþkenimizi eþleþtirdik

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

        //Hiç düþman kalmadýysa kazandý mesajýný göstermek için:
        if(enemyNumber == 0)
        {
            message.gameObject.SetActive(true);
            next_btn.gameObject.SetActive(true);
        }

        if(oyunDevamMi)
        {
            //Klavyeden tanký hareket ettirmek için:
            moveInput = Input.GetAxis("Vertical");//Klavyedeki yukarý aþaðý oklarýný kullanmamýzý saðlar
            rotationInput = Input.GetAxis("Horizontal");//Klavyedeki sað ve sol ok tuþalrý ile x eksenide hareket saðlar

            RotationWheels(moveInput, rotationInput);

            //Durdurma paneli görünür deðilse zaman akmaya devam eder.
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

    private void FixedUpdate() //Oyun baþladýktan sonra yani start()'dan sonra çalýþacak fonksiyon
    {
        MoveTankObject(moveInput);
        RotateTank(rotationInput);
    }


    void MoveTankObject(float input)
    {
        Vector3 movementDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;    //Time.fixedDeltaTime, FixedUpdate() fonksiyonun periyodu
        _rigidbody.MovePosition(_rigidbody.position+ movementDirection); //Pozisyonu kordinat sistemine göre düzenledik
    }


    //Tanký Döndürmek Ýçin Gerekli Fonksiyon:
    void RotateTank(float input)
    {
        float rotation= input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f); //Soldan saða doðru hareket ettirmek için X ve Z eksenini sýfýrladýk
        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);//y eksenine göre rotasyonu ayarladýk
    }


    //Tekerlekleri Döndürmek Ýçin Gerekli Fonksiyon
    void RotationWheels(float moveInput,float rotationInput)
    {
        float wheelRotation = wheelRotationSpeed * moveInput * Time.deltaTime;

        //Sol tekerleklerin hareketi için:
        foreach(GameObject wheel in leftWheels)
        {
            if (wheel != null)
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        //Sað tekerleklerin hareketi için:
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

    }

   
}
