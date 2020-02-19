using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float colliderDespawnDelay = 0.5f;               //Delay for destroying the hitbox gameObject (Aslong as it's in the game it will detect enemies in it)
    [SerializeField] private float spawnDistance = 0.7f;                         //The distance the box spawns away from the player.
    [SerializeField] private GameObject meleeBox;
    

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
        if (Input.GetButtonDown("Fire1") && GameManager.gameState == GameManager.GameState.PLAYING)  //Checks if the button was pressed and if the game is playing
        {
            attack();
        }
    }

    void attack()
    {
        setLocation();                                                                      //Runs the calculation to place the hitBox in front of the player no matter rotation
        StartCoroutine(ColliderDespawnDelay());                                             //Keeps the box there for set amount of time so it always hits enemy's for set time
    }

    void setLocation()
    {
        HitBox.playerPos = this.transform.position;                                         
        HitBox.playerDirection = this.transform.up;                                         //Takes what is up (Forward) compared to Players rotation. This will make it so we can place the box in front of the player
        HitBox.playerRotation = this.transform.rotation;                                    
        HitBox.spawnPos = HitBox.playerPos + (HitBox.playerDirection * spawnDistance);      //Adds Player position + (the direction the player is facing * the set distance the hitBox has to be placed in front of the player)
    }

    IEnumerator ColliderDespawnDelay()
    {
        GameObject hit = Instantiate(meleeBox, HitBox.spawnPos, HitBox.playerRotation);     //Spawns the prefab of the hitBox with new position Modified to be in front. Also had the rotation added
        yield return new WaitForSeconds(colliderDespawnDelay);                              //Waits for a set time before destorying it. The detroy happens after the delay. The return happends the moment it hits the line of code.
        Destroy(hit);
    }
}
