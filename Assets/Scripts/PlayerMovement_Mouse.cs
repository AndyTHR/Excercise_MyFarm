using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Mouse : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 TargetPosition;
    private Vector2 Movement;
    public Animator animator;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = FindObjectOfType<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }
        Movement = (TargetPosition - rb.position).normalized;
        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Vertical", Movement.y);
        animator.SetFloat("Speed", isMoving? 1:0);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + Movement * speed * Time.fixedDeltaTime);
            if (Vector2.Distance(rb.position, TargetPosition) <= 0.1f)
            {
                isMoving = false;

            }
        }
    }
}
