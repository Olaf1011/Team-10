using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCollider : MonoBehaviour
{
    public float atckRange = .5f, atckWitdh = .5f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().size = new Vector2(atckWitdh, atckRange);
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
