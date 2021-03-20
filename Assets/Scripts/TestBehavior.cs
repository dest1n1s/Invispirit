using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x*gameObject.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
