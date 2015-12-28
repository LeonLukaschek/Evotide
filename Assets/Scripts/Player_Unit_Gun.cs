using System.Collections;
using UnityEngine;

public class Player_Unit_Gun : MonoBehaviour
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

    private float nextShotTime;

    private void Start()
    {
        GameObject gun = Instantiate(weapon, weaponHolder.transform.position, Quaternion.identity) as GameObject;
        gun.transform.parent = this.transform.GetChild(1);
    }

    private void Update()
    {
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            GameObject newProjectile = Instantiate(bullet, bulletHolder.transform.position, this.gameObject.transform.rotation) as GameObject;
        }
    }
}