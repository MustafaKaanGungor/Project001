using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerObject;

    public GameObject gun;

    public Rigidbody2D playerRB;
    
    public bool engine;

    public float liftingEnginePower;

    public float verticalEnginePower;

    public float horizontalEnginePower;

    private Vector2 moveInput;

    private Vector2 pointerInput;

    private Vector3 rotationAngle;

    [SerializeField] private InputActionReference wasdControls;

    [SerializeField] private InputActionReference pointerPosition;

    private gunControl weaponParent;

    void Awake()
    {
		playerRB = GetComponent<Rigidbody2D>();

        weaponParent = GetComponentInChildren<gunControl>();
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

        pointerInput = GetPointerInput();

        weaponParent.PointerPosition = pointerInput;

    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
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
        moveInput = wasdControls.action.ReadValue<Vector2>(); 
    }

    public void engineController()
    {
        //TODO: Motor açıp kapamayı tek tuşa indirge
        //TODO: Motorun açılışına gecikme ekle https://www.youtube.com/watch?v=Qt5KPNmKglM
        //TODO: Motor açılırken pilotların check konuşmalarını ekle
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
        //TODO: Motor hasarına göre motor gücünü azaltma ekle
        if(engine == true)
        {
        playerRB.AddForce(Vector2.up * liftingEnginePower);
        }
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
