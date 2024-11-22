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
        readonly float dep;
        readonly float smooth;
        readonly int x, y, z;

        readonly float velocity;

        public LightBuble(Light l, float DepMasque, float velocity, float smooth = 0.5f, int x = 0, int y = 0, int z = 0)
        {
            this.x = x; this.y = y; this.z = z;
            Position = l.transform.position;
            dep = DepMasque;
            Light = l;
            this.smooth = smooth;
            this.velocity = velocity;
        }

        public void deplacement(GameObject Masque)
        {
            double dist2 = distance(Masque.transform.position, this.Light.transform.position);

            if (this.CheckLightDep() && dist2 <= this.velocity)
            {
                this.Position.x += dep * x;
                this.Position.y += dep * y;
                this.Position.z += dep * z;
                this.Light.transform.position = Position;
            }
        }

       public void updateIntensity(Vector3 positionSombre, double ScaleItem )
        {
            if (CheckLight())
            {
                double dist0 = distance(positionSombre, this.Light.transform.position);
                float teta = (float)((100 * ScaleItem) / dist0);
                this.Light.intensity = 5- teta/20;
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

        public void updateLight(GameObject Masque)
        {
            if (this.CheckLight())
            {
                /* Déplacement des lumière */

                this.deplacement(Masque);

                /* Check if inside bububle */

                this.CheckArea(Masque);

                /* Update the intensity in function of distance */

                this.updateIntensity(Masque.transform.position, Masque.transform.localScale.x / 2);
            }
        }
    }



    LightBuble LightTop;
    LightBuble LightBottom;
    LightBuble LightFront;
    LightBuble LightBack;
    LightBuble LightLeft;
    LightBuble LightRight;

    float impactSpeed;


    public void Start()
    {
        LightFront = new LightBuble(lightFront, DepLight,impactSpeed, z: -1);
        LightBack = new LightBuble(lightBack, DepLight, impactSpeed, z: 1);
        LightTop = new LightBuble(lightTop, DepLight,impactSpeed, y: 1);
        LightBottom = new LightBuble(lightBottom, DepLight, impactSpeed, y: -1);
        LightLeft = new LightBuble(lightLeft, DepLight, impactSpeed, x: -1);
        LightRight = new LightBuble(lightRight, DepLight, impactSpeed, x: 1);

        MasqueScale = Vector3.zero;

    }

    public void updateVitesse(float impactSpeed)
    {
        this.impactSpeed = impactSpeed*2;
    }

    private void Update()
    {
        /* Grossisement de l'ombre */

        MasqueScale.x += DepMasque;
        MasqueScale.y += DepMasque;
        MasqueScale.z += DepMasque;

        Masque.transform.localScale = MasqueScale;

        LightTop.updateLight(Masque);
        LightBottom.updateLight(Masque);
        LightLeft.updateLight(Masque);
        LightRight.updateLight(Masque);
        LightFront.updateLight(Masque);
        LightBack.updateLight(Masque);

        /* check if detroy item */

        if( !LightTop.CheckLight() &&
            !LightBottom.CheckLight() &&
            !LightLeft.CheckLight() &&
            !LightRight.CheckLight() &&
            !LightFront.CheckLight() &&
            !LightBack.CheckLight())
        {
            Destroy(Prefab);
        }

    }




}
