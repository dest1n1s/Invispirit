using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior: MonoBehaviour
{
    public GameObject Bullet;
    public Transform MuzzleTransform;

    //public Camera cam;
    private Vector3 mousePos;
    private Vector2 gunDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gunDirection = (mousePos - transform.position).normalized;
        if (gunDirection.sqrMagnitude == 0) gunDirection = new Vector2(0, 1);
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Bullet == null) Debug.Log("Œ¥’“µΩprefab");
            
            GameObject newBullet=Instantiate(Bullet, MuzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
            newBullet.GetComponent<BulletBehavior>().Direction = gunDirection;
        }
    }
}
