using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class DestroyedWall : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private string _isDestroy = "isDestroy";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AnimationPlay();
    }

    private void AnimationPlay()
    {
        if (gameObject != null)
            StartCoroutine(DestroyTime());
    }

    private IEnumerator DestroyTime()
    {
        float forceKick = 0.2f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        
        _animator.SetBool(_isDestroy, true);
        _rigidbody.AddForce(Vector3.forward * forceKick, ForceMode.Impulse);
        yield return waitForSeconds;
        gameObject.SetActive(false);
    }
}
