using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private GameObject meleeBox;

    public const float atckRange = .5f, atckWitdh = .5f;                    //The size of the hitbox
    public static float colliderDespawnDelay = 0.1f;                        //Delay for destroying the hitbox gameObject (Aslong as it's in the game it will detect enemies in it)
    [SerializeField] private float spawnDistance;                          //The distance the box spawns away from the player.

    //Struct with everything to do with the hitbox location spawning
    struct HitBox                           
    {
        public static Vector3 playerPos , playerDirection, spawnPos;
        public static Quaternion playerRotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.PLAYING:
                attack();
                break;
        }
    }

    void attack()
    {
        if (Input.GetButtonDown("Fire1")){
            setLocation();      
            GameObject hit = Instantiate(meleeBox, HitBox.spawnPos, HitBox.playerRotation);     //Keeps the box there for set amount of time so it always hits enemy's for set 
        }
    }

    void setLocation()
    {
        spawnDistance = (GetComponent<CircleCollider2D>().radius) + atckRange / 2; //Runs the calculation to place the hitBox in front of the player no matter rotation
        HitBox.playerPos = this.transform.position;                                         
        HitBox.playerDirection = this.transform.up;                                         //Takes what is up (Forward) compared to Players rotation. This will make it so we can place the box in front of the player
        HitBox.playerRotation = this.transform.rotation;                                    
        HitBox.spawnPos = HitBox.playerPos + (HitBox.playerDirection * spawnDistance);      //Adds Player position + (the direction the player is facing * the set distance the hitBox has to be placed in front of the player)
    }
}
