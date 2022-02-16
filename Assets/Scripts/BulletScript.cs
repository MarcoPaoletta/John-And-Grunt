using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    public AudioClip Sound;
    public float Speed;

    private Rigidbody2D Rigidbody2D; 
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    private void FixedUpdate()
    {
     Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet() // called when its animation finishes
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement jhon = collision.GetComponent<JohnMovement>();
        GruntScript grunt = collision.GetComponent<GruntScript>();

        if (jhon != null)
        {
            jhon.Hit();
        }

        if (grunt != null)
        {
            grunt.Hit();
        }

        DestroyBullet();

    }
}