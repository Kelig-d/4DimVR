using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoutteEau : MonoBehaviour
{
    public GameObject GoutteDeau;
    //public GameObject SpownPoint;

    private float currentTime = 5f;
    private bool tomber = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropRoutine());
    }

    IEnumerator DropRoutine()
    {
        while (true)
        {
            tombe();
            yield return new WaitForSeconds(currentTime);
        }
    }

    public void tombe()
    {
        Instantiate(GoutteDeau, transform.position, Quaternion.identity);
        //Goutte.transform.position = SpownPoint.transform.position;
        //Goutte.GetComponent<Rigidbody>().useGravity = true;
    }
}
