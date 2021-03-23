using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Config;
public class InitializeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1440, 720, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
