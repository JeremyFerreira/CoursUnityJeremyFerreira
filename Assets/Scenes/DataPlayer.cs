using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Player")]
public class DataPlayer : ScriptableObject
{
    public event Action LifePointsChanged;

    [SerializeField] string _name;
    public string Name => _name;

    [SerializeField] int _life;
    public int Life => _life;
    private int _currentLifePoints;
    public int CurrentLife
    {
        get => _currentLifePoints;
        set 
        {
            _currentLifePoints = Mathf.Clamp(value,0,_life);
            LifePointsChanged?.Invoke();
        }
    }
    private void OnEnable()
    {
        _currentLifePoints = Life;
    }
}
