using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerObject;

    public Rigidbody2D playerRB;
    
    public bool engine;

    public float liftingEnginePower;

    public float verticalEnginePower;

    public float horizontalEnginePower;

    private Vector2 moveInput;

    private Vector3 rotationAngle;

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
        engineController();

        inputManager();

    }

    void FixedUpdate()
    {
        liftingEngineThrust();

        verticalControl();

        horizontalControl();

        horizontalMovement();
    }

    public void inputManager()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    public void engineController()
    {
        //TODO: Motor açıp kapamayı tek tuşa indirge
        if(Input.GetKey(KeyCode.I)) 
        {
            engine = true;
        }
        if(Input.GetKey(KeyCode.O))
        {
           engine = false;
        }
    }

    public void liftingEngineThrust()
    {
        if(engine == true)
        {
        playerRB.AddForce(Vector2.up * liftingEnginePower);
        }
    }

    public void verticalControl()
    {
        if(moveInput.y > 0)
        {
            playerRB.AddForce(Vector2.up * verticalEnginePower);
        }
        if(moveInput.y < 0)
        {
            playerRB.AddForce(Vector2.down * verticalEnginePower);
        }
    }

    public void horizontalControl()
    {
        if(moveInput.x > 0)
        {
            transform.Rotate(-rotationAngle, horizontalEnginePower * Time.deltaTime);
        }
        if(moveInput.x < 0)
        {
            transform.Rotate(rotationAngle, horizontalEnginePower * Time.deltaTime);
        }
    }

    public void horizontalMovement()
    {
        if(transform.eulerAngles.z > 180)
        {
            playerRB.AddForce(Vector2.left * (transform.eulerAngles.z - 360f) * 0.1f);
        }
        if(transform.eulerAngles.z < 180)
        {
            playerRB.AddForce(Vector2.left * transform.eulerAngles.z * 0.1f);
        }
    }
}
