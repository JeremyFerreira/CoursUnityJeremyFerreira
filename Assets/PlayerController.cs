using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _forceAcceleration;

    [SerializeField] float _speedMax;
    [SerializeField] Camera cam;
    [SerializeField] Canvas UI;
    MyInputs _input;
    Vector2 _moveInput;
    [SerializeField]  bool inputPC;

    private void Awake()
    {
        
        _input = new MyInputs();
        Play();
    }
    private void OnEnable()
    {
        _input.Game.Enable();
    }
    private void OnDisable()
    {
        _input.Game.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _input.Game.Pause.performed += ctx => Pause();
        _input.Game.Pause.canceled -= ctx => Pause();
        _input.UI.Play.performed += ctx => Play();
        _input.UI.Play.canceled -= ctx => Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        _moveInput = _input.Game.Move.ReadValue<Vector2>();
        Debug.Log(_moveInput);
        Movement();
    }
   

    public void Movement()
    {
        transform.Translate(_moveInput * _speedMax * Time.deltaTime);
    }

    

    public void Pause()
    {
        Debug.Log("pause");
        _input.Game.Disable();
        _input.UI.Enable();
        UI.gameObject.SetActive(true);
    }
    public void Play()
    {
        _input.UI.Disable();
        _input.Game.Enable();
        UI.gameObject.SetActive(false);
    }
}
