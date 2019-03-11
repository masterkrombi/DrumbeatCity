using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour {

    public GameObject movementTutorialText;

    private void Start()
    {
        movementTutorialText.SetActive(false);
    }

    private void Update()
    {
        if (CustomInputManager.GetDrumYellow())
        {
            movementTutorialText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player Character")
            movementTutorialText.SetActive(true);
    }
}
