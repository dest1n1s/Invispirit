using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cartridge1 : MonoBehaviour
{
    private static Cartridge1 _instance;
    public static Cartridge1 Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                return null;
            }
        }
    }
    public Text CountText;
    public int Count;
    void Awake()
    {
        _instance = this;
        Count = 30;
    }
    public void AutoAdd()
    {
        CountText.text = "ammunition:" + --Count;
    }

                // Start is called before the first frame update
                void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
