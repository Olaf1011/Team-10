using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    static private EnemyAttack instance = null;

    // Lets other scripts find the instane of the game manager
    public static EnemyAttack Instance
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

    void Start () 
    {
          
	}
}
