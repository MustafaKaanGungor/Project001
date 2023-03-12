using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform camTransform;
    public Camera mainCam;
    public Transform playerTransform;

    public float smoothing;

     public float size;

    void FixedUpdate()
    { 
        //TODO: kameraya minSizeını 5 yap
        size = 5 + playerTransform.position.y;
        mainCam.orthographicSize = size;

        Vector3 targetPos = new Vector3(playerTransform.position.x, camTransform.position.y, camTransform.position.z);

        if (camTransform.position.x != playerTransform.position.x)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, targetPos , smoothing);
        } 
    }
}
