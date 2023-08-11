using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningX : MonoBehaviour
{
    public float spinningSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    float y = transform.rotation.eulerAngles.z;
    transform.rotation = Quaternion.Euler(0f, 0f, y + spinningSpeed * Time.deltaTime);
}
}
