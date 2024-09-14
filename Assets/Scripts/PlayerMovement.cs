using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 Movement;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
        {
            animator = FindObjectOfType<Animator>();
        }    
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal",Movement.x);
        animator.SetFloat("Vertical", Movement.y);
        animator.SetFloat("Speed",Movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * speed * Time.fixedDeltaTime);
    }
}
