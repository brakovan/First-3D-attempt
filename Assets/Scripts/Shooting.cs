using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shotPoint;

    private GameObject[] bulletPool;

    [SerializeField]
    private int numberBullets;
    [SerializeField]
    private float shotForce;
    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private float shotDistance;
    

    private void Awake()
    {
        bulletPool = new GameObject[numberBullets];

        for (int i = 0; i < bulletPool.Length; i++)
            bulletPool[i] = Instantiate(bulletPrefab);
    }

    private void Start()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Delete());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));

            Shot();

            yield return new WaitForSecondsRealtime(1 / rateOfFire);
        }
    }

    private IEnumerator Delete()
    {
        while (true)
        {
            BulletDelete();
            yield return null;
        }
    }

    private void Shot()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (bulletPool[i].activeInHierarchy)
                continue;

            Debug.Log(i);
            bulletPool[i].SetActive(true);
            bulletPool[i].transform.position = shotPoint.position;
            bulletPool[i].GetComponent<Rigidbody>().AddForce(shotPoint.forward * shotForce, ForceMode.Impulse);

            break;
        }
    }

    private void BulletDelete()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
                continue;

            if (Vector3.Distance(shotPoint.position, bulletPool[i].transform.position) > shotDistance)
            {
                bulletPool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletPool[i].SetActive(false);

                break;
            }
        }
    }
}
