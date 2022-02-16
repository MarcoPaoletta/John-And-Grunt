using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{

    // by making a var public, we can modify them in the inspector
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private float LastShoot; 
    private bool Grounded;
    private int Health = 5;

    void Start()
    {
      Rigidbody2D = GetComponent<Rigidbody2D>(); // reference to the rigid body
      Animator = GetComponent<Animator>(); // reference to the animator
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // returns 1 if we are pressing D or -1 if we are pressing A
        Animator.SetBool("running", Horizontal != 0.0f); // change the animation to running

        if (Horizontal < 0.0f) // if we are moving left
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal > 0.0f) // if we are moving right
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f)) // if we are on floor
        {
            Grounded = true;
        }
        else 
        {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded) // if we press the W
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f) // if we press space and have passed 0.25 seconds
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce); // add force 
    }

    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f) // if  we are moving right
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y); // change the X velocity deppending on the Horizontal var
    }

    public void Hit()
    {
        Health -= 1;

        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
