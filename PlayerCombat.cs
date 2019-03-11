using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    static public PlayerCombat instance;
    public bool combat = false;
    public bool victory = false;
    public float victoryForBeats = 3f;
    public float combatAttackSpeed = 25f;
    public int baseDamage = 0;
    public GameObject combatExclamationPoint = null;
    public GameObject shieldSprite = null;
    [Header("This number should be pretty small, probably between 0 and 0.1")]
    public float beatAcceptanceError = 0f;

    Vector3 targetPosition = Vector3.zero;
    //float dodgeTime = 0f;
    GameObject currentRoom = null;
    Vector3 currentCenter;
    bool bassPedal;
    float resetBassPedalTime = 0;
    public CustomSpriteAnimator characterAnimator;
    public SpriteFlipControl spriteFlipper;
    public GameObject jukebox;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(GameObject.Find("Player"));

        jukebox = GameObject.FindGameObjectWithTag("Jukebox");
        shieldSprite.SetActive(false);
    }

    void Update ()
    {
        /*
        //if halfway between beats reset bassPedal bool to allow bass pedal to pump momentum again
        if (resetBassPedalTime < SongManager.instance.currentDSPTime)
        {
            bassPedal = false;
        }*/

        if (combat)
        {
            Fight();
        }

        //Shielding
        if (CustomInputManager.GetBassPedalDown())
        {
            bassPedal = true;
            shieldSprite.SetActive(true);
            GetComponent<Damageable>().SetDamageReduction();
        }
        if (CustomInputManager.GetBassPedalUp())
        {
            bassPedal = false;
            shieldSprite.SetActive(false);
            GetComponent<Damageable>().RemoveDamageReduction();
        }

        //I ADDED THIS COLLIN.  I ALSO COMMENTED OUT SOME OTHER STUFF TO MAKE THE 
        //MOMENTUM RAISE AS YOU HIT THE PEDAL ON BEAT
        if (CustomInputManager.GetBassPedalDown())
        {
            //CalculateMomentum();
        }
    }
    
    void Fight()
    {
        if (targetPosition == transform.position)
        {
            targetPosition = currentCenter;
        }

        Room room = currentRoom.GetComponent<Room>();
        GameObject targetEnemy = null;

        if (!victory)
        {
            if (CustomInputManager.GetDrumRed() && !bassPedal)
            {
                //DL
                targetEnemy = room.GetEnemy(Direction.DL);
                targetPosition = transform.position + (-Vector3.forward + Vector3.right).normalized * 0.5f;

                if (targetEnemy)
                {
                    spriteFlipper.SetFacing(Facing.LeftFacing);
                    jukebox.GetComponent<SFXMaster>().playPunchDL = true;

                    targetEnemy.GetComponent<Damageable>().TakeDamage(baseDamage);
                    if (targetEnemy.GetComponent<Damageable>().currentDefense > 0 && !targetEnemy.GetComponentInParent<Enemy>().parried)
                    {
                        GetComponent<Damageable>().TakeDamage(5);
                    }
                }
            }
            if (CustomInputManager.GetDrumYellow() && !bassPedal)
            {
                //UL
                targetEnemy = room.GetEnemy(Direction.UL);
                targetPosition = transform.position + (-Vector3.right - Vector3.forward).normalized * 0.5f;

                if (targetEnemy)
                {
                    spriteFlipper.SetFacing(Facing.LeftFacing);
                    jukebox.GetComponent<SFXMaster>().playPunchUL = true;

                    targetEnemy.GetComponent<Damageable>().TakeDamage(baseDamage);
                    if (targetEnemy.GetComponent<Damageable>().currentDefense > 0 && !targetEnemy.GetComponentInParent<Enemy>().parried)
                    {
                        GetComponent<Damageable>().TakeDamage(5);
                    }
                }
            }
            if (CustomInputManager.GetDrumBlue() && !bassPedal)
            {
                //UR
                targetEnemy = room.GetEnemy(Direction.UR);
                targetPosition = transform.position + (Vector3.forward - Vector3.right).normalized * 0.5f;

                if (targetEnemy)
                {
                    spriteFlipper.SetFacing(Facing.RightFacing);
                    jukebox.GetComponent<SFXMaster>().playPunchUR = true;

                    targetEnemy.GetComponent<Damageable>().TakeDamage(baseDamage);
                    if (targetEnemy.GetComponent<Damageable>().currentDefense > 0 && !targetEnemy.GetComponentInParent<Enemy>().parried)
                    {
                        GetComponent<Damageable>().TakeDamage(5);
                    }
                }
            }
            if (CustomInputManager.GetDrumGreen() && !bassPedal)
            {
                //DR
                targetEnemy = room.GetEnemy(Direction.DR);
                targetPosition = transform.position + (Vector3.right + Vector3.forward).normalized * 0.5f;

                if (targetEnemy)
                {
                    spriteFlipper.SetFacing(Facing.RightFacing);
                    jukebox.GetComponent<SFXMaster>().playPunchDR = true;

                    targetEnemy.GetComponent<Damageable>().TakeDamage(baseDamage);
                    if (targetEnemy.GetComponent<Damageable>().currentDefense > 0 && !targetEnemy.GetComponentInParent<Enemy>().parried)
                    {
                        GetComponent<Damageable>().TakeDamage(5);
                    }
                }
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, combatAttackSpeed * Time.deltaTime);
    }
    /*
    void CalculateMomentum()
    {
        if ((Mathf.Abs(SongManager.instance.currentDSPTime - SongManager.instance.nextBeatTime) < beatAcceptanceError ||
             Mathf.Abs(SongManager.instance.currentDSPTime - SongManager.instance.previousBeatTime) < beatAcceptanceError) &&
            !bassPedal)
        {
            if (Mathf.Abs(SongManager.instance.currentDSPTime - SongManager.instance.nextBeatTime) <
                Mathf.Abs(SongManager.instance.currentDSPTime - SongManager.instance.previousBeatTime))
            {
                resetBassPedalTime = SongManager.instance.nextBeatTime + SongManager.instance.secondsPerBeat / 2;
            }
            else
            {
                resetBassPedalTime = SongManager.instance.previousBeatTime + SongManager.instance.secondsPerBeat / 2;
            }

            bassPedal = true;
        }
    }
    */

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Room"))
        {
            currentRoom = coll.gameObject;
            currentCenter = currentRoom.GetComponentInParent<Room>().GetCenter();
            StartCoroutine(currentRoom.GetComponent<Room>().GenerateEnemies());
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Room"))
        {
            if(!combat)
                currentRoom = null;
        }
    }

    static public IEnumerator EnterCombat()
    {
        instance.combat = true;
        instance.targetPosition = instance.currentRoom.GetComponent<Room>().GetCenter();

        instance.jukebox.GetComponent<SFXMaster>().playStart = true;


        instance.combatExclamationPoint.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        instance.combatExclamationPoint.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        instance.combatExclamationPoint.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        instance.combatExclamationPoint.SetActive(false);

        instance.characterAnimator.ChangeAnimation(AnimationType.Idle);
    }

    static public void GameWon()
    {
        instance.victory = true;
        instance.characterAnimator.ChangeAnimation(AnimationType.Victory);
    }

    static public IEnumerator Victory()
    {
        instance.victory = true;
        float startVictoryTime = SongManager.instance.nextBeatTime;
        float endVictoryTime = SongManager.instance.nextBeatTime + SongManager.instance.secondsPerBeat * instance.victoryForBeats;

        instance.jukebox.GetComponent<SFXMaster>().playEnd = true;


        while (SongManager.instance.currentDSPTime < startVictoryTime)
        {
            yield return null;
        }

        instance.characterAnimator.ChangeAnimation(AnimationType.Victory);

        while (SongManager.instance.currentDSPTime < endVictoryTime)
        {
            yield return null;
        }
        
        instance.victory = false;
        instance.combat = false;
    }

}
