using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _seeDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Player _trigger;

    private Vector3 _kickDirection = new Vector3(0, 25, 65);
    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _hitSound;
    private string _onHit = "onHit";
    private string _isKick = "isKick";

    private void Start()
    {
        _hitSound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Vector3.Distance(gameObject.transform.position, _trigger.transform.position) <= _seeDistance)
        {
            transform.LookAt(_trigger.transform);
            transform.Translate(new Vector3(0, 0, _speed) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            _speed = 0;
    }

    private IEnumerator Kick()
    {
        float forceKick = 30f;

        _hitSound.Play();
        _rigidbody.AddForce(_kickDirection * forceKick);
        _animator.SetBool(_onHit, true);
        _animator.SetBool(_isKick, true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public void KickedOfPlayer()
    {
        StartCoroutine(Kick());
    }
}
