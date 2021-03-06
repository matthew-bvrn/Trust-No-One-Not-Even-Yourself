﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip music;

    public GameObject flag;
    public float roundLengthDefend=120f;
    public float roundLengthDefendRound1 = 140f;
    public float roundLengthAttack = 120f;
    public float fightTimeLength=60f;
    public string roundType="defend";
    public float timeLeftThisRound;
    public int roundNumber;
    public string gameState;
    public float MasterVolume;
    public bool newRound;
    public Player player;
    public bool debugMode=false;
    // Start is called before the first frame update
    void Start()
    {
        gameState = "menu";
        roundNumber = 1;
        timeLeftThisRound = roundLengthDefendRound1;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        newRound = false;

        if (gameState=="paused")
        {

        }

        if (gameState == "active")
        {
            if (Input.GetKeyDown("escape"))
            {
                gameState = "paused";
            }

            timeLeftThisRound -= Time.deltaTime;

            if (timeLeftThisRound <= 0f)
            {
                if (roundType == "defend")
                {
                    newRoundF();
                }
                else
                {
                    gameState = "lose";
                }
            }
        }
    }

    public void SetVolume(System.Single value)
    {
        MasterVolume = value;
    }

    public void SetMusicVolume(System.Single value)
    {
        audioSource.volume = value*0.5f;
    }

    public void newRoundF()
    {
        newRound = true;
        
        roundNumber++;
        if (roundNumber % 2 == 1)
        {
            if (roundNumber == 1)
            {
                timeLeftThisRound = roundLengthDefendRound1;
            }
            else
            {
                timeLeftThisRound = roundLengthDefend;
            }
            roundType = "defend";
        }
        else
        {
            timeLeftThisRound = roundLengthAttack;
            player.health.health = player.maxHealth;
            roundType = "attack";
        }
    }

    public void newGame()
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        List<GameObject> spawnLocs = new List<GameObject>();
        foreach (object o in obj)
        {
           // Debug.Log("test");
            GameObject g = (GameObject)o;
            if (g.tag == "Enemy" || g.tag=="Turret"||g.tag=="Resource"||g.tag=="Barrier")
            {
              //  Debug.Log("destroy" + g.tag.ToString());
                Destroy(g);
            }
            if(g.tag=="SpawnPad")
            {
                g.GetComponent<SpawnPad>().currentSpawnedResources = 0;
            }
            

        }
        roundNumber = 1;
        timeLeftThisRound = roundLengthDefendRound1;
        roundType = "defend";
        player.reset();
    }
}
