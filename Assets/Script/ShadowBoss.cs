using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowBoss : Enemy
{
    public enum ShadowState { Attack, Run }
    public ShadowState shadowState;
    public Animator animator;

    [Header("Run State")]
    public Transform posA;
    public Transform posB;

    [Header("Attack State")]
    public int maxProjectileCount = 5;
    public float forceProjectile = 1000.0f;
    public Rigidbody2D projectilePrefab;
    public Transform player;
    public Transform rootProjectile;
    public Transform randomUp, randomDown;

    private int tempProjectileCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (shadowState)
        {
            case ShadowState.Attack:
                ShadowAttack();
                break;

            case ShadowState.Run:
                ShadowRun();
                break;
        }
    }

    public void ChangeState(ShadowState state)
    {
        shadowState = state;
    }

    private void ShadowRun()
    {
        PlayStateAnimator("Shadow_Run");
    }

    private void ShadowAttack()
    {
        PlayStateAnimator("Shadow_Attack");
    }

    public void Teleport()
    {
        float distA = Vector2.Distance(posA.position, player.position);
        float distB = Vector2.Distance(posB.position, player.position);
        Transform randTarget = distA > distB ? posA : posB;
        transform.position = new Vector2(Random.Range(0, randTarget.position.x), transform.position.y);
        ChangeState(ShadowState.Attack);
    }

    public void ShootProjectile()
    {
        float dirX = player.position.x - transform.position.x;

        // player in right direction
        if (dirX > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        Vector2 randDir = new Vector2(dirX, Random.Range(randomUp.position.y, randomDown.position.y));
        Rigidbody2D projectile = Instantiate(projectilePrefab, rootProjectile);
        projectile.transform.localScale = new Vector3(-projectile.transform.localScale.x, 1, 1);
        projectile.AddForce(randDir.normalized * forceProjectile);
        tempProjectileCount++;

        if (tempProjectileCount > maxProjectileCount)
        {
            ChangeState(ShadowState.Run);
            tempProjectileCount = 0;
        }
    }
    private void ForcePlayStateAnimator(string name)
    {
        animator.Play(name);
    }

    private void PlayStateAnimator(string name)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return;
        }
        ForcePlayStateAnimator(name);
    }
}
