using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] DataPlayer _dataPlayer;
    private void Start()
    {
        StartCoroutine(ReduceLifeCoroutine());
    }
    private IEnumerator  ReduceLifeCoroutine()
    {
        while(_dataPlayer.CurrentLife>0)
        {
            yield return new WaitForSeconds(2);
            _dataPlayer.CurrentLife -= 10;
        }
        
    }
}
