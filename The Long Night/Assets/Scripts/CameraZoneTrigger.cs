using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    public Transform cameraAnchor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cameraAnchor != null)
        {
            FixedCamera cam = Camera.main.GetComponent<FixedCamera>();
            if (cam != null)
            {
                cam.SetAnchor(cameraAnchor);
            }
        }
    }
}