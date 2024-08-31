
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : Enemy
{
    public enum SlimeState { Patrol, Chase, Attack }
    [Header("State")]
    public SlimeState slimeState;
    public GameObject player;

    [Header("Chase")]
    public float chaseSpeed = 10.0f;
    [Range(1.25f, 5.0f)] public float radiusAttack = 1.25f;
    public float waitToPatrol;

    [Header("Attack")]
    public float hitActive;
    public GameObject colliderAttack;

    [Header("Patrol")]
    public float speed = 5.0f;
    public Transform startPos, endPos;

    // Private Variables
    private Transform currentTarget;
    private Rigidbody2D rb2d;
    private Animator animator;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentTarget = startPos;
        direction = (currentTarget.position - transform.position).normalized;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3((direction.x > 0 ? -1 : 1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        switch (slimeState)
        {
            case SlimeState.Patrol:
                Patrol();
                break;

            case SlimeState.Chase:
                Chase();
                break;

            case SlimeState.Attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {
        direction = (currentTarget.position - transform.position).normalized;
        rb2d.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == startPos)
            {
                currentTarget = endPos;
            }
            else
            {
                currentTarget = startPos;
            }
        }

        // if (Vector2.Distance(transform.position, player.transform.position) < radiusChase)
        // {
        //     slimeState = SlimeState.Chase;
        // }
        if (player)
        {
            slimeState = SlimeState.Chase;
        }
    }

    private void Chase()
    {
        if (player)
        {
            StopCoroutine(IntoPatrol());
            wait = false;
            // if (Vector2.Distance(transform.position, player.transform.position) < radiusChase)
            // {

            // }
            direction = (player.transform.position - transform.position).normalized;
            rb2d.MovePosition((Vector2)transform.position + direction * chaseSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < radiusAttack)
            {
                slimeState = SlimeState.Attack;
            }
        }
        else
        {
            if (!wait)
            {
                StartCoroutine(IntoPatrol());
            }
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        rb2d.velocity = Vector2.zero;

        if (!player || Vector2.Distance(transform.position, player.transform.position) > radiusAttack)
        {
            slimeState = SlimeState.Chase;
            animator.SetBool("Attack", false);
        }
    }

    // di assign di Object Detector
    public void SetPlayer(Collider2D playerCol)
    {
        player = playerCol.gameObject;
    }

    // di assign di Object Detector
    public void SetPlayerNull(Collider2D playerCol)
    {
        player = null;
    }

    // di assign di animation event Slime_Attack
    public void ActiveHit()
    {
        StartCoroutine(ActiveDeactiveHit());
    }

    private IEnumerator ActiveDeactiveHit()
    {
        colliderAttack.SetActive(true);
        yield return new WaitForSeconds(hitActive);
        colliderAttack.SetActive(false);
    }

    private bool wait;
    private IEnumerator IntoPatrol()
    {
        wait = true;
        yield return new WaitForSeconds(1.0f);
        slimeState = SlimeState.Patrol;
        wait = false;
    }
}
