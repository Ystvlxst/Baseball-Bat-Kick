using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMover : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject _containerPlayer;

    private Player _player;
    private float _playerSpeed = 25f;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        MoveForvard();
    }

    private void MoveForvard()
    {
        Vector3 position = _containerPlayer.transform.position;
        position.z += _playerSpeed * Time.deltaTime;
        _containerPlayer.transform.position = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float positionXfactor = 0.025f;
        float deltaXfactor = 1.5f;
        Vector2 delta = eventData.delta;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            Vector3 position = _containerPlayer.transform.position;
            position.x += deltaXfactor * delta.x * positionXfactor;
            _containerPlayer.transform.position = position;
        }
        else
        {
            if (delta.y < 0)
                _player.TryHitEnemy();
        }
    }
}
