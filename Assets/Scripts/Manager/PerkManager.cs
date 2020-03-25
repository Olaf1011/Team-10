using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    private ShipControl playerScript;
    private Combo comboScript;

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
        comboScript = GetComponent<Combo>();
    }

    public void AddPerk(PerkType perk)
    {
        activePerks.Add(perk);
        RefreshPerks();
    }

    public void RefreshPerks()
    {
        if (activePerks.Contains(PerkType.IncreaseMoveSpeed))
        {
            playerScript.speed = playerScript.defaultSpeed * 5;
        }
        if (activePerks.Contains(PerkType.SlowCombo))
        {
            comboScript.TIME_MULTIPER = comboScript.TIME_MULTIPER_DEFAULT / 2;
        }
    }
}
