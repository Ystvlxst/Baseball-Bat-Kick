using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private Vector3 _directionThrow = new Vector3(4, 0, -20);
    private float _forceThrow = 50f;

    public void ThrowBall()
    {
        _ball.Rb.AddForce(_directionThrow * _forceThrow);
    }
}
