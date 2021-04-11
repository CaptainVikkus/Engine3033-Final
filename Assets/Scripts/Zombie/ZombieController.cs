using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] float attackSpeed = 2;
    [SerializeField] float attackRange = 1;
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float health = 10;

    private GameObject player;
    private NavMeshAgent ai;
    private Animator animator;
    private static readonly int IsAttackingHash = Animator.StringToHash("isAttacking");

    private bool canAttack = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.speed = walkSpeed;

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack && Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            animator.SetBool(IsAttackingHash, true);
            //Damage here
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    public void StopAttack()
    {
        animator.SetBool(IsAttackingHash, false);
    }

    IEnumerator UpdatePath()
    {
        while(isActiveAndEnabled)
        {
            yield return new WaitForSeconds(1);
            //Set Destination
            ai.SetDestination(player.transform.position);
            if (!ai.hasPath)
            {
                //Idle
            }
        }
    }

    public void Kill()
    {
        SpawnerManager.numZombies--;
        LootSpawner.SpawnLoot(10, transform);
        Destroy(this.gameObject);
    }

    public void Damage(float damageTaken)
    {
        health += damageTaken;
        if (health <= 0)
            Kill();
    }
}
