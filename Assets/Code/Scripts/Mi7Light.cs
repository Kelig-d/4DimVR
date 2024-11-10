using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;





public class Mi7Light : MonoBehaviour
{
    /// <summary>
    /// Input data 
    /// </summary>

    public GameObject Prefab;
    public float smooth;


    // Effet sombre
    public GameObject Masque;
    public Vector3 MasqueScale;
    public float DepMasque, DepLight;

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
        float smooth;
        int x, y, z;
        bool move;

        public LightBuble(Light l, float DepMasque, float smooth = 0.5f, int x = 0, int y = 0, int z = 0)
        {
            this.x = x; this.y = y; this.z = z;
            Position = l.transform.position;
            dep = DepMasque;
            Light = l;
            move = true;
            this.smooth = smooth;
        }

        public void deplacement()
        {
            if (this.CheckLightDep())
            {
                this.Position.x += dep * x;
                this.Position.y += dep * y;
                this.Position.z += dep * z;
                this.Light.transform.position = Position;
            }


        }

       

        private double distance(Vector3 objet1, Vector3 objet2)
        {
            return Math.Sqrt(Math.Pow(objet2.x - objet1.x, 2) + Math.Pow(objet2.y - objet1.y, 2) + Math.Pow(objet2.z - objet1.z, 2));
        }

        private bool CheckLightDep()
        {
            
            return this.Light.transform.localScale != Vector3.zero && CheckLight();
        }

        public bool CheckLight()
        {

            return this.Light.intensity != 0;
        }

        public void CheckArea(GameObject Masque)
        {
            if (this.CheckLight())
            {
                //double dist = distance(Vector3.zero, Masque.transform.localScale);// Math.Sqrt(3.0f) * .x;
                double dist2 = distance(Masque.transform.position,this.Light.transform.position);
                if (( Masque.transform.localScale.x /2 ) + smooth >= dist2)
                {
                    this.Light.intensity = 0;

                }
            }

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
        LightFront = new LightBuble(lightFront, DepLight, z: -1);
        LightBack = new LightBuble(lightBack, DepLight, z: 1);
        LightTop = new LightBuble(lightTop, DepLight, y: 1);
        LightBottom = new LightBuble(lightBottom, DepLight, y: -1);
        LightLeft = new LightBuble(lightLeft, DepLight, x: -1);
        LightRight = new LightBuble(lightRight, DepLight, x: 1);

        MasqueScale = Vector3.zero;
        print("Masque : " + Masque.transform.position);

    }


    private void Update()
    {
        /* Grossisement de l'ombre */

        MasqueScale.x += DepMasque;
        MasqueScale.y += DepMasque;
        MasqueScale.z += DepMasque;

        Masque.transform.localScale = MasqueScale;

        /* Déplacement des lumière */
        LightTop.deplacement();
        LightBottom.deplacement();
        LightLeft.deplacement();
        LightRight.deplacement();
        LightFront.deplacement();
        LightBack.deplacement();


        /* Check if inside bububle */

        LightTop.CheckArea(Masque);
        LightBottom.CheckArea(Masque);
        LightLeft.CheckArea(Masque);
        LightRight.CheckArea(Masque);
        LightFront.CheckArea(Masque);
        LightBack.CheckArea(Masque);

        /* check if detroy item */

        if( !LightTop.CheckLight() &&
            !LightBottom.CheckLight() &&
            !LightLeft.CheckLight() &&
            !LightRight.CheckLight() &&
            !LightFront.CheckLight() &&
            !LightBack.CheckLight())
        {
            Destroy(Prefab);
            print("destroy");
        }




    }




}
