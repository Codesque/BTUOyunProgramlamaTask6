using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    [SerializeField] private int SensitivityX = 1;
    [SerializeField] private int SensitivityY = 1;

    [SerializeField] private int MinX = 0; 
    [SerializeField] private int MinY = 0;
    [SerializeField] private int MaxX = 300;
    [SerializeField] private int MaxY = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localRotation = Quaternion.Euler(
            
            Mathf.Clamp(transform.localRotation.eulerAngles.x - Input.GetAxisRaw("Mouse Y") * SensitivityY * Time.deltaTime , MinY , MaxY )   , 
            Mathf.Clamp(transform.localRotation.eulerAngles.y + Input.GetAxisRaw("Mouse X") * SensitivityX * Time.deltaTime , MinX , MaxX )   , 
            transform.localRotation.eulerAngles.z
            );
        
    }
}
