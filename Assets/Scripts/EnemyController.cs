using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject player;
    private Vector3 targetPosition; 

    [SerializeField]
    private float enemyMoveSpeed;
    [SerializeField]
    private float visibilityRadius;
    public float HP;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetTargetPosition();

        if (Vector3.Distance(transform.position, player.transform.position) < visibilityRadius)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemyMoveSpeed * Time.deltaTime);
        }
        else 
            transform.LookAt(Vector3.forward);

        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void  GetTargetPosition()
    {
        targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
