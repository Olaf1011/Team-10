using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] private float speed = 500;

    private Rigidbody2D _rb;
    private Vector2 movement;
    private Vector2 stopMovement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);

    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.PLAYING: //Change to playing in the future
                //calling the functions
                getMouse();
                getInputs();
                Move();
                break;
            case GameManager.GameState.PAUSED:
                _rb.velocity = stopMovement;
                break;
            default:
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
    }

    void Move()
    {
        //moving the character by adding velocity
        _rb.velocity = movement * speed * Time.deltaTime;
    }
}