using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    public static PlayerController pc;

    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    [SerializeField] private bool candoublejump;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpFore = 10f;

    [SerializeField] private float hurtForce = 5f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource jumpaudio;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
      
        

    }

    // Update is called once per frame
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }        
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherry.Play();
            PermanentUI.perm.cherries += 1;
            PermanentUI.perm.cherryText.text = PermanentUI.perm.cherries.ToString();
        }
        if (collision.tag == "Diamonds")
        {
            
            Destroy(collision.gameObject);
            cherry.Play();
            PermanentUI.perm.diamonds += 1;
            PermanentUI.perm.diamondText.text = PermanentUI.perm.diamonds.ToString();
        }
        if (collision.tag == "opo")
        {
            rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            state = State.hurt;
            HeartManager.health--;
            if (HeartManager.health <= 0)
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
               PermanentUI.perm.Reset();
               HeartManager.health = 3;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
             
            if (state == State.falling)
            {
                enemy.Jumpdone();
                
                jump();
            }
            else
            {
                state = State.hurt;
                HeartManager.health--;
                if(HeartManager.health <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    PermanentUI.perm.Reset();
                    HeartManager.health = 3;
                }
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            //rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.position = transform.position + new Vector3(-speed * Time.deltaTime, 0f,0f);
            transform.localScale = new Vector2(-1, 1);
           

        }
        else if (hDirection > 0)
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f);
            //rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
           
        }
        if (Input.GetButtonDown("Jump"))
        {
            
            if ( coll.IsTouchingLayers(ground))
            {
                jump();
                jumpaudio.Play();
                candoublejump = true;
            } 
            else
            if(candoublejump == true)
            {
                candoublejump = false;
                jump();
                jumpaudio.Play();
            }
        }
        
    }
    private void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpFore);
        state = State.jumping;
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) <.1f)
            {
                state = State.idle;
            }
        }
      //else if(Mathf.Abs(rb.velocity.x) >2f)
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }
   
}
