using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCollider : MonoBehaviour
{

    [SerializeField] private float atckRange = .5f, atckWitdh = .5f;      //The size of the hitbox
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(atckWitdh, atckRange); //Sets size of the hitbox
    }

    // Update is called once per frame
    void Update()
    {   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Hit3");
        }
    }
}
