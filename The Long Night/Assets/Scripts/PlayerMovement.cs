using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float aimMoveSpeed = 1f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 movementInput;
    private FixedCamera camScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camScript = Camera.main.GetComponent<FixedCamera>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleRotation();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Camera-relative movement
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            movementInput = camForward * inputDirection.z + camRight * inputDirection.x;
        }

        else
        {
            movementInput = Vector3.zero; 
        }
    }

    private void HandleRotation()
    {

        if (movementInput == Vector3.zero) return; 


        if (camScript != null && camScript.isAiming)
        {
            /*// Face same direction as camera
            Vector3 camForward = cameraTransform.forward;
            if (camForward != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(camForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
            }*/
        }
        else
        {
            // Rotate toward movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        float speed = (camScript != null && camScript.isAiming) ? aimMoveSpeed : moveSpeed;
        Vector3 targetPos = rb.position + movementInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }
}