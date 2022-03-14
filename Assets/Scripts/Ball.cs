using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 _kick;

    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private float _forceKick = 10f;

    public Rigidbody Rb => _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
            _rigidbody.AddForce(_kick * _forceKick);
    }
}
