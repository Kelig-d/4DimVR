using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLightOnContact : MonoBehaviour
{

    public Light light;

    private void OnTriggerEnter(Collider other) {this.light.transform.localScale = Vector3.zero;}
}
