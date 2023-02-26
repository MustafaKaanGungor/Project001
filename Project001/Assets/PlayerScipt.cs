using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScipt : MonoBehaviour
{

    Vector2 movementInput;

    [SerializeField]
    private InputActionReference VerticalControls;

    void Awake()
    {
		playerRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        engine = false;

        rotationAngle = new Vector3(0, 0, 1);
    }
    
    void Update() 
    {
        movementInput = VerticalControls.action.ReadValue<Vector2>(); 
    }
}
