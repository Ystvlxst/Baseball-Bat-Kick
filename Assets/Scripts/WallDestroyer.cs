using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _currentObject;
    [SerializeField] private GameObject[] _destroyedWall;

    private string _player = "Player";
    private string _ball = "Ball";
    private string _enemy = "Enemy";

    private void Start()
    {
        _currentObject.SetActive(true);

        foreach (GameObject newObject in _destroyedWall)
            newObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == _player || col.gameObject.tag == _ball || col.gameObject.tag == _enemy)
        {
            foreach (GameObject newObject in _destroyedWall)
                newObject.SetActive(true);

            _currentObject.SetActive(false);
        }
    }
}
