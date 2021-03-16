using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthWhenCollide : MonoBehaviour
{
    float tempTime;
    float currentTime;
    public bool Show = false;
    void Start()
    {
        tempTime = 0;
        currentTime = 0;
    }
    void OnCollisionEnter(Collision OtherObj)
    {
        Show = true;
    }
    void Update()
    {
        if (Show)
        {
            if (10 * tempTime < 1)
            {
                tempTime = tempTime + Time.deltaTime;
            }
            if (GetComponent<Renderer>().material.color.a > 0)
            {
                GetComponent<Renderer>().material.color = new Color(1, 1, 1, (float)1 - 10 * tempTime);
            }
        }
        if (GetComponent<Renderer>().material.color.a == 0)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        if (currentTime == 5)
        {
            Fade();
        }
    }
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = f;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}

