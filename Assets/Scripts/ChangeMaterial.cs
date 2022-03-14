using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _hooray;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _hooray.Play();
            _player.ChangeMaterial();
        }
    }
}
