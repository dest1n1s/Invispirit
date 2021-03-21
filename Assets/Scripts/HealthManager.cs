using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int Hp;
    [Header("UI")]
    public Text HpText;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 100;
        HpText = GameObject.Find("HpText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HpText.text = Hp.ToString();
    }
}