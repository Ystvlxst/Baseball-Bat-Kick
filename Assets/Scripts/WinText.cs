using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinText : MonoBehaviour
{
    private bool _isFinished;

    private void Start()
    {
        _isFinished = false;
    }

    public void TryFinished(bool isFinished)
    {
        _isFinished = isFinished;

        if (isFinished == true)
            gameObject.SetActive(true);
    }
}
