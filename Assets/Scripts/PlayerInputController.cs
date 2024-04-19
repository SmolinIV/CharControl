using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Mover _mover;
   
    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void FixedUpdate()
    {
       _mover.UpdateMovingDirection(_playerInput.Player.Move.ReadValue<Vector2>());
       _mover.UpdateLookingDirection(_playerInput.Player.Look.ReadValue<Vector2>());
    }
}
