using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private string Walk_Animation = "Walk_Animation";

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private PlayerAttack playerAttack;

    private enum MovementState { idle, running, jumping, falling, attack }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerAttack.gameObject.SetActive(true);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0)
        {
            anim.SetBool(Walk_Animation, true);
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            anim.SetBool(Walk_Animation, true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool(Walk_Animation, false);
        }
        //MovementState state;

        //if (dirX > 0f)
        //{
        //    state = MovementState.running;
        //    sprite.flipX = false;
        //    playerAttack.transform.localPosition = new Vector3(1.5f, 1, 1);
        //}
        //else if (dirX < 0f)
        //{
        //    state = MovementState.running;
        //    sprite.flipX = true;
        //    playerAttack.transform.localPosition = new Vector3(-1.5f, 1, 1);
        //}
        //else
        //{
        //    state = MovementState.idle;
        //}

        //if (rb.velocity.y > .1f)
        //{
        //    state = MovementState.jumping;
        //}
        //else if (rb.velocity.y < -.1f)
        //{
        //    state = MovementState.falling;
        //}

        //if (playerAttack.gameObject.activeSelf && anim.GetInteger("state") != 4)
        //{
        //    state = MovementState.attack;
        //}

        //anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Assign into animation player attack
    public void DeactivePlayerAttack()
    {
        playerAttack.gameObject.SetActive(false);
        playerAttack.attacked = false;
    }
}
