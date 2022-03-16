using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMover : MonoBehaviour, IDragHandler
{
    [SerializeField] private Player[] _playerBodyes;
    private float _playerSpeed = 25f;

    private void Update()
    {
        PlayerMoveForvard();
    }

    private void PlayerMoveForvard()
    {
        foreach (var currentPlayerBody in _playerBodyes)
        {
            Vector3 position = currentPlayerBody.transform.position;
            position.z += _playerSpeed * Time.deltaTime;
            currentPlayerBody.transform.position = position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float positionXfactor = 0.025f;
        float deltaXfactor = 1.5f;
        Vector2 delta = eventData.delta;

        foreach (var currentPlayerBody in _playerBodyes)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                Vector3 position = currentPlayerBody.transform.position;
                position.x += deltaXfactor * delta.x * positionXfactor;
                currentPlayerBody.transform.position = position;
            }
            else
            {
                if (delta.y < 0)
                    currentPlayerBody.Hitting();
            }
        }
    }
}
