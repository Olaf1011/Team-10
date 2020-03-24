using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] private float speed = 500;          //The players speed
    [SerializeField] private float dodgeDistance = 1.0f; //The distance the player roughly travels while dodging
    [SerializeField] private float dodgeSpeedMod = 1.5f; //How quick player goes from A to B while dodging
    [SerializeField] private float dodgeDelay = 0.5f;   //The delay between dodges starts from the moment the dodge key is pressed
    [SerializeField] private bool dodgeDelayed = true;  //Needs to starts as true if dodge is enabled.

    private Rigidbody2D _rb;
    private Vector2 movement;
    private Vector2 stopMovement = Vector2.zero;
    private Vector2 OGPosPlayer;
    private Vector2 dodgeDir;
    private bool dodging = false;

    public enum PlayerState
    {
        WALKING,
        ROLLING
    }

    PlayerState playerState = PlayerState.WALKING;

    Melee melee;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
        melee = GameObject.Find("Player").GetComponent<Melee>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.PLAYING: //Change to playing in the future
                //calling the functions
                Movement();
                melee.Attacking();
                break;
            case GameManager.GameState.PAUSED:
                _rb.velocity = stopMovement;
                break;
            default:
                break;
        }
    }

    void Movement()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        getInputs();
        switch (playerState)
        {
            case PlayerState.ROLLING:
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy.gameObject.GetComponent<Collider2D>(), true);
                Dodge();
                break;
            case PlayerState.WALKING:
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy.gameObject.GetComponent<Collider2D>(), false);
                getMouse();
                Move();
                break;
        }
    }

    void getMouse()
    {
        //sets sprite to follow the mouse position

        //assigns a new variable with the coordinates of the screen
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //the quaternion is the angle we want, which we find by substracting the position from the mouse position
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        //transforms the sprite with the new rotation
        transform.rotation = rotation;

        //forces rotation only on the Z axis (prevents wobbling)
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    void getInputs()
    {
        //makes a new 2d vector which has the input of the player's x and y
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if((Input.GetAxis("Dodge")) == 1 && playerState == PlayerState.WALKING)
        {
            playerState = dodgeDelayed ? PlayerState.ROLLING : PlayerState.WALKING;
            if(playerState == PlayerState.ROLLING)
            {
                StartCoroutine(DodgeDelay());
            }
        }
    }

    IEnumerator DodgeDelay()
    {
        dodgeDelayed = false;
        yield return new WaitForSeconds(dodgeDelay);
        dodgeDelayed = true;
    }

    void Move()
    {
        //moving the character by adding velocity
        _rb.velocity = movement * speed * Time.deltaTime;
    }

    void Dodge()
    {   
        if (!dodging)
        {
            dodging = true;
            dodgeDir = movement != Vector2.zero ? movement : (Vector2)transform.up; //is this condition true ? yes : no
            OGPosPlayer = _rb.transform.localPosition;            
        }
        float currentDistance = Vector3.Distance(OGPosPlayer, _rb.transform.localPosition); //Gets the distance between the starting roll position and the current position
        if (dodging && (currentDistance <= dodgeDistance))
        { 
            _rb.velocity = ((dodgeDir * speed) * Time.deltaTime) * dodgeSpeedMod; //With velocity it's a precise dodge distance. With add force it won't bug out with it moving slow.
        }
        else
        {
            dodging = false;
            playerState = PlayerState.WALKING;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Walls" && playerState == PlayerState.ROLLING)
        {
            playerState = PlayerState.WALKING;
            dodging = false;

        }
    }
}