using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

    static CustomInputManager instance;

    public string[] joysticks;
    public string[] split;

    public eControllerType controllerType = eControllerType.Keyboard;

    public enum eControllerType
    {
        Keyboard,
        Xbox,
        Playstation
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        joysticks = Input.GetJoystickNames();
        char[] splitters = { ' ', '(', ')', };
        if (joysticks.Length == 0)
        {
            controllerType = eControllerType.Keyboard;
            return;
        }
        split = joysticks[0].Split(splitters);
        for (int i = 0; i < split.Length; i++)
        {
            if (split[i] == "PlayStation")
            {
                controllerType = eControllerType.Playstation;
                break;
            }
            else if (split[i] == "Xbox")
            {
                controllerType = eControllerType.Xbox;
                break;
            }
        }
    }

    /// <summary>
    /// Used to get the input of the Blue Drum pad
    /// </summary>
    /// <returns></returns>
    static public bool GetDrumBlue()
    {
        bool drumBlue = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                drumBlue = Input.GetKeyDown(KeyCode.E);
                break;
            case eControllerType.Playstation:
                drumBlue = Input.GetButtonDown("Button 0");
                break;
            case eControllerType.Xbox:
                drumBlue = Input.GetButtonDown("Button 2");
                break;
        }

        return drumBlue;
    }

    /// <summary>
    /// Used to get the input of the yellow Drum pad
    /// </summary>
    /// <returns></returns>
    static public bool GetDrumYellow()
    {
        bool drumYellow = false;
        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                drumYellow = Input.GetKeyDown(KeyCode.W);
                break;
            case eControllerType.Playstation:
                drumYellow = Input.GetButtonDown("Button 3");
                break;
            case eControllerType.Xbox:
                drumYellow = Input.GetButtonDown("Button 3");
                break;
        }

        return drumYellow;
    }

    /// <summary>
    /// Used to get the input of the Green Drum pad
    /// </summary>
    /// <returns></returns>
    static public bool GetDrumGreen()
    {
        bool drumGreen = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                drumGreen = Input.GetKeyDown(KeyCode.R);
                break;
            case eControllerType.Playstation:
                drumGreen = Input.GetButtonDown("Button 1");
                break;
            case eControllerType.Xbox:
                drumGreen = Input.GetButtonDown("Button 0");
                break;
        }

        return drumGreen;
    }

    /// <summary>
    /// Used to get the input of the Red Drum pad
    /// </summary>
    /// <returns></returns>
    static public bool GetDrumRed()
    {
        bool drumRed = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                drumRed = Input.GetKeyDown(KeyCode.Q);
                break;
            case eControllerType.Playstation:
                drumRed = Input.GetButtonDown("Button 2");
                break;
            case eControllerType.Xbox:
                drumRed = Input.GetButtonDown("Button 1");
                break;
        }

        return drumRed;
    }

    /// <summary>
    /// Used to get the input of the Bass Pedal
    /// </summary>
    /// <returns></returns>
    static public bool GetBassPedalDown()
    {
        bool bassPad = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                bassPad = Input.GetKeyDown(KeyCode.Space);
                break;
            case eControllerType.Playstation:
                bassPad = Input.GetButtonDown("Button 4");
                break;
            case eControllerType.Xbox:
                bassPad = Input.GetButtonDown("Button 5");
                break;
        }
        return bassPad;
    }

    /// <summary>
    /// Used to get the input of the Bass Pedal
    /// </summary>
    /// <returns></returns>
    static public bool GetBassPedalUp()
    {
        bool bassPad = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                bassPad = Input.GetKeyUp(KeyCode.Space);
                break;
            case eControllerType.Playstation:
                bassPad = Input.GetButtonUp("Button 4");
                break;
            case eControllerType.Xbox:
                bassPad = Input.GetButtonUp("Button 5");
                break;
        }
        return bassPad;
    }

    static public bool Pause()
    {
        bool pause = false;

        switch (instance.controllerType)
        {
            case eControllerType.Keyboard:
                pause = Input.GetKeyDown(KeyCode.Escape);
                break;
            case eControllerType.Playstation:
                pause = Input.GetButtonDown("Button 9");
                break;
            case eControllerType.Xbox:
                pause = Input.GetButtonUp("Button 7");
                break;
        }
        return pause;
    }
}
