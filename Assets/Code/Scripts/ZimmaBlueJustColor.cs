using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;

public class ZimmaBlueJustColor : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private Rigidbody myRigidbody;
    private BoxCollider myObject;

    private float timeColor;

    private Color colorBlue = new Color(0.082f,0.004f,1.0f,1.0f);
    private Color colorYellow = new Color(0.996f,1.0f,0.004f,1.0f);
    private Color colorGreen = new Color(0.133f,1.0f,0.004f,1.0f);
    private Color colorPurple = new Color(0.761f,0.004f,1.0f,1.0f);
    private Color colorRed = new Color(1.0f,0.004f,0.004f,1.0f);
    private Color colorWhite = new Color(1.0f,1.0f,1.0f,1.0f);


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_ShaderX",0.0f);
        myObject = GetComponent<BoxCollider>();
        Renderer renderer = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        RetourNormalColor();
    }

    public void ChangeColor(Color newcolor){
        // si pas de couleur donnée par le joueur
        if(myMaterial.GetFloat("_ShaderX") == 0.0f){
            // object prend la couleur du laser 
            myMaterial.color = newcolor; 
            myMaterial.SetFloat("_ShaderX",0.25f);
            // commencement du timer 
            timeColor= 5f;
            StartCoroutine(timerColor());
        }
        // si objet même couleur que le laser et si il est remplis à 0.25
        else if (myMaterial.GetFloat("_ShaderX") == 0.25f && myMaterial.color == newcolor){
            myMaterial.SetFloat("_ShaderX",0.5f);
            // ajoute du temps supplementaire 
            timeColor += 5f ; 
        }
        // si objet même couleur que le laser et si il est remplis à 0.50
        else if (myMaterial.GetFloat("_ShaderX") == 0.50f && myMaterial.color == newcolor){
            myMaterial.SetFloat("_ShaderX",0.75f);
            // ajoute du temps supplementaire 
            timeColor += 35f ; 
        }
    }

    //La couleur de l'objet redevient à sa couleur d'orignie en fonction du temps qui passe
    public void RetourNormalColor(){
        if( timeColor == 30.0f ){
            myMaterial.SetFloat("_ShaderX",0.5f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 15.0f ){
            myMaterial.SetFloat("_ShaderX",0.25f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f && timeColor == 0.0f ){
            myMaterial.SetFloat("_ShaderX",0.0f);
        }
    }
  
    IEnumerator timerColor(){
        while(timeColor>= 0){
            timeColor --;
            Debug.Log("TIMER"+timeColor);
            yield return new WaitForSeconds(1f);
        }
    }

}
