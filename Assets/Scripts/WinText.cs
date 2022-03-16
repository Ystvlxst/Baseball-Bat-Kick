using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinText : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PlayerFinished()
    {
        gameObject.SetActive(true);
    }
}
