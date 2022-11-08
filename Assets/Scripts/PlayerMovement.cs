using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private InputReader inputReader = default;
    private Vector2 previousMovementInput;


    private void OnEnable()
    {
        inputReader.moveEvent += Move;
        inputReader.fireEvent += Fire;

    }
    void Update()
    {
        this.transform.Translate(previousMovementInput * Time.deltaTime);
    }

    void Move(Vector2 movement)
    {
        previousMovementInput = movement;
    }



    void Fire()
    {
        this.Log("Firing!");
    }
}
