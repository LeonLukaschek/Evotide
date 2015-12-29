using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float damage;

    private GameObject bulletHolder;

    private void Start()
    {
        bulletHolder = GameObject.Find("BulletHolder");
        this.transform.parent = bulletHolder.transform;

        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider collision)
    {
        Destroy(this.gameObject);
    }
}