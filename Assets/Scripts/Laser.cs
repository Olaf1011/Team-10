using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour {

    public int speed = 500;
    public int damage = 1;
    public string[] ignoreTags;
    public GameObject explosion;

    private Rigidbody2D _rb;
    private bool okToFire = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(explosion != null);
    }

	void Start ()
    {
        _rb.velocity = new Vector2(0, speed * 0.03f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        foreach (string tag in ignoreTags)
            if (other.tag == tag)
                return;

        GameObject fire = (GameObject)Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
        Destroy(fire, 1.0f);
        Destroy(other.gameObject);
        Destroy(gameObject);
        if ( other.tag == "Enemy")
        {
            Score.IncreaseScore();
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

}
