using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private float spawnDistanceMod = 0;
    [SerializeField] private float atckRange = .5f, atckWitdh = .5f;        //The size of the hitbox
    [SerializeField] private float colliderDespawnDelay = 1.0f;            //Delay for destroying the hitbox gameObject (Aslong as it's in the game it will detect enemies in it)

    private Vector2 attackSize;
    BoxCollider2D box;


    //Struct with everything to do with the hitbox location spawning
    struct HitBox
    {
        public Vector3 enemyPos, enemyDirection, spawnPos;
        public Quaternion enemyRotation;
        public float spawnDistance;                                         //The distance the box spawns away from the player.
    }

    HitBox hitBox;
    // Start is called before the first frame update
    void Start()
    {
        attackSize = new Vector2(atckRange, atckWitdh);
        box = EnemyAttack.Instance.gameObject.GetComponent<BoxCollider2D>();    //This needs to be done while the gameObject is active
        box.size = attackSize;                                                  //Sets the size of the hitbox first frame.
        EnemyAttack.Instance.gameObject.SetActive(false);                       //Makes sure the gameObject Attack is inactive after setting box to be the boxCollider2D
    }

    public void Attacking()
    {
        if (!EnemyAttack.Instance.gameObject.activeInHierarchy)
        {
            SetLocation();
            BoxHitbox();
        }
    }

    void BoxHitbox()
    {
        EnemyAttack.Instance.gameObject.transform.position = hitBox.spawnPos;                //Moves the gameobject infront of the player
        EnemyAttack.Instance.gameObject.SetActive(true);                                     //Sets the gameObject to check if there is an enemy
        StartCoroutine(ColliderDespawnDelay());
    }

    IEnumerator ColliderDespawnDelay()
    {
        yield return new WaitForSeconds(colliderDespawnDelay);                              //Waits for a set time before deactiviting it. The return happends the moment it hits the line of code.
        EnemyAttack.Instance.gameObject.SetActive(false);
    }

    void SetLocation()
    {
        
        hitBox.spawnDistance = (this.gameObject.GetComponent<CircleCollider2D>().radius) + (atckRange / 2) + spawnDistanceMod;                                       //Runs the calculation to place the hitBox in front of the player no matter rotation
        hitBox.enemyPos = this.transform.position;
        hitBox.enemyDirection = -this.transform.up;                                                 //Takes what is up (Forward) compared to Players rotation. This will make it so we can place the box in front of the player
        hitBox.enemyRotation = this.transform.rotation;
        hitBox.spawnPos = hitBox.enemyPos + (hitBox.enemyDirection * hitBox.spawnDistance);      //Adds Player position + (the direction the player is facing * the set distance the hitBox has to be placed in front of the player)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.gameObject.tag == "Enemy")
        {
            Debug.Log("Gotcha");
        }
    }
}
