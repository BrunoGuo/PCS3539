// https://www.youtube.com/watch?v=_QajrabyTJc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public float movementSpeed = 10f;

    public CharacterController controller;


    public void keyboardController() {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;
        controller.Move(move * movementSpeed * Time.deltaTime);
    }

    void Start() {
    
    }

    void Update() {
        keyboardController();
    }
}
