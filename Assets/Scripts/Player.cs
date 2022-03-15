using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyes;
    [SerializeField] private float _speed;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _currentBody;
    [SerializeField] private GameObject _newBody;
    [SerializeField] private ParticleSystem _hitEffect;

    private CharacterController _controller;
    private MobileMovement _mMovement;
    private float _seeDistance = 10;
    private Vector3 _moveVector;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isClick;
    private const string _preHit = "preHit";

    private void Start()
    {
        _mMovement = GetComponent<MobileMovement>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        _renderer.enabled = true;
        _renderer.sharedMaterial = _materials[0];
    }

    private void Update()
    {
        Movement();
        Hit();
    }

    private void Movement()
    {
        _moveVector.x = _mMovement.Horizontal() * _speed;
        _moveVector.z = _mMovement.Vertical() * _speed;

        _controller.Move(_moveVector * Time.deltaTime);
    }

    private void Hit()
    {
        if (_isClick == true)
        {
            _animator.SetBool(_preHit, true);
            StartCoroutine(SetEuler());
        }
        else
        {
            _animator.SetBool(_preHit, false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IHitable>(out IHitable iHitable))
            _hitEffect.Play();
    }

    private IEnumerator SetEuler()
    {
        yield return new WaitForSeconds(2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void KickEnemy()
    {
        foreach (Enemy enemy in _enemyes)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= _seeDistance)
            {
                enemy.PlayerKickMe();
            }
        }
    }

    private IEnumerator HitButtonClick()
    {
        _isClick = true;
        yield return new WaitForSeconds(1f);
        _isClick = false;
    }

    public void Hittting()
    {
        StartCoroutine(HitButtonClick());
    }

    public void ChangeMaterial()
    {
        _renderer.sharedMaterial = _materials[1];
    }

    public void ChangeBody()
    {
        Vector3 panPosition = new Vector3(6.3f, 0.1f, -609f);
        Instantiate(_newBody, panPosition, Quaternion.Euler(0, 0, 0));
        gameObject.SetActive(false);
    }
}
