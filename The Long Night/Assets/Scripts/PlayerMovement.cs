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
    private bool hasMovementInput;

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
            hasMovementInput = true;

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
            hasMovementInput= false;

            movementInput = Vector3.zero; 
        }
    }

    private void HandleRotation()
    {
        bool isAiming = camScript != null && camScript.isAiming;

        if (isAiming)
        {
            RotateTowardCursor();
            return;
        }

        if (movementInput.magnitude < 0.01f)
        {
            movementInput = Vector3.zero;
            return; 
        }

        if (!hasMovementInput) return;

         Quaternion targetRotation = Quaternion.LookRotation(movementInput);
         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        
    }

    private void RotateTowardCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 lookDir = hitPoint - transform.position;
            lookDir.y = 0f; // keep rotation flat

            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 15f * Time.deltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        float speed = (camScript != null && camScript.isAiming) ? aimMoveSpeed : moveSpeed;
        Vector3 targetPos = rb.position + movementInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }
}