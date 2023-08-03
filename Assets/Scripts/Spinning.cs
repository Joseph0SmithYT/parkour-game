using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float spinningSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    float y = transform.rotation.eulerAngles.y;
    transform.rotation = Quaternion.Euler(0f, y + spinningSpeed * Time.deltaTime, 0f);
}
}
