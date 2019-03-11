using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPulse : MonoBehaviour {

    //public float roomScaleSize = 1.5f;
    public Color pulseColor;
    public float lerpingSpeed = 10f;
    Color originalColor;
    SpriteRenderer sRend;
    float nextBeat;

	// Use this for initialization
	void Start () {
        sRend = GetComponent<SpriteRenderer>();
        originalColor = sRend.color;
        nextBeat = SongManager.instance.nextBeatTime;
    }

    // Update is called once per frame
    void Update() {

        if (SongManager.instance.currentDSPTime >= nextBeat && 
            PlayerCombat.instance.combat)
        {
            sRend.color = pulseColor;
            nextBeat = SongManager.instance.nextBeatTime;
        }
        else
        {
            sRend.color = Color.Lerp(sRend.color, originalColor, SongManager.instance.deltaDSPTime * lerpingSpeed);
        }
    }
}
