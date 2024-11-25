using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CommandActivator : MonoBehaviour
{
    [SerializeField] private SoCommand command;
    [SerializeField] private String arguments;

    public SoCommand Command
    {
        get => command;
        set => command = value;
    }

    public string Arguments
    {
        get => arguments;
        set => arguments = value;
    }
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if(command != null)
        {
            string[] args = arguments.Split(' ');
            command.Execute(args);
        }
        else
            Debug.LogError($"{name}: {nameof(command)} is null!", gameObject);
    }
}