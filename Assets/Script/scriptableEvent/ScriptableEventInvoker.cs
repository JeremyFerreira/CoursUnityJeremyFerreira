using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEventInvoker : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _triggerEvent;
    private void Start() => _triggerEvent.Invoke();
}
