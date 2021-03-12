using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class CharactorMove : MonoBehaviour
{
    // character movement variable
    float MovePower = 30f; 
    float JumpPower = 5f;

    bool isJumping;
    bool isGround;
    bool isBattling = false;
    bool isDead = false;

    Rigidbody Rigid;

    Collider col;

    // camera and character sight variable
    float sightSensitivity = 3f;
    float sightRotation = 60f;
    float CurrentSightRotationX;
    public Camera cam;

    // character mp hp stamina variable
    float recoverMpTime = 0;
    float recoverHpTime = 0;
    float recoverStaminaTime = 0;
    float BattleOutTime = 0;

    public Slider Stamina_Bar;
    public Slider HP_Bar;
    public Slider MP_Bar;

    

    // Start is called before the first frame update
    void Start()
    {
        Rigid = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        Move();
        Jump();
        Charater_Rotation();
        Sight_Rotation();
        Run();
        HpandMpRecover();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 movingX = transform.right * moveX;
        Vector3 movingY = transform.forward * moveY;

        Vector3 velocity = (movingX + movingY).normalized * MovePower;

        Rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    void Jump()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
        
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                if (isJumping)
                {
                    Rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                    isJumping = false;
                }
            }
        }
    }

    void Charater_Rotation()
    {
        float charactorRotation = Input.GetAxisRaw("Mouse X");

        Vector3 charactorRoatationY = new Vector3(0, charactorRotation, 0) * sightSensitivity;
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(charactorRoatationY));
    }

    void Sight_Rotation()
    {
        float xSight = Input.GetAxisRaw("Mouse Y");
        float SightRotationX = xSight * sightSensitivity;

        CurrentSightRotationX -= SightRotationX;
        CurrentSightRotationX = Mathf.Clamp(CurrentSightRotationX, -sightRotation, sightRotation);

        cam.transform.localEulerAngles = new Vector3(CurrentSightRotationX, 0, 0);
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                if ( Stamina_Bar.value > 0)
                {
                    MovePower = 50f;
                    Stamina_Bar.value -= 0.0012f;
                    recoverStaminaTime = 0;
                }
                else if ( Stamina_Bar.value == 0)
                {
                    MovePower = 30f;
                    recoverStaminaTime = 0;
                }
            }
        }
        else
        {
            MovePower = 30f;

            if ( Stamina_Bar.value > 0)
            {
                recoverStaminaTime += Time.deltaTime;
            }
            else if ( Stamina_Bar.value == 0)
            {
                MovePower = 20f;
                recoverStaminaTime += Time.deltaTime;
            }
        }

        if ( recoverStaminaTime > 2)
        {
            Stamina_Bar.value += Time.deltaTime;
        }
    }

    void HpandMpRecover()
    {
        if (!isBattling && !isDead)
        {
            if ( HP_Bar.value < 1)
            {
                recoverHpTime += Time.deltaTime * 0.1f;
            }
            
            if ( MP_Bar.value < 1)
            {
                recoverMpTime += Time.deltaTime * 0.1f;
            }
        }
        else if (isBattling)
        {
            recoverHpTime = 0;
            recoverMpTime = 0;
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.F))
        {
            isBattling = true;
            BattleOutTime = 3f;
        }
        else
        {
            if (BattleOutTime > 0)
            {
                BattleOutTime -= Time.deltaTime * 1f;
            }
            else if (BattleOutTime == 0)
            {
                isBattling = false;
            }
        }
    }

    void CheckHP()
    {
        if ( HP_Bar.value == 0)
        {
            isDead = true;
            MovePower = 0;
            JumpPower = 0;

        }
        else
        {
            isDead = false;
            MovePower = 30f;
            JumpPower = 5f;
        }
    }

    void OpenInventory()
    {

    }



}
