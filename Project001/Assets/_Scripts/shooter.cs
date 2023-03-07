using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
 
    public Vector2 PointerPosition;

    public GameObject gun;

    private Vector3 dispersed;

    [SerializeField] private float rateOfFire = 0.2f;
    [SerializeField] private float lastShootTime;

    private float currentDispersion;
    [SerializeField] private float dispersionAmount;

    
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

        transform.up = (PointerPosition-(Vector2)transform.position).normalized;
        
    }

    private void FixedUpdate() {

    }
    void shoot()
    {
        if( Input.GetMouseButton(0))
        {
            dispersion();
            if(Time.time > rateOfFire + lastShootTime)
            {

                lastShootTime = Time.time;

                dispersed = new Vector3(0,currentDispersion,0);
            var hit = Physics2D.Raycast(gunPoint.position, transform.up + dispersed, weaponRange);

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

    void dispersion()
    {
        currentDispersion = ((Random.value - 0.5f) / 2) * dispersionAmount;
    }
}
