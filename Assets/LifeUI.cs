using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class LifeUI : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] DataPlayer _dataPlayer;
    private void Awake()
    {
        _slider =  GetComponent<Slider>();
    }
    private void Start()
    {
        _dataPlayer.LifePointsChanged += OnLifePointsChanged;
        OnLifePointsChanged();
    }
    void OnLifePointsChanged()
    {
        _slider.value = (float)_dataPlayer.CurrentLife / _dataPlayer.Life;
    }
    
}
