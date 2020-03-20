using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

    private int combo;
    private float decreaseComboCounter;

    [SerializeField] private Text comboText;
    [SerializeField] private Image comboMeter;

    public float MAX_COUNTER = 100;
    public int TIME_MULTIPER = 2;
    public int TIME_MULTIPER_DEFAULT;

    // Use this for initialization
    void Start () 
    {
        TIME_MULTIPER_DEFAULT = TIME_MULTIPER;
        combo = 0;
        decreaseComboCounter = MAX_COUNTER;
    }
	
	// Update is called once per frame
	void Update () {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.PLAYING:
                switch (GameManager.roomState)
                {
                    case GameManager.RoomState.FIGHTING:
                    decreaseCombo();
                    break;
                }
                break;
        }
	}

    private void decreaseCombo()
    {
        if(combo > 0)
        {
            decreaseComboCounter -= (combo * (Time.deltaTime * TIME_MULTIPER));
            if(decreaseComboCounter <= 0)
            {
                combo--;
                decreaseComboCounter = MAX_COUNTER;
            }
            comboMeter.rectTransform.sizeDelta = new Vector2(decreaseComboCounter, 20);
            byte colour = (byte)(decreaseComboCounter * 255 / 100);
            byte red = (byte)(255 - colour);
            comboMeter.color = new Color32(red , colour, 0 , 255);
        }
    }

    public void IncreaseScore()
    {
        decreaseComboCounter = 100;
        combo++;
    }
    void LateUpdate()
    {
        comboText.text = "x" + combo;
    }
}
