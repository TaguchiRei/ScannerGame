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
    private float _EndTimingTimer;
    
    bool MaxParticle = false;
    
    private void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
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
        
        if (MaxParticle)
        {
            if(_EndTimingTimer <= Time.time)
            {
                _EndTimingTimer = Time.time + 2f;
                _visualEffect.SendEvent("OnPlay");
            }
        }
    }

    public void DisplayParticle()
    {
        if (MaxParticle) return;
        if (Random.Range(0, 5) == 0)
            _particleCount++;
        _visualEffect.SetInt("Count", _particleCount);
        if (_particleCount >= 20)
        {
            MaxParticle = true;
            _particleCount = 100;
            _visualEffect.SetInt("Count", _particleCount);
        }
        _visualEffect.SendEvent("OnPlay");
    }
}