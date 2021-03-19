using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÒÑÆúÓÃ
/// </summary>
public class EmergeWhenFire : MonoBehaviour
{
    float currentTime;
    public bool Show = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Show = true;
        }
        if (Show)
        {
            currentTime = 0;
            StartCoroutine("Emerge");
            Show = false;
        }

        if (!Show)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime > 5 && currentTime < 5.1)
            {
                StartCoroutine("Fade");
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
