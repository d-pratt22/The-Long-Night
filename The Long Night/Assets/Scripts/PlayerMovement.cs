using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float aimMoveSpeed = 1f;
    public Transform cameraTransform;

    public int damage = 10;

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

            if (camScript != null && camScript.isAiming)
            {
                // Face same direction as camera
                Vector3 camFlatForward = camForward.normalized;
                if (camFlatForward != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(camFlatForward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
                }
            }
            else
            {
                // Rotate toward movement direction
                Quaternion targetRotation = Quaternion.LookRotation(movementInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }
        }
        else
        {
            movementInput = Vector3.zero;
        }

        // Fire on left click
        if (camScript != null && camScript.isAiming && Input.GetMouseButtonDown(0))
        {
            FireFromMouse();
        } 
    }

    void FireFromMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            Debug.Log("Hit object: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, hit.point);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPos; 
        

        if (camScript.isAiming)
        {
            targetPos = rb.position + movementInput * aimMoveSpeed * Time.fixedDeltaTime;  
        }

        else
        {
            targetPos = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
        }

        rb.MovePosition(targetPos);
    }
}