using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 _kick;

    private Enemy _enemy;
    private Rigidbody _rb;
    private float _forceKick = 10f;
    private string _player = "Player";
    private string _enemy_ = "Enemy";

    public Rigidbody Rb => _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _enemy = GameObject.FindGameObjectWithTag(_enemy_).GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == _player)
            _rb.AddForce(_kick * _forceKick);
    }
}
