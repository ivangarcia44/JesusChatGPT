using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MainInputMap mainInputMap;

    private void Awake() {
        mainInputMap = new MainInputMap();
    }

    private void OnEnable() {
        mainInputMap.Enable();
    }

    private void OnDisable() {
        mainInputMap.Disable();
    }
}
