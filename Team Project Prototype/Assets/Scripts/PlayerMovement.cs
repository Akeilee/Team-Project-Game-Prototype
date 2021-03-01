using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 10;
    public float jumpForce = 9.8f;
    private Animator m_animator;
    private int collisionCount = 0;

    //slopes
    private float slopeForce = 10;
    private float slopeForceRayLength = 2;

    public bool slope;
    public float slopeAngle;
    private float minSlope = 32;

    public bool slowDown;
    Vector3 movement;
    Vector3 newMovement;

    Vector3 lastPosition;
    public bool uphill;
    public bool ignore;
    public PhysicMaterial physicMat;
    public bool onSlopeFloor;

    void Start() {
        rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Grounded", true);

        slowDown = false;
        ignore = false;
        uphill = false;
        slope = false;
        onSlopeFloor = false;
        rb.GetComponent<CapsuleCollider>().material = null;
    }


    void Update() {

        Vector3 currentPosition = transform.position;
        if (currentPosition.y >= lastPosition.y) {
            uphill = true;
        }
        else if (currentPosition.y < lastPosition.y) {
            uphill = false;
        }

        lastPosition = currentPosition;

        if (onSlopeFloor == true) {
            OnSlope();
        }

        PlayerMove();

    }

    void FixedUpdate() {
        rb.AddForce(Physics.gravity);
    }

    private void PlayerMove() {
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float z = Input.GetAxisRaw("Vertical") * speed;

        float y = transform.position.y;

        movement = transform.right * x + transform.forward * z;
        newMovement = new Vector3(movement.x, rb.velocity.y, movement.z);




        //animator movement
        if (collisionCount == 0) {
            m_animator.SetBool("Grounded", false);
            //rb.velocity = new Vector3(movement.x, -jumpForce, movement.z);
            rb.AddForce(new Vector3(0, -9.8f, 0));
        }

        if (collisionCount > 0) {
            m_animator.SetBool("Grounded", true);

            if (slowDown ==true) { //on water
                rb.velocity = newMovement / 2.5f;
            }
            else if (slope == true && uphill == false && ignore == false) {
                rb.AddForce(Vector3.down * 5);
                rb.AddForce(Vector3.forward * 100);
                //rb.AddForce(transform.forward * 2);
                rb.velocity = new Vector3(movement.x * 1.5f, rb.velocity.y, movement.z *1.5f);
                //rb.velocity = newMovement;
            }
            else {
                rb.AddForce(Vector3.zero);
                rb.velocity = newMovement;
            }
            
        }

        m_animator.SetFloat("MoveSpeed", newMovement.magnitude);
    }

    private bool OnSlope() {

        RaycastHit hit; //hitting ground
        RaycastHit forwardHit;


        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1 * slopeForceRayLength)) {  //raycast from player to ground
            if (hit.normal != Vector3.up) {
                
                slopeAngle = Vector3.Angle(Vector3.up, hit.normal);

                if (slopeAngle >= minSlope) {

                    if (Physics.Raycast(transform.position, Vector3.down, out forwardHit, 1f) && ignore == false) {  //prevent bouncing from jumping down slope
                        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*forwardHit.distance, Color.red);
                        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - forwardHit.distance + 0.01f, rb.transform.position.z);
                    }

                    rb.GetComponent<CapsuleCollider>().material = physicMat;
                    slope = true;
                    return true; //on slope
                }

               
            }     


        }
        rb.GetComponent<CapsuleCollider>().material = null;
        slope = false;
        return false;
        
    }




    private void OnCollisionEnter(Collision collision) {
        collisionCount++;


    }

    private void OnCollisionExit(Collision collision) {
        collisionCount--;

    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pond") {
            slowDown = true;
        }

        if (other.gameObject.tag == "IgnoreFloor") {
            ignore = true;
        }

        if (other.gameObject.tag == "SlopeFloor") {
            onSlopeFloor = true;
        }

    }


    private void OnTriggerExit(Collider other) {
        slowDown = false;
        ignore = false;
        onSlopeFloor = false;
    }
}
