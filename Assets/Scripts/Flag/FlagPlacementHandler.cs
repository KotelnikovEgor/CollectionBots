using UnityEngine;
using UnityEngine.InputSystem;

public class FlagPlacementHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _baseLayer;
    [SerializeField] private LayerMask _groundLayer;

    private Camera _camera;
    private PlayerInput _playerInput;
    private Base _selectedBase;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Enable();
        _playerInput.Gameplay.Click.performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Click.performed -= OnClickPerformed;
        _playerInput.Gameplay.Disable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit baseHit, Mathf.Infinity, _baseLayer))
        {
            if (baseHit.collider.TryGetComponent(out Base clickedBase))
            {
                _selectedBase = clickedBase;
                return;
            }
        }

        if (_selectedBase != null && Physics.Raycast(ray, out RaycastHit groundHit, Mathf.Infinity, _groundLayer))
        {
            _selectedBase.SetFlag(groundHit.point);
            _selectedBase = null;
        }
    }
}
