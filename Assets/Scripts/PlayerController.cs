using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  public float walkSpeed = 10;
  public float lookSpeed = 10;
  Rigidbody rb;
  Vector3 moveDirection;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void Update() {
    float horizontalMovement = Input.GetAxisRaw("Horizontal");
    float verticalMovement = Input.GetAxisRaw("Vertical");

    moveDirection = (verticalMovement * transform.forward).normalized;
  }

  void FixedUpdate() {
    Move();
  }

  void Move() {
    rb.velocity = moveDirection * walkSpeed * Time.deltaTime;
    transform.Rotate(0, Input.GetAxis("Horizontal") * lookSpeed*Time.deltaTime, 0);
  }
}
