using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            return;

        collision.gameObject.GetComponent<EnemyController>().HP 
            = collision.gameObject.GetComponent<EnemyController>().HP - bulletDamage;

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
