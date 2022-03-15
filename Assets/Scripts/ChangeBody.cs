using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBody : MonoBehaviour
{
    [SerializeField] public ParticleSystem _hooray;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _hooray.Play();
            player.ChangeBody();
        }
    }
}
