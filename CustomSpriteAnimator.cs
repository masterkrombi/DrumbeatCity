using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum AnimationType
{
    Stop,
    Idle,
    WindUp,
    Attack,
    BiteFollowThrough,
    Stun,
    WalkingDown,
    WalkingUp,
    Victory
}

public class CustomSpriteAnimator : MonoBehaviour {

    public List<Sprite> idleSprites;
    public List<Sprite> windUpSprites;
    public List<Sprite> attackSprites;
    public List<Sprite> biteFollowThroughSprites;
    public List<Sprite> stunSprites;
    public List<Sprite> walkingDownSprites;
    public List<Sprite> walkingUpSprites;
    public List<Sprite> victorySprites;

    float animationLength = 0;
    float currentAnimationTime = 0;
    SpriteRenderer sRend;
    public AnimationType currentAnimationType = AnimationType.Idle;

	// Use this for initialization
	void Start ()
    {
        animationLength = SongManager.instance.secondsPerBeat;
        currentAnimationTime = SongManager.instance.nextBeatTime - SongManager.instance.currentDSPTime;

        sRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentAnimationTime += SongManager.instance.deltaDSPTime;
        
        if (currentAnimationTime >= animationLength)
        {
            currentAnimationTime -= animationLength;
        }

        float currentAnimationPercent = currentAnimationTime / animationLength;

        switch (currentAnimationType)
        {
            case AnimationType.Stop:
                break;
            case AnimationType.Idle:
                sRend.sprite = idleSprites[(int)(currentAnimationPercent * idleSprites.Count)];
                break;
            case AnimationType.WindUp:
                sRend.sprite = windUpSprites[(int)(currentAnimationPercent * windUpSprites.Count)];
                break;
            case AnimationType.Attack:
                sRend.sprite = attackSprites[(int)(currentAnimationPercent * attackSprites.Count)];
                break;
            case AnimationType.BiteFollowThrough:
                sRend.sprite = biteFollowThroughSprites[(int)(currentAnimationPercent * biteFollowThroughSprites.Count)];
                break;
            case AnimationType.Stun:
                sRend.sprite = stunSprites[(int)(currentAnimationPercent * stunSprites.Count)];
                break;
            case AnimationType.WalkingDown:
                sRend.sprite = walkingDownSprites[(int)(currentAnimationPercent * walkingDownSprites.Count)];
                break;
            case AnimationType.WalkingUp:
                sRend.sprite = walkingUpSprites[(int)(currentAnimationPercent * walkingUpSprites.Count)];
                break;
            case AnimationType.Victory:
                sRend.sprite = victorySprites[(int)(currentAnimationPercent * victorySprites.Count)];
                break;
        }
    }

    public void ChangeAnimation(AnimationType type)
    {
        currentAnimationType = type;
    }

    public void FlipX()
    {
        sRend.flipX = !sRend.flipX;
    }
}
