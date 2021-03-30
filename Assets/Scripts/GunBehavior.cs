using Assets.Scripts;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior: NetworkBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject Bullet;
    public Transform MuzzleTransform;
    public float x;

    //private VisibilityManager heroManager;
    private Vector2 gunDirection;
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        x = transform.localScale.x;
    }
    public override void OnStartClient()
    {
        x = transform.localScale.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gunDirection = (mousePos - transform.position).normalized;
        if (gunDirection.sqrMagnitude == 0) gunDirection = new Vector2(0, 1);
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        CmdUpdateTransform(transform.position, transform.rotation,transform.localScale);
        if (Input.GetMouseButtonDown(0))
        {
            //heroManager.Emerge();
            GetComponent<VisibilityChangeBehavior>().CmdEmerge();
            if (Bullet == null)
                Debug.Log("δ�ҵ�prefab");
            CmdFire(mousePos, gameObject);            
        }
        if ((mousePos.x - transform.position.x) > 0)
        {
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
            Gun1.transform.RotateAround(circle1.transform.position, new Vector3(0, 0, 1), angle - Gun1.transform.rotation.eulerAngles.z);
            Gun2.transform.RotateAround(circle2.transform.position, new Vector3(0, 0, 1), angle - Gun2.transform.rotation.eulerAngles.z);
        }
        else
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            Gun1.transform.RotateAround(circle1.transform.position, new Vector3(0, 0, 1), Mathf.PI * Mathf.Rad2Deg + angle - Gun1.transform.rotation.eulerAngles.z);
            Gun2.transform.RotateAround(circle2.transform.position, new Vector3(0, 0, 1), Mathf.PI * Mathf.Rad2Deg + angle - Gun2.transform.rotation.eulerAngles.z);
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
    void CmdUpdateTransform(Vector3 position, Quaternion rotation, Vector3 localscale)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        this.transform.localScale = localscale;
        //Debug.Log($"Update:{position},{rotation.eulerAngles}");
    }
}
