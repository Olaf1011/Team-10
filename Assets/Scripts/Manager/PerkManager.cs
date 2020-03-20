using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    private ShipControl playerScript;

    public enum PerkType
    {
        IncreaseMoveSpeed,
        SlowCombo
    }
    public HashSet<PerkType> activePerks;
    // Start is called before the first frame update
    void Start()
    {
        activePerks = new HashSet<PerkType>();
        playerScript = GetComponent<ShipControl>();
        activePerks.Add(PerkType.IncreaseMoveSpeed);
        if (activePerks.Contains(PerkType.IncreaseMoveSpeed))
        {
            playerScript.speed = playerScript.speed * 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
