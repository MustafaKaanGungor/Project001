using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
 
    public Vector2 PointerPosition;

    public GameObject gun;

    
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float weaponRange = 10f;

     void Awake() {
	
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        
    }

    private void FixedUpdate() {

    }
    void shoot()
    {
        if( Input.GetMouseButton(0))
        {
            var hit = Physics2D.Raycast(gunPoint.position, transform.up, weaponRange);

            var trail = Instantiate(bullet, gunPoint.position, transform.rotation);

            var trailScript = trail.GetComponent<bulletTrailer>();

            if(hit.collider != null)
            {
                trailScript.setTargetPos(hit.point);
            }
            else
            {
                var endPosition = gunPoint.position + transform.up * weaponRange;
                trailScript.setTargetPos(endPosition);
            }
        }
    }
}
