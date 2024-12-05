using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;

/// <summary>
/// This class manages the color changes and timer behavior for a GameObject when it interacts with a projectile (e.g., a laser).
/// The object can be colored and a timer is started to revert its color back after a certain time.
/// </summary>
public class ZimmaBlueJustColor : MonoBehaviour
{
    /// <summary>
    /// The material of the object that will be changed to different colors.
    /// </summary>
    [SerializeField]
    private Material myMaterial;

    /// <summary>
    /// The Rigidbody component of the object.
    /// </summary>
    private Rigidbody myRigidbody;

    /// <summary>
    /// The BoxCollider component of the object.
    /// </summary>
    private BoxCollider myObject;

    /// <summary>
    /// The timer value to track how long the color should remain changed.
    /// </summary>
    private float timeColor;

    /// <summary>
    /// The color blue used for the laser interaction.
    /// </summary>
    private Color colorBlue = new Color(0.082f, 0.004f, 1.0f, 1.0f);

    /// <summary>
    /// The color yellow used for the laser interaction.
    /// </summary>
    private Color colorYellow = new Color(0.996f, 1.0f, 0.004f, 1.0f);

    /// <summary>
    /// The color green used for the laser interaction.
    /// </summary>
    private Color colorGreen = new Color(0.133f, 1.0f, 0.004f, 1.0f);

    /// <summary>
    /// The color purple used for the laser interaction.
    /// </summary>
    private Color colorPurple = new Color(0.761f, 0.004f, 1.0f, 1.0f);

    /// <summary>
    /// The color red used for the laser interaction.
    /// </summary>
    private Color colorRed = new Color(1.0f, 0.004f, 0.004f, 1.0f);

    /// <summary>
    /// The color white used for the laser interaction.
    /// </summary>
    private Color colorWhite = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    // Start is called before the first frame update
    /// <summary>
    /// Initializes the Rigidbody, material, and collider components.
    /// Also sets the initial shader property for the object.
    /// </summary>
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_ShaderX", 0.0f);  // Initial shader state
        myObject = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the color of the object and manages the return to the original color over time.
    /// </summary>
    void Update()
    {
        RetourNormalColor();
    }

    /// <summary>
    /// Changes the color of the object based on the color provided by the laser.
    /// The object's shader state and timer are updated accordingly.
    /// </summary>
    /// <param name="newcolor">The new color to change the object to.</param>
    public void ChangeColor(Color newcolor)
    {
        // If no color has been assigned yet (ShaderX is 0.0f)
        if (myMaterial.GetFloat("_ShaderX") == 0.0f)
        {
            // Change the object's color to the color of the laser
            myMaterial.color = newcolor;
            myMaterial.SetFloat("_ShaderX", 0.25f);  // Set shader state to 0.25 (indicating color is applied)
            timeColor = 5f;  // Start the timer at 5 seconds
            StartCoroutine(timerColor());  // Start the countdown timer
        }
        // If the object is already the same color as the laser, increase the shader state
        else if (myMaterial.GetFloat("_ShaderX") == 0.25f && myMaterial.color == newcolor)
        {
            myMaterial.SetFloat("_ShaderX", 0.5f);
            timeColor += 5f;  // Add 5 more seconds to the timer
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.50f && myMaterial.color == newcolor)
        {
            myMaterial.SetFloat("_ShaderX", 0.75f);
            timeColor += 35f;  // Add 35 more seconds to the timer
        }
    }

    /// <summary>
    /// This method gradually returns the object to its original color based on the timer.
    /// </summary>
    public void RetourNormalColor()
    {
        if (timeColor == 30.0f)
        {
            myMaterial.SetFloat("_ShaderX", 0.5f);
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 15.0f)
        {
            myMaterial.SetFloat("_ShaderX", 0.25f);
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.25f && timeColor == 0.0f)
        {
            myMaterial.SetFloat("_ShaderX", 0.0f);  // Return to original color (ShaderX 0.0f)
        }
    }

    /// <summary>
    /// Coroutine that manages the countdown for the color timer.
    /// </summary>
    /// <returns>Enumerator that yields until the timer reaches zero.</returns>
    IEnumerator timerColor()
    {
        while (timeColor >= 0)
        {
            timeColor--;  // Decrease time
            Debug.Log("TIMER: " + timeColor);
            yield return new WaitForSeconds(1f);  // Wait for one second before decreasing again
        }
    }
}
