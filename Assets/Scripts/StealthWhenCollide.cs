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
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
    }
    void OnCollisionEnter2D(Collision2D OtherObj)
    {
        Show = true;
    }
    void OnCollisionStay2D(Collision2D OtherObj)
    {
        Show = true;
    }
    void Update()
    {
        if (Show)
        {
            currentTime = 0;
            tempTime = tempTime + Time.deltaTime;
            if (10 * tempTime > 1)
            {
                StartCoroutine("Emerge");
                Show = false;
            }
        }

        if (!Show)
        {

            currentTime = currentTime + Time.deltaTime;
            if (currentTime > 5 && currentTime < 5.1)
            {
                StartCoroutine("Fade");
                tempTime = 0;
            }
        }
    }
    IEnumerator Fade()
    {
        for (float f = GetComponent<Renderer>().material.color.a; f >= 0; f -= 0.1f)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.1f);
        }
    }
    IEnumerator Emerge()
    {
        for (float f = GetComponent<Renderer>().material.color.a; f <= 1f; f += 0.1f)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(.1f);
        }
    }
}
