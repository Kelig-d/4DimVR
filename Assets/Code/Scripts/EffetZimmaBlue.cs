using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;

/// <summary>
/// Handles special visual and physical effects for an object influenced by the ZimmaBlue artifact.
/// </summary>
public class EffetZimmaBlue : MonoBehaviour
{
    /// <summary>
    /// The material of the object, used to manipulate its shader and color.
    /// </summary>
    [SerializeField]
    private Material myMaterial;

    /// <summary>
    /// The rigidbody of the object, used for physical interactions.
    /// </summary>
    private Rigidbody myRigidbody;

    /// <summary>
    /// The collider of the object.
    /// </summary>
    private BoxCollider myObject;

    /// <summary>
    /// Minimum height target for downward movement.
    /// </summary>
    public float targetMin = 0.5f;

    /// <summary>
    /// Maximum height target for upward movement.
    /// </summary>
    public float targetHeight = 5f;

    /// <summary>
    /// Speed at which the object moves vertically.
    /// </summary>
    public float moveSpeed = 1f;

    /// <summary>
    /// The original scale of the object, used to reset size.
    /// </summary>
    private Vector3 originalScale;

    /// <summary>
    /// Timer to track the duration of the current color state.
    /// </summary>
    private float timeColor;

    // Predefined colors for specific effects
    private Color colorBlue = new Color(0.082f, 0.004f, 1.0f, 1.0f);
    private Color colorYellow = new Color(0.996f, 1.0f, 0.004f, 1.0f);
    private Color colorGreen = new Color(0.133f, 1.0f, 0.004f, 1.0f);
    private Color colorPurple = new Color(0.761f, 0.004f, 1.0f, 1.0f);
    private Color colorRed = new Color(1.0f, 0.004f, 0.004f, 1.0f);
    private Color colorWhite = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    /// <summary>
    /// Initializes the object's material, rigidbody, and other components.
    /// </summary>
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_ShaderX", 0.0f);
        myObject = GetComponent<BoxCollider>();
        Renderer renderer = GetComponent<Renderer>();
        originalScale = transform.localScale;
    }

    /// <summary>
    /// Called every frame to apply color effects and reset properties over time.
    /// </summary>
    void Update()
    {
        EffetColor();
        RetourNormalColor();
    }

    /// <summary>
    /// Changes the object's color based on input and adjusts its state.
    /// </summary>
    /// <param name="newcolor">The new color to apply to the object.</param>
    public void ChangeColor(Color newcolor)
    {
        if (myMaterial.GetFloat("_ShaderX") == 0.0f)
        {
            myMaterial.color = newcolor;
            myMaterial.SetFloat("_ShaderX", 0.25f);
            timeColor = 5f;
            StartCoroutine(timerColor());
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.25f && myMaterial.color == newcolor)
        {
            myMaterial.SetFloat("_ShaderX", 0.5f);
            timeColor += 5f;
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.50f && myMaterial.color == newcolor)
        {
            myMaterial.SetFloat("_ShaderX", 0.75f);
            timeColor += 35f;
        }
    }

    /// <summary>
    /// Applies visual and physical effects based on the object's color and state.
    /// </summary>
    public void EffetColor()
    {
        if (myMaterial.GetFloat("_ShaderX") == 0.75f)
        {
            if (myMaterial.color.ToString() == colorBlue.ToString())
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetMin, step), transform.position.z);
            }
            else if (myMaterial.color.ToString() == colorYellow.ToString() && transform.position.y < targetHeight)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetHeight, step), transform.position.z);
            }
            else if (myMaterial.color.ToString() == colorPurple.ToString())
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale / 2f, Time.deltaTime * 2f);
            }
            else if (myMaterial.color.ToString() == colorWhite.ToString())
            {
                GetComponent<Renderer>().enabled = false;
            }
        }
    }

    /// <summary>
    /// Gradually resets the object's state and color over time.
    /// </summary>
    public void RetourNormalColor()
    {
        if (timeColor == 30.0f)
        {
            GetComponent<Renderer>().enabled = true;
            myMaterial.SetFloat("_ShaderX", 0.5f);
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 15.0f)
        {
            myMaterial.SetFloat("_ShaderX", 0.25f);
        }
        else if (myMaterial.GetFloat("_ShaderX") == 0.25f && timeColor == 0.0f)
        {
            myMaterial.SetFloat("_ShaderX", 0.0f);
            ResetSize();
        }
    }

    /// <summary>
    /// Resets the object's size to its original scale.
    /// </summary>
    private void ResetSize()
    {
        transform.localScale = originalScale;
    }

    /// <summary>
    /// Timer coroutine that decrements the color timer over time.
    /// </summary>
    IEnumerator timerColor()
    {
        while (timeColor >= 0)
        {
            timeColor--;
            Debug.Log("TIMER" + timeColor);
            yield return new WaitForSeconds(1f);
        }
    }
}
