using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyes;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _currentBody;
    [SerializeField] private GameObject _newBody;
    [SerializeField] private ParticleSystem _hitEffect;

    private float _seeDistance = 10;
    private Renderer _renderer;
    private Animator _animator;
    private const string _preHit = "preHit";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();

        _renderer.enabled = true;
        _renderer.sharedMaterial = _materials[0];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IHitable iHitable))
            _hitEffect.Play();
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

    private IEnumerator Hit()
    {
        _animator.SetBool(_preHit, true);
        StartCoroutine(SetEuler());
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool(_preHit, false);

    }

    private IEnumerator SetEuler()
    {
        yield return new WaitForSeconds(1.25f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Hitting()
    {
        StartCoroutine(Hit());
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
