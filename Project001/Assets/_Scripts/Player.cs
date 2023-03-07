using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    private shooter shooterScript;


    [Header("GameObjects")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject landingGear;


    [Header("Components")]
    public Rigidbody2D playerRB;


    [Header("Engine Parameters")] 
    public bool engine;
    public float liftingEnginePower;
    public float verticalEnginePower;
    public float horizontalEnginePower;


    [Header("Input")]
    [SerializeField] private InputActionReference wasdControls;
    [SerializeField] private InputActionReference pointerPosition;
    private Vector2 moveInput;
    private Vector2 pointerInput;

    private Vector3 rotationAngle;

    void Awake()
    {
		playerRB = GetComponent<Rigidbody2D>();

        shooterScript = GetComponentInChildren<shooter>();
    }

    void Start()
    {
        engine = false;

        rotationAngle = new Vector3(0, 0, 1);
    }

    void Update()
    {
        engineController();

        gearController();

        inputManager();
    }

    void FixedUpdate()
    {
        if(engine == true)
        {
            liftingEngineThrust();

            verticalControl();

            horizontalControl();

            horizontalMovement();
        }
    }

    public void inputManager()
    {
        moveInput = wasdControls.action.ReadValue<Vector2>(); 

        pointerInput = GetPointerInput();

        shooterScript.PointerPosition = pointerInput;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void engineController()
    {
        //TODO: Motorun açılışına gecikme ekle https://www.youtube.com/watch?v=Qt5KPNmKglM
        //TODO: Motor açılırken pilotların check konuşmalarını ekle
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            engine = !engine;
        }
    }

    public void gearController()
    {
        //TODO: gearın açılışına gecikme ekle https://www.youtube.com/watch?v=Qt5KPNmKglM
         if(Input.GetKeyDown(KeyCode.K)) 
        {
            landingGear.SetActive(!landingGear.activeSelf);
        }
    }

    public void liftingEngineThrust()
    {
        //TODO: Motor hasarına göre motor gücünü azaltma ekle
        playerRB.AddForce(Vector2.up * liftingEnginePower);
    }

    public void verticalControl()
    {
        //TODO: Motor hasarına göre motor gücünü azaltma ekle
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
            playerRB.AddForce(Vector2.up * (transform.eulerAngles.z - 360f) * 0.01f);
            transform.Rotate(rotationAngle, 1f * Time.deltaTime);
        }
        if(transform.eulerAngles.z < 180)
        {
            playerRB.AddForce(Vector2.left * transform.eulerAngles.z * 0.1f);
            playerRB.AddForce(Vector2.up * transform.eulerAngles.z * 0.01f);
            transform.Rotate(-rotationAngle, 1f * Time.deltaTime);
        }
    }
}
