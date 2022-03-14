using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _trigger;
    [SerializeField] private float _seeDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _kickDirection;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _hitSound;

    private float _forceKick = 30f;

    private string _onHit = "onHit";
    private string _isKick = "isKick";

    public Vector3 KickDirection => _kickDirection;
    public Animator EnemyAnimator => _animator;
    public AudioSource HitSound => _hitSound;
    public Rigidbody EnemyRigidbody => _rigidbody;
    public float ForceKick => _forceKick;
    public string OnHit => _onHit;
    public string IsKick => _isKick;

    private void Start()
    {
        _hitSound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _trigger = GetComponent<Player>();
        Movement();
    }

    private void Movement()
    {
        if (Vector3.Distance(transform.position, _trigger.transform.position) <= _seeDistance)
        {
            transform.LookAt(_trigger.transform);
            transform.Translate(new Vector3(0, 0, _speed));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
            _speed = 0;
    }
}
