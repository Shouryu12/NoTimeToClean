using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;
    public bool toJump;

    private Rigidbody2D rig;
    private Animator anim;

    bool isBlowing;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(FireButton.Instance){
            FireButton.Instance.button.onClick.AddListener(() => Jump());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Jump();
    }

    void Move()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * Speed;

        if(SingleJoystick.Instance)
        {
            float movement = SingleJoystick.Instance.GetInputDirection().x;
            rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

            if(movement > 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f,0f,0f);
            }
            
            if(movement < 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f,180f,0f);

            }
            if(movement == 0f)
            {
                anim.SetBool("walk", false);
            }
        }
    }

    public void changeBool(){
        toJump = true;
    }

    void Jump()
    {
        if(!isBlowing)
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump",true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {            
            isJumping = false;
            anim.SetBool("jump",false);  
            toJump = false; 
        }
        if(collision.gameObject.tag == "Spike")
        {            
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Saw")
        {            
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }
    }
}
