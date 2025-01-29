using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoutteEau : MonoBehaviour
{
    public GameObject GoutteDeau;
    public GameObject SpownPoint;

    private float currentTime = 0;
    private bool tomber = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tomber == false && currentTime == 0f)
        {
            currentTime = 60f;
            tombe();
        }
    }

    public void tombe()
    {
        tomber = true;
        GameObject Goutte = Instantiate(GoutteDeau);
        Goutte.transform.position = SpownPoint.transform.position;
        do
        {
            currentTime -= Time.deltaTime;
        } while (currentTime > 45f);
        Goutte.GetComponent<Rigidbody>().useGravity = true;
        Destroy(Goutte, 5f);
       
    }
}
