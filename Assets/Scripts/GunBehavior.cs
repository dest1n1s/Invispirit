using Assets.Scripts;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior: NetworkBehaviour
{
    public GameObject Bullet;
    public Transform MuzzleTransform;
    
    //private VisibilityManager heroManager;
    private Vector2 gunDirection;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gunDirection = (mousePos - transform.position).normalized;
        if (gunDirection.sqrMagnitude == 0) gunDirection = new Vector2(0, 1);
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        CmdUpdateTransform(transform.position, transform.rotation);
        if (Input.GetMouseButtonDown(0))
        {
            //heroManager.Emerge();
            GetComponent<VisibilityChangeBehavior>().CmdEmerge();
            if (Bullet == null)
                Debug.Log("δ�ҵ�prefab");
            CmdFire(mousePos, gameObject);            
        }
    }
    [Command]
    void CmdFire(Vector3 mousePos, GameObject gameObject)
    {
        gunDirection = (mousePos - transform.position).normalized;
        if (gunDirection.sqrMagnitude == 0) gunDirection = new Vector2(0, 1);
        GameObject newBullet = Instantiate(Bullet, MuzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        newBullet.GetComponent<BulletBehavior>().Direction = gunDirection;
        newBullet.GetComponent<BulletBehavior>().Shooter = gameObject;
        NetworkServer.Spawn(newBullet, gameObject);
    }

    [Command]
    void CmdUpdateTransform(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        //Debug.Log($"Update:{position},{rotation.eulerAngles}");
    }
}
