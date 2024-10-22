using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateSystem : MonoBehaviour
{
    public GameObject objectToRotate;
    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    Quaternion targetRotation;

    private void Update()
    {
        CheckRotation();
    }

    void CheckRotation()
    {
        
         objectToRotate.transform.Rotate(rotationX, rotationY, rotationZ);

        /*
        targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x+0.1F, objectToRotate.transform.eulerAngles.y+.1F, objectToRotate.transform.eulerAngles.z);
        objectToRotate.transform.rotation = targetRotation;*/
    }
}
