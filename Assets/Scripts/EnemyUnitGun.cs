using System.Collections;
using UnityEngine;

public class EnemyUnitGun : MonoBehaviour
{
    public float msBetweenShots = 100;

    //The weapon
    public GameObject weapon;

    //The bullet
    public GameObject bullet;

    //Used as a position for weapon initiation
    public GameObject weaponHolder;

    //Used as a position for bullet initiation
    public GameObject bulletHolder;

    public GameObject bulletInit;

    private float nextShotTime;

    private void Start()
    {
        GameObject gun = Instantiate(weapon, weaponHolder.transform.position, Quaternion.identity) as GameObject;
        gun.transform.parent = this.transform.GetChild(1);
        bulletHolder = GameObject.Find("BulletHolder");
    }

    private void Update()
    {
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Instantiate(bullet, bulletInit.transform.position, this.gameObject.transform.rotation);
        }
    }
}