using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public static SongManager instance;
    public float BPM;
    public float previousBeatTime = 0;
    public float nextBeatTime = 0;
    public float deltaDSPTime = 0;
    public AudioSource explore;
    public AudioSource fight;

    [HideInInspector]public float secondsPerBeat;
    float lastDSPTime = 0;
    public float currentDSPTime = 0;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;
        secondsPerBeat = 60f / BPM; Debug.Log(secondsPerBeat);
        lastDSPTime = (float)AudioSettings.dspTime;
        currentDSPTime = lastDSPTime;
        previousBeatTime = lastDSPTime;
        nextBeatTime = previousBeatTime + secondsPerBeat;
        explore.volume = 0.3f;
        fight.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentDSPTime = (float)AudioSettings.dspTime;
        //Calculate dspDeltaTime
        deltaDSPTime = currentDSPTime - lastDSPTime;

        //Calculate next beat
        if (currentDSPTime >= nextBeatTime)
        {
            previousBeatTime = nextBeatTime;
            nextBeatTime += secondsPerBeat;
        }

        lastDSPTime = currentDSPTime;

        if (PlayerCombat.instance.combat)
        {
            if (explore.volume > 0f)
                explore.volume -= deltaDSPTime;
            else
                explore.volume = 0f;

            if (fight.volume < 0.3f)
                fight.volume += deltaDSPTime;
            else
                fight.volume = 0.3f;
        }
        else
        {
            if (explore.volume < 0.3f)
                explore.volume += deltaDSPTime;
            else
                explore.volume = 0.3f;

            if (fight.volume > 0f)
                fight.volume -= deltaDSPTime;
            else
                fight.volume = 0f;
        }
	}
}
