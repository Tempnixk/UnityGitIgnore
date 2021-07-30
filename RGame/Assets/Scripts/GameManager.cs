using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance;


    public int currentScore;
    
    public static int AttackDamage = 200;

    public int scorePerNote = AttackDamage / 2;
    public int scorePerGoodNote = AttackDamage;
    public int scorePerPerfectNote = AttackDamage * 2;



    public int currentCombo;
    public int comboTracker;
    public int[] comboThresholds;

    public Text scoreText; 
    public Text comboText;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentCombo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentCombo - 1 < comboThresholds.Length)
        {
            comboTracker++;

            if (comboThresholds[currentCombo - 1] <= comboTracker)
            {
                comboTracker = 0;
                currentCombo++; 
            }
        }

        comboText.text = "Combo: x" + currentCombo;

        //currentScore += scorePerNote * currentCombo;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentCombo;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentCombo;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentCombo;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentCombo = 1;
        comboTracker = 0;

        comboText.text = "Combo: x" + currentCombo;
    }


}
