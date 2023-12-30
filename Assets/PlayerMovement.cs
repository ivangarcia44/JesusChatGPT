using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;
    public float speed = 10;
    public float rotationSpeed = 1;

    public float runSpeed = 15;
    public float jumpForce = 200;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.mainInputMap.Movement.Jump.started += _isGrounded => Jump();
    }

    void Update() {
        float forward = inputManager.mainInputMap.Movement.Forward.ReadValue<float>();
        float rotation = inputManager.mainInputMap.Movement.Rotation.ReadValue<float>();
        Vector3 move = transform.forward * forward;

        move *= inputManager.mainInputMap.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;

        transform.localScale = 
            new Vector3(1, inputManager.mainInputMap.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.72618f, 1);

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        var rotationInput = inputManager.mainInputMap.Movement.Rotation.ReadValue<float>();
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Ground")) {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.transform.CompareTag("Ground")) {
            _isGrounded = false;
        }
    }

    void Jump() {
        if (_isGrounded) {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
