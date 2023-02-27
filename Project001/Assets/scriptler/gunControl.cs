using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunControl : MonoBehaviour
{
    public Vector2 PointerPosition;

    public GameObject gun;


     void Awake() {
	
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() 
    {
        if(PointerPosition.x > gun.transform.position.x && PointerPosition.y < gun.transform.position.y)
        transform.right = (PointerPosition-(Vector2)transform.position).normalized;
    }

    private void FixedUpdate() {

    }
}
