using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
   public float forward = 15;
    public float right = 7;
    float jump = 400;
    float JumpLimit = 4.0f;
    public int jumpCount = 0;

    float DeadLimit = -7.5f;

    Animator anim;

    Animation anima;

    public bool IsGrounded;

    public GameObject panel;

    public bool hasjumped = false;
    

    int point;

    public GameObject JumperInd;

    float Fmove, Rmove;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
        
        
    }

    
    void FixedUpdate()
    {
        PlayerDead();
        MovementF();
        MovementR();
        JumpMovement();
        JumpIndicator();

        





    }

    void MovementF()
    {
       
             Fmove = CrossPlatformInputManager.GetAxis("Vertical");
            transform.Translate(Vector3.forward * forward * Time.deltaTime * Fmove);
        

       



    }

    void MovementR()
    {
        
        
             Rmove = CrossPlatformInputManager.GetAxis("Horizontal");
            transform.Translate(Vector3.right * right * Time.deltaTime * Rmove);
        
        if (Rmove == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }



    }

    public void JumpMovement()
    {
        
        bool JumpM = CrossPlatformInputManager.GetButton("Jump");
        if (JumpM && transform.position.y < JumpLimit && IsGrounded)
        {
            anim.SetTrigger("takeof");
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            anim.Play("jump start");
            IsGrounded = false;
        }
        if (IsGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;

        if (collision.collider.tag == "obstacles")
        {
           
            FindObjectOfType<GameManager>().gameOver();
        }
        if (collision.collider.tag == "finish")
        {
            FindObjectOfType<GameManager>().Finish();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jumper"))
        {
            hasjumped = true;
            JumperInd.SetActive(true);
            jump = 800;
            jumpCount += 1;
            Destroy(other.gameObject);
            StartCoroutine(JumpER());
        }

        if (other.CompareTag("point"))
        {
            point += 20;
            FindObjectOfType<GameManager>().pointCount(point);
            Destroy(other.gameObject);
        }

        

    }

   IEnumerator JumpER()
    {
        yield return new WaitForSeconds(5);
        hasjumped = false;
        JumperInd.SetActive(false);
        jump = 400;
        
    }

    void PlayerDead()
    {
        if (gameObject.transform.position.y < DeadLimit)
        {
            FindObjectOfType<GameManager>().gameOver();
        }
    }
   
    void JumpIndicator()
    {
        JumperInd.transform.position = transform.position + new Vector3(0, 0, 0);
    }


    


}
