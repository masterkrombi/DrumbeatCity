using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    AnimationType animBool = AnimationType.Idle;
    public float nextAnimation;
    public float parryWindowAcceptanceError;
    float endStunTime;
    public float stunForBeats = 2f;
    CustomSpriteAnimator anim;
    public bool ableToBeParried;
    Damageable thisDamageable;
    public Room thisRoom;
    int beatCountDown;

    public CustomSpriteAnimator biteAttackAnim;
    public GameObject jukebox;
    public int doubleIdleCount = 0;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponentInChildren<CustomSpriteAnimator>();
        nextAnimation = SongManager.instance.nextBeatTime;
        thisDamageable = GetComponent<Damageable>();

        jukebox = GameObject.FindGameObjectWithTag("Jukebox");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (nextAnimation <= SongManager.instance.currentDSPTime)
        {
            if (beatCountDown > 0)
            {
                beatCountDown--;
            }
            if (beatCountDown <= 0)
            {
                switch (animBool)
                {
                    case AnimationType.Idle:
                        if (doubleIdleCount == 1)
                        {
                            anim.ChangeAnimation(AnimationType.WindUp);
                            biteAttackAnim.ChangeAnimation(AnimationType.Idle);
                            animBool = AnimationType.WindUp;
                            doubleIdleCount = 0;
                        }
                        else
                        {
                            doubleIdleCount = 1;
                        }
                        break;
                    case AnimationType.WindUp:
                        anim.ChangeAnimation(AnimationType.Attack);
                        animBool = AnimationType.Attack;
                        biteAttackAnim.ChangeAnimation(AnimationType.Attack);
                        StartCoroutine(ParryWindow());
                        //jukebox.GetComponent<SFXMaster>().playRear = true;
                        break;
                    case AnimationType.Attack:
                        anim.ChangeAnimation(AnimationType.Idle);
                        if (!parried)
                        {
                            biteAttackAnim.ChangeAnimation(AnimationType.BiteFollowThrough);
                        }
                        animBool = AnimationType.Idle;
                        jukebox.GetComponent<SFXMaster>().playAttack = true;
                        break;/*
                    case AnimationType.Stun:
                        biteAttackAnim.ChangeAnimation(AnimationType.Idle);
                        if (SongManager.instance.currentDSPTime >= endStunTime)
                        {
                            anim.ChangeAnimation(AnimationType.Idle);
                            animBool = AnimationType.Idle;
                            parried = false;
                            if (thisDamageable.currentDefense <= 0)
                                thisDamageable.ResetDefense();
                        }
                        break;*/
                }
                //beatCountDown = thisRoom.GetEnemyCount();
            }
            nextAnimation = SongManager.instance.nextBeatTime;
        }
	}

    [HideInInspector]public bool parried = false;
    public void ChangeAnimation(AnimationType type)
    {
        if (type == AnimationType.Stun)
        {
            parried = true;
            biteAttackAnim.ChangeAnimation(AnimationType.Idle);
            ableToBeParried = false;
            /*if (thisDamageable.currentDefense <= 0)
            {
                animBool = type;
                anim.ChangeAnimation(type);
                endStunTime = SongManager.instance.nextBeatTime + SongManager.instance.secondsPerBeat * stunForBeats;
            }*/
        }
    }

    IEnumerator ParryWindow()
    {
        //yield return new WaitForSeconds(SongManager.instance.secondsPerBeat * (thisRoom.GetEnemyCount() - 1));
        
        float parryWindowStartTime = SongManager.instance.nextBeatTime - parryWindowAcceptanceError;
        float parryWindowEndTime = SongManager.instance.nextBeatTime + parryWindowAcceptanceError;

        while (SongManager.instance.currentDSPTime < parryWindowStartTime)
        {
            yield return null;
        }
        ableToBeParried = true;

        while (SongManager.instance.currentDSPTime < parryWindowEndTime)
        {
            yield return null;
        }

        if (!parried)
        {
            if (PlayerCombat.instance.GetComponent<Damageable>())
            {
                PlayerCombat.instance.gameObject.GetComponent<Damageable>().TakeDamage(10);
            }
        }
        else
        {
        }
        
        ableToBeParried = false;
        parried = false;
    }

    public void SetBeat(int beat)
    {
        beatCountDown = beat;
    }
}
