using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speed;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _currentBody;
    [SerializeField] private GameObject _newBody;
    [SerializeField] private ParticleSystem _hitEffect;

    private Renderer _rend;
    private Rigidbody _rb;
    private Animator _animator;

    private string _preHit = "preHit";
    private string _enemy_ = "Enemy";
    private string _ball = "Ball";
    private string _box = "Box";
    private string _wall = "Wall";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rend = GetComponent<Renderer>();

        _rend.enabled = true;
        _rend.sharedMaterial = _materials[0];
    }

    private void FixedUpdate()
    {
        _enemy = GameObject.FindGameObjectWithTag(_enemy_).GetComponent<Enemy>();
        Movement();
        Hit();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.S))
            _rb.AddForce(-transform.forward * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.D))
            _rb.AddForce(transform.right * _speed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.A))
            _rb.AddForce(-transform.right * _speed, ForceMode.Impulse);
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
        if (other.gameObject.tag == _enemy_ || other.gameObject.tag == _ball || other.gameObject.tag == _box || other.gameObject.tag == _wall)
            _hitEffect.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ChangeBody>(out ChangeBody changeBody))
        {
            changeBody._hooray.Play();
            ChangeBody();
        }
    }

    private void ChangeBody()
    {
        Vector3 panPosition = new Vector3(6.3f, 0.1f, -609f);
        Instantiate(_newBody, panPosition, Quaternion.Euler(0, 0, 0));
        gameObject.SetActive(false);
    }

    private IEnumerator SetEuler()
    {
        yield return new WaitForSeconds(1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator KickEnemy()
    {
        float seeDistance = 10;
        
        if (Vector3.Distance(transform.position, _enemy.transform.position) <= seeDistance)
        {
            _enemy.HitSound.Play();
            _enemy.EnemyRigidbody.AddForce(_enemy.KickDirection * _enemy.ForceKick);
            _enemy.EnemyAnimator.SetBool(_enemy.OnHit, true);
            _enemy.EnemyAnimator.SetBool(_enemy.IsKick, true);
            yield return new WaitForSeconds(1.5f);
            _enemy.gameObject.SetActive(false);
        }
    }

    public void ChangeMaterial()
    {
        _rend.sharedMaterial = _materials[1];
    }
}
