using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject hitbox;

    [SerializeField] float speed = 250;
    [SerializeField] float rotSpeed = 6;
    [SerializeField] float stoppingDistance = 1;

    private Rigidbody2D _rb;

    enum enemyState { CHASING, ATTACKING };
    private enemyState state = enemyState.CHASING;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        LookAtPlayer();
        DistCheck(dist);

        switch (state)
        {
            case enemyState.CHASING:
                Move();
                break;
            case enemyState.ATTACKING:
                _rb.velocity = Vector3.zero;

                spawnHitbox();

                if (dist > stoppingDistance)
                    state = enemyState.CHASING;
                break;
        }

    }

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

    //Distance check between enemy and player
    void DistCheck(float dist)
    {
        if (dist < stoppingDistance)
            state = enemyState.ATTACKING;
        else
            state = enemyState.CHASING;
    }

    void spawnHitbox()
    {
        Instantiate(hitbox, transform.position, transform.rotation);
    }
}
