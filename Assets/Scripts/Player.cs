using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyes;
    [SerializeField] private Vector3 _kickDirection;
    [SerializeField] private float _speed;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _currentBody;
    [SerializeField] private GameObject _newBody;
    [SerializeField] private ParticleSystem _hitEffect;

    private float _seeDistance = 10;
    private float _forceKick = 30f;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private string _preHit = "preHit";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        _renderer.enabled = true;
        _renderer.sharedMaterial = _materials[0];
    }

    private void FixedUpdate()
    {
        Movement();
        Hit();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.S))
            _rigidbody.AddForce(-transform.forward * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.D))
            _rigidbody.AddForce(transform.right * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.A))
            _rigidbody.AddForce(-transform.right * _speed, ForceMode.Impulse);
    }

    private void Hit()
    {
        if (Input.GetKey(KeyCode.Space))
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
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy) || other.gameObject.TryGetComponent<Ball>(out Ball ball) || other.gameObject.TryGetComponent<Box>(out Box box) || other.gameObject.TryGetComponent<Wall>(out Wall wall))
            _hitEffect.Play();
    }

    private IEnumerator SetEuler()
    {
        yield return new WaitForSeconds(1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator KickEnemy()
    {
        foreach (Enemy enemy in _enemyes)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= _seeDistance)
            {
                enemy.HitSound.Play();
                enemy.EnemyRigidbody.AddForce(_kickDirection * _forceKick);
                enemy.EnemyAnimator.SetBool(enemy.OnHit, true);
                enemy.EnemyAnimator.SetBool(enemy.IsKick, true);
                yield return new WaitForSeconds(1.5f);
                enemy.gameObject.SetActive(false);
            }
        }
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
