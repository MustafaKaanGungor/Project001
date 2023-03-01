using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunControl : MonoBehaviour
{
    public Vector2 PointerPosition;

    public GameObject gun;

    public float rotLim = 50f;


     void Awake() {
	
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() 
    {
        LimitRot();

        Quaternion targetRot = Quaternion.LookRotation(PointerPosition - transform.localRotation);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, 10f * Time.deltaTime);
        
        //transform.right = (PointerPosition-(Vector2)transform.position).normalized;
    
    }

    private void LimitRot()
    {
        Vector3 playerEulerAngles = transform.localRotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -rotLim, rotLim);

        Debug.Log(playerEulerAngles.z);

        transform.localRotation = Quaternion.Euler(playerEulerAngles);
    }
    

    private void FixedUpdate() {

    }
}
