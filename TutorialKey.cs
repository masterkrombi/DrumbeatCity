using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKey : MonoBehaviour {

    public GameObject keyTutorialText;

    private void Start()
    {
        keyTutorialText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player Character") keyTutorialText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player Character") keyTutorialText.SetActive(false);
    }
}
