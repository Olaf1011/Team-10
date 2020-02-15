using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float colliderDespawnDelay = 0.5f;
    public GameObject meleeBox;
    private Vector2 currentPlayerPositon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.gameState == GameManager.GameState.WELCOME)
        {
            Debug.Log("Fire");
            attack();
        }
    }
    private void attack()
    {
        getPlayerLocation();
        setLocation();
        StartCoroutine(ColliderDespawnDelay());
    }

    void getPlayerLocation()
    {
        currentPlayerPositon.x = gameObject.transform.position.x;
        currentPlayerPositon.y = gameObject.transform.position.y;
    }

    void setLocation()
    {
        meleeBox.transform.position = new Vector2(0, 0);
    }

    IEnumerator ColliderDespawnDelay()
    {
        meleeBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(colliderDespawnDelay);
        meleeBox.gameObject.SetActive(false);
    }
}
