// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PlayerMovement : MonoBehaviour
{
    Vector2 motionVector;
    public Vector2 lastmotionVector;
    Animator animator;
    public bool moving; // 캐릭터가 움직이는 여부를 기준을 삼아서 참, 거짓

    // static public members
    public static PlayerMovement instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 movement;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        float horizontal = movement.x;
        float vertical = movement.y;

        // update members
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        

        if (horizontal != 0 || vertical != 0) 
        {
            lastmotionVector = new Vector2(horizontal, vertical).normalized;
            // 정규화(normalization)는 벡터의 길이를 1로 만드는 과정을 말합니다.
            // 벡터의 방향은 그대로 유지하면서 길이만 1로 맞춤
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }
    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
