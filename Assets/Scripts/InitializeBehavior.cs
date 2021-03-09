using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class InitializeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ConfigManager.Initialize();
        Debug.Log(this.GetType().ToString().ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
