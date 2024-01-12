using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private Transform groundCheckTransform;
    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody rb;
    private Vector3 mousePosition;
    private Vector3 whatToLookAt;

    [SerializeField]
    private float movePlayerSpeed;
    [SerializeField]
    private float jumpPlayerForce;

    private float axisX;
    private float axisY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(axisX * movePlayerSpeed, rb.velocity.y, axisY * movePlayerSpeed);
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;

        InputAxisXY();
        PlayerLook();

        if (Input.GetButton("Jump") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPlayerForce, rb.velocity.z);
        }
    }

    void InputAxisXY()
    {
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");
    }

    private void PlayerLook()
    {
        Ray ray = m_camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            whatToLookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }

        transform.LookAt(whatToLookAt);
    }

    private bool Grounded()
    {
        return Physics.CheckSphere(groundCheckTransform.position, .1f, groundLayer, QueryTriggerInteraction.Ignore);
    }
}
