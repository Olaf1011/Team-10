using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCollider : MonoBehaviour
{
    private bool firstFire = true;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(Melee.atckWitdh, Melee.atckRange); //Sets size of the hitbox
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFire)
        {
            StartCoroutine(ColliderDespawnDelay());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Hit3");
        }
    }
    IEnumerator ColliderDespawnDelay()
    {
        firstFire = false;
        yield return new WaitForSeconds(Melee.colliderDespawnDelay);                              //Waits for a set time before destorying it. The detroy happens after the delay. The return happends the moment it hits the line of code.
        Destroy(gameObject);
    }
}
