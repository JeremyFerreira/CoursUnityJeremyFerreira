using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _scriptableEvent;
    [SerializeField] UnityEvent _callBackEvent;
    // Start is called before the first frame update
    void Awake()
    {
        _scriptableEvent.Event += CallBack;
    }
    private void CallBack()
    {
        Debug.Log("callback");
    }
}
