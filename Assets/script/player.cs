using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool canMove;
    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private float vertical;
    private bool Grounded;
    private Animator Animator;
    private bool Fire = false;
    private float LastShot;
    private float DeltaFire;
    public GameObject balaPreFabs;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
         
        // Obtiene si se esta presionando derecha o izquierda
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");



        // Gira al personaje a la izquierda o derecha
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if(horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (vertical < 0.0f && horizontal == 0.0f)
        {
            Animator.SetBool("down", true);
        }
        else
        {
            Animator.SetBool("down", false);
        }



        // Dibuja rayo pasa saber si esta tocando el sueñp
        Debug.DrawRay(transform.position, Vector3.down * 0.18f, Color.red);

        if(Physics2D.Raycast(transform.position, Vector3.down, 0.18f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false; 
        }



        Animator.SetBool("runnig", horizontal != 0.0f);
        Animator.SetBool("jumping", (Grounded == false));  
        Animator.SetBool("jumpIdle", (horizontal == 0.0f && !Grounded));

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {

            Jump();

        }




        fire();



    }


    private void FixedUpdate()
    {

        fire();
        if (!canMove){

            Rigidbody2D.velocity = Vector2.zero;
            return;

        }

        Rigidbody2D.velocity = new Vector2(horizontal, Rigidbody2D.velocity.y);

    }


    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
       
    }


    private void fire()
    {

        Vector3 direction;

        if (Input.GetKeyDown(KeyCode.E) && Time.time > LastShot)
        {
            Animator.SetBool("fire", true);
       
            Rigidbody2D.velocity = new Vector2(0.0f, Rigidbody2D.velocity.y);
            LastShot = Time.time + 0.25f;
            if(Grounded)
            {
                canMove = false;
            }

            if (transform.localScale.x == 1.0f) direction = Vector2.right;
            else direction = Vector2.left;

            GameObject bala = Instantiate(balaPreFabs, transform.position + direction * 0.2f, Quaternion.identity);
          

           
       

        }
        else
        {
            Animator.SetBool("fire", false);

        }


        if (Time.time > LastShot)
        {

            canMove = true;

        }

       

    }




}
