using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMotion : MonoBehaviour {

    public int speed = 3;
    public float xLimit = 4.5f;
    [Tooltip("offset at end of row transition")]
    public Vector2 rowDrop = new Vector2(0.25f,0.5f); 

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
    }
    
	void Start () 
    {
        _rb.velocity = new Vector2(speed * 0.03f, 0);
	}

	void Update ()
    {
        if (transform.position.x >= xLimit)
        {
            transform.position = new Vector2(transform.position.x - rowDrop.x, transform.position.y - rowDrop.y);
            speed = -speed;
            _rb.velocity = new Vector2(speed * 0.03f, 0);
        }
        else if (transform.position.x <= -xLimit)
        {
            transform.position = new Vector2(transform.position.x + rowDrop.x, transform.position.y - rowDrop.y);
            speed = -speed;
            _rb.velocity = new Vector2(speed * 0.03f, 0);
        }
	}
}
