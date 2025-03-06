using System;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private VisualEffect _visualEffect;
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private Vector3 _velocity;
    
    private GameObject _playerObj;
    private int _particleCount = 0;

    private void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_playerObj != null)
        {
            if (Vector3.Distance(_playerObj.transform.position, transform.position) <= 50f)
            {
                transform.rotation = Quaternion.LookRotation(_playerObj.transform.position - transform.position);
                _rigidbody.linearVelocity = transform.forward * _moveSpeed;
            }
        }
        else
        {
            Debug.Log("Player object not found");
        }
    }

    public void AddParticle()
    {
        _particleCount++;
        _visualEffect.SetInt("Count",_particleCount);
        _visualEffect.Play();
    }
}
