using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _currentObject;
    [SerializeField] private GameObject[] _destroyedWall;

    private void Start()
    {
        _currentObject.SetActive(true);

        foreach (GameObject newObject in _destroyedWall)
            newObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.TryGetComponent(out DestroyerWall destroyerWall))
        {
            foreach (GameObject newObject in _destroyedWall)
                newObject.SetActive(true);

            _currentObject.SetActive(false);
        }
    }
}
