using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Mi7Light : MonoBehaviour
{
    /// <summary>
    /// Input data 
    /// </summary>

    // Effet sombre
    public GameObject Masque;
    public Vector3 MasqueScale;
    public float DepMasque , DepLight;

    // Set of light
   

    public Light lightTop;
    public Light lightBottom;
    public Light lightFront;
    public Light lightBack;
    public Light lightLeft;
    public Light lightRight;

    /// <summary>
    /// Class pour les spots
    /// </summary>

    class LightBuble
    {

        public Light Light;
        public Vector3 Position;
        float dep;
        int x,y,z;

        public LightBuble(Light l, float DepMasque, int x = 0, int y = 0, int z = 0)
        {
            this.x = x; this.y = y; this.z = z;
            Position = l.transform.position;
            dep = DepMasque;
            Light = l;
        }

        public void deplacement()
        {
            this.Position.x += dep * x;
            this.Position.y += dep * y;
            this.Position.z += dep * z;
            this.Light.transform.position = Position;

        }
        public void check()
        {

        }


    }



    LightBuble LightTop;
    LightBuble LightBottom;
    LightBuble LightFront;
    LightBuble LightBack;
    LightBuble LightLeft;
    LightBuble LightRight;


    public void Start()
    {
        LightFront = new LightBuble(lightFront, DepLight, z:-1) ;
        LightBack  = new LightBuble(lightBack, DepLight, z: 1);
        LightTop = new LightBuble(lightTop, DepLight , y:1);
        LightBottom = new LightBuble(lightBottom, DepLight, y:-1);
        LightLeft = new LightBuble(lightLeft, DepLight, x:-1);
        LightRight = new LightBuble(lightRight, DepLight,x:1);

        MasqueScale = Vector3.zero;
        CheckArea();
    }


    private void Update()
    {
        /* Grossisement de l'ombre
        MasqueScale.x += DepMasque;
        MasqueScale.y += DepMasque;
        MasqueScale.z += DepMasque;

        Masque.transform.localScale = MasqueScale;
        */

        /* Déplacement des lumière
        LightTop.deplacement();
        LightBottom.deplacement();
        LightLeft.deplacement();
        LightRight.deplacement();
        LightFront.deplacement();
        LightBack.deplacement();
        */


        LightTop.check();

    }

    private bool CheckArea()
    {
        Vector3 t = Masque.transform.position;
        Matrix4x4 tt = Masque.transform.localToWorldMatrix;

        print("Position :" + t);
        print("Matrice : " + tt);

        return false;


    }
}
