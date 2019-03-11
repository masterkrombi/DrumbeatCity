using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShieldParry : MonoBehaviour {

    public GameObject shieldTutorialText;
    public GameObject parryTutorialText;

    bool doTheShield = false;
    bool doTheParry = false;

    private void Start()
    {
        shieldTutorialText.SetActive(false);
        parryTutorialText.SetActive(false);
    }

    private void Update()
    {
        if (CustomInputManager.GetBassPedalDown() && doTheShield)
        {
            shieldTutorialText.SetActive(false);
            parryTutorialText.SetActive(true);
            doTheShield = false;
        }

        if (!doTheShield && doTheParry && 
            CustomInputManager.GetDrumRed() || CustomInputManager.GetDrumYellow() ||
            CustomInputManager.GetDrumBlue() || CustomInputManager.GetDrumGreen())
        {
            parryTutorialText.SetActive(false);
            doTheParry = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player Character")
        {
            doTheShield = true;
            doTheParry = false;
            shieldTutorialText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player Character")
        {
            doTheShield = false;
            doTheParry = false;
            shieldTutorialText.SetActive(false);
            parryTutorialText.SetActive(false);
        }
    }
}
