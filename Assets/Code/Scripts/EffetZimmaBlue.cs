using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;

public class EffetZimmaBlue : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private Rigidbody myRigidbody;
    private BoxCollider myObject;
    public float targetMin = 0.5f;
    public float targetHeight = 5f;  // Hauteur cible à laquelle le cube doit monter
    public float moveSpeed = 1f; 
    private Vector3 originalScale; // Nouvelle taille lorsqu'il est violet

    private float timeColor;
    public float pushForce = 50f; 

    private Color colorBlue = new Color(0.082f,0.004f,1.0f,1.0f);
    private Color colorYellow = new Color(0.996f,1.0f,0.004f,1.0f);
    private Color colorGreen = new Color(0.133f,1.0f,0.004f,1.0f);
    private Color colorPurple = new Color(0.761f,0.004f,1.0f,1.0f);
    private Color colorRed = new Color(1.0f,0.004f,0.004f,1.0f);
    private Color colorWhite = new Color(1.0f,1.0f,1.0f,1.0f);
    /*
    private InputActionReference targetDevice;   // Périphérique de la manette
    private bool isPushing = false;     // Indicateur si le joueur appuie sur la gâchette

    public InputActionReference pushButton;
    */

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_ShaderX",0.0f);
        myObject = GetComponent<BoxCollider>();
        Renderer renderer = GetComponent<Renderer>();
        originalScale = transform.localScale;
        //pushButton.action.started += ButtonWasPressed;
        //pushButton.action.canceled += ButtonWasReleased;
    }

    // Update is called once per frame
    void Update()
    {
        EffetColor();
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

    public void EffetColor(){
        if (myMaterial.GetFloat("_ShaderX") == 0.75f){
            if (myMaterial.color.ToString() == colorBlue.ToString()){
                // Déplace le cube progressivement vers le BAS en fonction de la vitesse
                float step = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetMin, step), transform.position.z);
        
            }else if(myMaterial.color.ToString() == colorYellow.ToString() && transform.position.y < targetHeight){
                // Déplace le cube progressivement vers le haut en fonction de la vitesse
                float step = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetHeight, step), transform.position.z);
                
            }else if(myMaterial.color.ToString() == colorPurple.ToString()){
                // Change la taille du cube
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale / 2f, Time.deltaTime * 2f);   
            }/*else if(myMaterial.color.ToString() == colorRed.ToString()){
                Debug.Log("rouge");
            }*/
            else if(myMaterial.color.ToString() == colorWhite.ToString()){
                GetComponent<Renderer>().enabled = false;  // Cache le cube
            }
        }
    }

    //La couleur de l'objet redevient à sa couleur d'orignie en fonction du temps qui passe
    public void RetourNormalColor(){
        if( timeColor == 30.0f ){
            GetComponent<Renderer>().enabled = true;
            //myObject.enabled=true;
            myMaterial.SetFloat("_ShaderX",0.5f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 15.0f ){
            myMaterial.SetFloat("_ShaderX",0.25f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f && timeColor == 0.0f ){
            myMaterial.SetFloat("_ShaderX",0.0f);
            ResetSize();
        }
    }

    /*public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Bouton pressé !");
        if (myMaterial.color.ToString() == colorGreen.ToString())
        {
            PushCube();
        }
    }

    public void ButtonWasReleased(InputAction.CallbackContext context)
    {
        myRigidbody.isKinematic = true;
        myRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
        myRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotation;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezePositionX;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezePositionZ;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezeRotationX;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezeRotationZ;
    }*/

    private void ResetSize()
    {
        // Réinitialise la taille du cube à sa taille d'origine
        transform.localScale = originalScale;
    }
  
    IEnumerator timerColor(){
        while(timeColor>= 0){
            timeColor --;
            Debug.Log("TIMER"+timeColor);
            yield return new WaitForSeconds(1f);
        }
    }

    /*
     * permet de au joueur de pousser le cube 
    private void PushCube()
    {
        myRigidbody.isKinematic = false;
        myRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
        myRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotation;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezePositionY;
        myRigidbody.constraints = ~RigidbodyConstraints.FreezeRotationY;

        // Applique une force pour pousser le cube
        // Direction de la poussée en fonction de la manette
        Vector3 pushDirection = transform.position + Camera.main.transform.position; // Direction du joueur vers l'objet
        pushDirection.y = 0; // Ignore la hauteur pour éviter de pousser en l'air
        myRigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.Force);
    }
    */

}
