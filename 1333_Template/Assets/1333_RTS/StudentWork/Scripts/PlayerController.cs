using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //temp pause panel (should use UI manager)
    [SerializeField] private RectTransform pausePanel;
    [SerializeField] private CanvasGroup pauseCanvas;
    private float openPos = 0f;
    private float closedPos = -1050f;
    [SerializeField] private float transitionDuration;

    //Temporary pause check
    private bool isPaused = false;

    [SerializeField] CinemachineCamera cam;
    private Vector3 input;
    private float panSpeed = 10f;
    private bool keyboardPanning;
    private float zoom;
    private float zoomSpeed = 50f;
    private float maxZoom = 20f;
    private float minZoom = 50f;

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused)
        {
            pausePanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutQuad);
            isPaused = true;
            return;
        }

        if (context.performed && isPaused)
        {
            pausePanel.DOAnchorPosY(closedPos, transitionDuration).SetEase(Ease.OutQuad);
            isPaused = false;
        }
    }
    private void MoveCamera()
    {
        cam.transform.Translate(input * Time.deltaTime * panSpeed, Space.World);
    }

    private void ZoomCamera()
    {
        cam.Lens.FieldOfView = cam.Lens.FieldOfView + (zoom * Time.deltaTime * zoomSpeed);
        if (cam.Lens.FieldOfView <= maxZoom) { cam.Lens.FieldOfView = maxZoom; }
        if (cam.Lens.FieldOfView >= minZoom) { cam.Lens.FieldOfView = minZoom; }
    }

    public void WASD(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        if (context.performed) { keyboardPanning = true; }
        else { keyboardPanning = false; }

        if (!isPaused)
        {
            Debug.Log($"{context.ReadValue<Vector2>()}");
            input.x = context.ReadValue<Vector2>().x * 2;
            input.z = context.ReadValue<Vector2>().y * 2;
        }
    }

    public void MousePan(InputAction.CallbackContext context)
    {
        if (keyboardPanning) return;
        if (!isPaused)
        {
            //Debug.Log($"{context.ReadValue<Vector2>()}");

            if (context.ReadValue<Vector2>().x > (Screen.width * 0.9f)) { input.x = 1f; }
            else if (context.ReadValue<Vector2>().x < (Screen.width * 0.1f)) { input.x = -1f; } 
            else { input.x = 0f; }

            if (context.ReadValue<Vector2>().y > (Screen.height * 0.9f)) { input.z = 1f; }
            else if (context.ReadValue<Vector2>().y < (Screen.height * 0.1f)) { input.z = -1f; } 
            else { input.z = 0f; }
        }
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            //Debug.Log($"{context.ReadValue<Vector2>()}");

            if (context.ReadValue<Vector2>().y >= 1) { zoom = -2; }
            else if (context.ReadValue<Vector2>().y <= -1) { zoom = 2; }
            else { zoom = 0; }
        }
    }
}
