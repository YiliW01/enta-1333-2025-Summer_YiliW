using DG.Tweening;
using UnityEngine;
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

    [SerializeField] Transform cameraTransform;
    private Vector3 input;
    private float panSpeed = 10f;
    private bool keyboardPanning;

    private void Update()
    {
        cameraTransform.Translate(input * Time.deltaTime * panSpeed, Space.World);
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused)
        {
            isPaused = true;
            pausePanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutQuad);
            return;
        }

        if (context.performed && isPaused)
        {
            isPaused = false;
            pausePanel.DOAnchorPosY(closedPos, transitionDuration).SetEase(Ease.OutQuad);
        }
    }

    public void WASD(InputAction.CallbackContext context)
    {
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
            Debug.Log($"{context.ReadValue<Vector2>()}");

            if (context.ReadValue<Vector2>().x > (Screen.width * 0.9f)) { input.x = 1f; }
            else if (context.ReadValue<Vector2>().x < (Screen.width * 0.1f)) { input.x = -1f; } 
            else { input.x = 0f; }

            if (context.ReadValue<Vector2>().y > (Screen.height * 0.9f)) { input.z = 1f; }
            else if (context.ReadValue<Vector2>().y < (Screen.height * 0.1f)) { input.z = -1f; } 
            else { input.z = 0f; }
        }
    }
}
