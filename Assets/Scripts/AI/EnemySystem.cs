using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private GameObject laser;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        StateMachine();
        DistCheck(dist);
    }


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- Enemy movement code -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    [SerializeField] float speed = 250;
    [SerializeField] float rotSpeed = 6;
    [SerializeField] float stoppingDistance = 1;

    void LookAtPlayer()
    {
        //Gets the rotation
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position, Vector3.forward).normalized;

        //Rotate over time according to speed until in the required rotation
        //A slerp is like a lerp but for rotation (turns it into a from and to vector, rather than points in the world)
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);

        //Stops the sprite wobbling
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    void Move()
    {
        //moving the character by adding velocity
        _rb.velocity = -transform.up * speed * Time.deltaTime;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- Distance checker -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    [SerializeField] bool rangedEnemy = false;
    [SerializeField] bool enemyCharger = false;
    float dist;

    //Distance check between enemy and player
    void DistCheck(float dist)
    {
        dist = Vector3.Distance(player.position, transform.position);

        // ----- if else statements for enemy behaviour -----
        if (dist < stoppingDistance)
            state = enemyState.ATTACKING;

        else if ((dist < (stoppingDistance + 5)) && enemyCharger)
            state = enemyState.CHARGE;

        else if ((dist < (stoppingDistance + 5)) && rangedEnemy)
            state = enemyState.RANGED_ATTACK;

        else
            state = enemyState.CHASING;
        // --------------------
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- The StateMachine and its variables -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    enum enemyState { CHASING, ATTACKING, RANGED_ATTACK, CHARGE };
    private enemyState state = enemyState.CHASING;

    void StateMachine()
    {
        switch (state)
        {
            case enemyState.CHASING:
                Move();
                break;
            case enemyState.ATTACKING:
                _rb.velocity = Vector3.zero;

                if (!attacking)
                    StartCoroutine(SpawnHitbox());

                if (dist > stoppingDistance)
                    state = enemyState.CHASING;
                break;
            case enemyState.RANGED_ATTACK:
                _rb.velocity = Vector3.zero;

                if (!attacking)
                    StartCoroutine(FireLaser());

                if (dist > (stoppingDistance + 5))
                    state = enemyState.CHASING;
                break;
            case enemyState.CHARGE:
                if (!charging)
                    StartCoroutine(Charge());
                break;
        }
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- The IEnumerators -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    private bool attacking = false;
    private bool charging = false;

    IEnumerator SpawnHitbox()
    {
        Instantiate(hitbox, transform.position, transform.rotation);

        attacking = true;

        yield return new WaitForSeconds(2);

        attacking = false;
    }

    IEnumerator FireLaser()
    {
        Instantiate(laser, transform.position, transform.rotation);

        attacking = true;

        yield return new WaitForSeconds(5);

        attacking = false;
    }

    IEnumerator Charge()
    {
        _rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1);

        charging = true;

        Vector3 chargeDir = player.position - transform.position;

        _rb.velocity = chargeDir * (speed * 1.1f) * Time.deltaTime;

        yield return new WaitForSeconds(5);

        charging = false;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
}
