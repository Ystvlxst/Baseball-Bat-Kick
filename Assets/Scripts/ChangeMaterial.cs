using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hooray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _hooray.Play();
            player.ChangeMaterial();
        }
    }
}
