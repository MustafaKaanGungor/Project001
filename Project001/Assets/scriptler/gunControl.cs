using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunControl : MonoBehaviour
{
    public Vector2 PointerPosition;

     void Awake() {
	
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = (PointerPosition-(Vector2)transform.position).normalized;
    }

    private void FixedUpdate() {

    }
}
