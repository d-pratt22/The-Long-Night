using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform target;
    public Transform anchorPoint;

    public float lookSpeed = 5f;
    public float positionLerpSpeed = 5f;

    public float normalFOV = 60f;
    public float aimFOV = 40f;
    public float fovLerpSpeed = 5f;

    private Camera cam;

    public bool isAiming { get; private set; }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target || !anchorPoint) return;

        // Follow anchor point
        transform.position = Vector3.Lerp(transform.position, anchorPoint.position, positionLerpSpeed * Time.deltaTime);

        // Look at player
        Vector3 directionToPlayer = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);

        // Handle aim zoom
        isAiming = Input.GetMouseButton(1); // Right click
        float targetFOV = isAiming ? aimFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovLerpSpeed * Time.deltaTime);
    }

    public void SetAnchor(Transform newAnchor)
    {
        anchorPoint = newAnchor;
    }
}