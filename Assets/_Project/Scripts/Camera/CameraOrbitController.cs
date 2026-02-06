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

        // 1. WASD & Arrows: Control Total (H y V)
        if (Keyboard.current != null)
        {
            // Horizontal
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalDelta -= keyboardSensitivity * Time.deltaTime;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalDelta += keyboardSensitivity * Time.deltaTime;

            // Vertical
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalDelta += keyboardSensitivity * Time.deltaTime;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalDelta -= keyboardSensitivity * Time.deltaTime;
        }

        // 2. Click y Arrastre: Solo Horizontal
        if (Mouse.current != null && (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed))
        {
            float mouseX = Mouse.current.delta.ReadValue().x;
            horizontalDelta += mouseX * mouseHorizontalSensitivity;
        }

        // 3. Scroll: Solo Vertical (Simulando W/S)
        if (Mouse.current != null)
        {
            float scrollValue = Mouse.current.scroll.ReadValue().y;
            if (Mathf.Abs(scrollValue) > 0.1f)
            {
                // Multiplicamos por 10 para que el "paso" del scroll se sienta natural
                verticalDelta += (scrollValue > 0 ? 1 : -1) * scrollVerticalSensitivity;
            }
        }

        // Aplicar los cambios a Cinemachine 3
        orbitalFollow.HorizontalAxis.Value += horizontalDelta;
        orbitalFollow.VerticalAxis.Value += verticalDelta;
    }
}