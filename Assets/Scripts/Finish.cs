using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finish;
    [SerializeField] private WinText _winText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _finish.Play();
            _winText.PlayerFinished();
        }
    }
}
