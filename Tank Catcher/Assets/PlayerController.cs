using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 streetBounds = new Vector2(-10, 10);
    public float turnSpeed = 1;
    public float turnAngle = 30;
    public float accelSpeed = 0.1f;
    public float accelDist = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float turn = input.x * turnSpeed;
        if (!(input.x > 0 && transform.position.x < streetBounds.y) && !(input.x < 0 && transform.position.x > streetBounds.x))
            turn = 0;
        transform.rotation = Quaternion.Euler(0, turn * turnAngle, 0);
        transform.position = new Vector3(transform.position.x + turn, 0, Mathf.Lerp(transform.position.z, input.y * accelDist, accelSpeed));
    }
}
