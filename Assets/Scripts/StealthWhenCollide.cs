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
    void OnCollisionEnter2D(Collision2D OtherObj)
    {
        Show = true;
        Debug.Log("Collide");
    }
    void Update()
    {
        if (Show)
        {
            tempTime = tempTime + Time.deltaTime;
            if (10 * tempTime > 1)
            {
                StartCoroutine("Emerge");
                Debug.Log("Emerge");
                Show = false;
                tempTime = 0;
            }
        }

        if (!Show)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime > 5)
            {
                StartCoroutine("Fade");
                currentTime = 0;
            }
        }
    }
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.1f);
        }
    }
    IEnumerator Emerge()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1 - f);
            yield return new WaitForSeconds(.1f);
        }
    }
}

