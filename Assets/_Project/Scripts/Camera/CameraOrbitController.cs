using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraOrbitController : MonoBehaviour
{
    [Header("Referencias")]
    public CinemachineCamera virtualCamera;

    [Header("Configuración de Velocidad")]
    public float keyboardSensitivity = 50f;
    public float mouseHorizontalSensitivity = 0.5f;
    public float scrollVerticalSensitivity = 1.5f;

    private CinemachineOrbitalFollow orbitalFollow;

    void Start()
    {
        if (virtualCamera != null)
            orbitalFollow = virtualCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    void Update()
    {
        if (orbitalFollow == null) return;

        float horizontalDelta = 0;
        float verticalDelta = 0;
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalDelta -= keyboardSensitivity * Time.deltaTime;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalDelta += keyboardSensitivity * Time.deltaTime;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalDelta += keyboardSensitivity * Time.deltaTime;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalDelta -= keyboardSensitivity * Time.deltaTime;
        }

        if (Mouse.current != null && (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed))
        {
            float mouseX = Mouse.current.delta.ReadValue().x;
            horizontalDelta += mouseX * mouseHorizontalSensitivity;
        }

        if (Mouse.current != null)
        {
            float scrollValue = Mouse.current.scroll.ReadValue().y;
            if (Mathf.Abs(scrollValue) > 0.1f)
            {
                verticalDelta += (scrollValue > 0 ? 1 : -1) * scrollVerticalSensitivity;
            }
        }

        orbitalFollow.HorizontalAxis.Value += horizontalDelta;
        orbitalFollow.VerticalAxis.Value += verticalDelta;
    }
}