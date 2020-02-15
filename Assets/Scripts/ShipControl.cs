 using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl : MonoBehaviour 
{
    public GameObject shotPrefab;
    public float moveSpeed = 1000.0f;
    public float leftLimit, rightLimit;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
        Debug.Assert(shotPrefab.GetComponent<Laser>() != null);
    }
	
	void Update () 
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0);
        if (transform.position.x <= leftLimit && _rb.velocity.x < 0)
            _rb.velocity = Vector2.zero;
        else if (transform.position.x >= rightLimit && _rb.velocity.x > 0)
            _rb.velocity = Vector2.zero;

        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetButtonDown("Fire1"))
        {
            GameObject shot = Instantiate( shotPrefab, transform.position, Quaternion.identity) as GameObject;
            Debug.Assert(shot);
        }
	}
}
