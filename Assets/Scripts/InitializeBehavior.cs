using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Config;

public class InitializeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConfigManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
