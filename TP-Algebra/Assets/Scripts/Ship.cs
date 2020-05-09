using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Ship : MonoBehaviour
{
    Bullet bullet;
    public Vec3 speed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject bulletToInstantiate = Instantiate(bullet, transform.position, Quaternion.identity).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W)) {
            transform.position += (speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            transform.position += -(speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.A)){
            
        }
        if (Input.GetKeyDown(KeyCode.D)){
        
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate<GameObject>().
        }
    }
}
