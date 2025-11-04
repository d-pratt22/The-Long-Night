using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Vector2 hotSpot = Vector2.zero;

    private FixedCamera camScript;

    void Start()
    {
        camScript = Camera.main.GetComponent<FixedCamera>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (camScript == null) return;

        if (camScript.isAiming && crosshairTexture != null)
        {
            Cursor.SetCursor(crosshairTexture, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}

