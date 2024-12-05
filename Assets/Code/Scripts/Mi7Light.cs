using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Controls the behavior of light sources and a shadow mask in a 3D environment. 
/// The lights move in 3D space based on the mask's position and their intensity is updated based on their distance from the mask.
/// </summary>
public class Mi7Light : MonoBehaviour
{
    /// <summary>
    /// Prefab to be destroyed when all lights are turned off.
    /// </summary>
    public GameObject Prefab;

    /// <summary>
    /// Smoothness factor for the mask's transformation.
    /// </summary>
    public float smooth;

    /// <summary>
    /// The mask that affects the lights' movement and intensity.
    /// </summary>
    public GameObject Masque;

    /// <summary>
    /// The scale of the mask.
    /// </summary>
    public Vector3 MasqueScale;

    /// <summary>
    /// The rate at which the mask expands or shrinks.
    /// </summary>
    public float DepMasque, DepLight;

    /// <summary>
    /// Set of light sources that are affected by the mask.
    /// </summary>
    public Light lightTop;
    public Light lightBottom;
    public Light lightFront;
    public Light lightBack;
    public Light lightLeft;
    public Light lightRight;
    
    /// <summary>
    /// Internal class representing a light source and its behavior within the mask's influence.
    /// </summary>
    class LightBuble
    {
        public Light Light;  // Light source reference
        public Vector3 Position;  // Position of the light
        readonly float dep;  // The speed at which the light moves
        readonly float smooth;  // Smoothness of the light's movement
        readonly int x, y, z;  // Direction factors for movement
        readonly float velocity;  // Speed factor for movement

        /// <summary>
        /// Initializes a new light source with given properties.
        /// </summary>
        /// <param name="l">The light to control.</param>
        /// <param name="DepMasque">The speed of the light's movement.</param>
        /// <param name="velocity">The velocity of the light.</param>
        /// <param name="smooth">The smoothness of the light's movement.</param>
        /// <param name="x">Movement factor along the x-axis.</param>
        /// <param name="y">Movement factor along the y-axis.</param>
        /// <param name="z">Movement factor along the z-axis.</param>
        public LightBuble(Light l, float DepMasque, float velocity, float smooth = 0.5f, int x = 0, int y = 0, int z = 0)
        {
            this.x = x; this.y = y; this.z = z;
            Position = l.transform.position;
            dep = DepMasque;
            Light = l;
            this.smooth = smooth;
            this.velocity = velocity;
        }

        /// <summary>
        /// Moves the light if it is within the movement range.
        /// </summary>
        /// <param name="Masque">The shadow mask to check distance with.</param>
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

        /// <summary>
        /// Updates the light intensity based on its distance from the mask.
        /// </summary>
        /// <param name="positionSombre">The position of the mask.</param>
        /// <param name="ScaleItem">The scale of the mask affecting the light.</param>
        public void updateIntensity(Vector3 positionSombre, double ScaleItem)
        {
            if (CheckLight())
            {
                double dist0 = distance(positionSombre, this.Light.transform.position);
                float teta = (float)((100 * ScaleItem) / dist0);
                this.Light.intensity = 5 - teta / 20;
            }
        }

        /// <summary>
        /// Calculates the distance between two points in 3D space.
        /// </summary>
        /// <param name="objet1">The first object position.</param>
        /// <param name="objet2">The second object position.</param>
        /// <returns>The distance between the two points.</returns>
        private double distance(Vector3 objet1, Vector3 objet2)
        {
            return Math.Sqrt(Math.Pow(objet2.x - objet1.x, 2) + Math.Pow(objet2.y - objet1.y, 2) + Math.Pow(objet2.z - objet1.z, 2));
        }

        /// <summary>
        /// Checks if the light's position is valid for movement.
        /// </summary>
        /// <returns>True if the light can move, false otherwise.</returns>
        private bool CheckLightDep()
        {
            return this.Light.transform.localScale != Vector3.zero && CheckLight();
        }

        /// <summary>
        /// Checks if the light is active (i.e., if its intensity is not zero).
        /// </summary>
        /// <returns>True if the light is active, false otherwise.</returns>
        public bool CheckLight()
        {
            return this.Light.intensity != 0;
        }

        /// <summary>
        /// Checks if the light is inside the shadow mask's area, and deactivates it if necessary.
        /// </summary>
        /// <param name="Masque">The shadow mask.</param>
        public void CheckArea(GameObject Masque)
        {
            if (this.CheckLight())
            {
                double dist2 = distance(Masque.transform.position, this.Light.transform.position);
                if ((Masque.transform.localScale.x / 2) + smooth >= dist2)
                {
                    this.Light.intensity = 0;
                }
            }
        }

        /// <summary>
        /// Updates the light's movement, area check, and intensity based on its proximity to the mask.
        /// </summary>
        /// <param name="Masque">The shadow mask affecting the light.</param>
        public void updateLight(GameObject Masque)
        {
            if (this.CheckLight())
            {
                this.deplacement(Masque);
                this.CheckArea(Masque);
                this.updateIntensity(Masque.transform.position, Masque.transform.localScale.x / 2);
            }
        }
    }

    // Instantiate each light bubble
    LightBuble LightTop;
    LightBuble LightBottom;
    LightBuble LightFront;
    LightBuble LightBack;
    LightBuble LightLeft;
    LightBuble LightRight;

    float impactSpeed;

    /// <summary>
    /// Initializes the light bubbles and mask scale.
    /// </summary>
    public void Start()
    {
        LightFront = new LightBuble(lightFront, DepLight, impactSpeed, z: -1);
        LightBack = new LightBuble(lightBack, DepLight, impactSpeed, z: 1);
        LightTop = new LightBuble(lightTop, DepLight, impactSpeed, y: 1);
        LightBottom = new LightBuble(lightBottom, DepLight, impactSpeed, y: -1);
        LightLeft = new LightBuble(lightLeft, DepLight, impactSpeed, x: -1);
        LightRight = new LightBuble(lightRight, DepLight, impactSpeed, x: 1);

        MasqueScale = Vector3.zero;
    }

    /// <summary>
    /// Updates the speed of light movement based on external input.
    /// </summary>
    /// <param name="impactSpeed">The new speed value for light movement.</param>
    public void updateVitesse(float impactSpeed)
    {
        this.impactSpeed = impactSpeed * 2;
    }

    /// <summary>
    /// Updates the mask scale and adjusts the lights based on the mask's position and size.
    /// </summary>
    private void Update()
    {
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

        // Check if all lights have been turned off and destroy the prefab if so
        if (!LightTop.CheckLight() &&
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
