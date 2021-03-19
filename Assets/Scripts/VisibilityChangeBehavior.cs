using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityChangeBehavior : MonoBehaviour
{
    public static VisibilityChangeBehavior instance;
    private VisibilityManager manager;
    void Awake()
    {
        instance = GetComponent<VisibilityChangeBehavior>();
    }
    void Start()
    {
        manager = VisibilityManager.GetInstance(GetComponent<Renderer>());
    }
    void OnCollisionEnter2D(Collision2D OtherObj)
    {
        manager.Emerge();
    }
    void OnCollisionStay2D(Collision2D OtherObj)
    {
        manager.Emerge();
    }
    //void Update()
    //{
    //    if (Show)
    //    {
    //        currentTime = 0;
    //        StartCoroutine("Emerge");
    //        Show = false;
    //    }

    //    if (!Show)
    //    {
    //        currentTime = currentTime + Time.deltaTime;
    //        if (currentTime > 5 && currentTime < 5.01)
    //        {
    //            StartCoroutine("Fade");
    //        }
    //    }
    //}
    //IEnumerator Fade()
    //{
    //    for (float f = GetComponent<Renderer>().material.color.a; f >= 0; f -= 0.1f)
    //    {
    //        GetComponent<Renderer>().material.color = new Color(1, 1, 1, f);
    //        yield return new WaitForSeconds(.1f);
    //    }
    //}
    //IEnumerator Emerge()
    //{
    //    for (float f = GetComponent<Renderer>().material.color.a; f <= 1f; f += 0.1f)
    //    {
    //        GetComponent<Renderer>().material.color = new Color(1, 1, 1, f);
    //        yield return new WaitForSeconds(.1f);
    //    }
    //}
}
