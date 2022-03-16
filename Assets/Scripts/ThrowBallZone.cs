using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallZone : MonoBehaviour
{
    [SerializeField] private BallEnemy _ballEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _ballEnemy.ThrowBall();
    }
}
