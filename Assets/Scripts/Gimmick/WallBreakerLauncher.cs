using System;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakerLauncher : MonoBehaviour
{
    [SerializeField] private List<ColorEngine> _engines = new();
    [SerializeField] private WallBreaker _wallBreaker;
    [SerializeField] private Vector2 _force;
    private bool _isShoted = false;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _wallBreaker._IsActive = false;
        for(int i = 0; i < _engines.Count; i++)
        {
            _engines[i].SetLinePos(transform.position);
        }
    }

    void Update()
    {
        if(CheckEnginesAllActive() && !_isShoted)
        {
            _isShoted = true;
            _wallBreaker._IsActive = true;
            _wallBreaker.AddForce(_force);
        }
    }

    private bool CheckEnginesAllActive()
    {
        for(int i = 0; i < _engines.Count; i++)
        {
            if (!_engines[i]._isActive)
            {
                return false;
            }
        }
        return true;
    }
}
