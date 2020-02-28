using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    static public int armourAmount = 0;
    static public bool isAlive = true;
    private int maxArmour = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    void ArmourCheck()
    {
        if (armourAmount > 0)
            BreakArmour();
        else
            KillPlayer();
    }

    void BreakArmour()
    {
        armourAmount -= 1;
    }

    void GiveArmour()
    {
        if(armourAmount < maxArmour)
            armourAmount += 1;
    }

    void KillPlayer()
    {
        isAlive = false;
        GameManager.PlayerDied();
    }
}
