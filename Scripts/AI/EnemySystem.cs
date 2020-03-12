using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] float speed = 250;

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
        Move();
    }

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position, Vector3.forward);
        transform.rotation = rotation;

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    void Move()
    {
        var movement = Vector3.forward;
        //moving the character by adding velocity
        _rb.velocity = movement * speed * Time.deltaTime;
    }
}
