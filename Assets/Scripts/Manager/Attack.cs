using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    static private Attack instance = null;

    // Lets other scripts find the instane of the game manager
    public static Attack Instance
    {
        get
        {
            return instance;
        }
    }

    // Ensure there is only one instance of this object in the game
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
