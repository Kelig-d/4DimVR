using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetZimmaBlue : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    public float positionY;

    private Rigidbody myRigidbody;

    private float timeColor;
    private Vector3 positionHaute;  // Position cible
    public float speed = 1.0f;
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
            EffetColor();
            // ajoute du temps supplementaire 
            timeColor += 35f ; 
        }
    }

    public void EffetColor(){
        if (myMaterial.color.ToString() == colorBlue.ToString()){
            // objet tombe
            myRigidbody.isKinematic = false;
        }else if(myMaterial.color.ToString() == colorYellow.ToString()){
            // objet monte 
            // definir à quelle hauteur l'objet pet monter au maximum
            positionHaute = new Vector3(transform.position.x, positionY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, positionHaute, speed * Time.deltaTime);
            Debug.Log(Vector3.Distance(transform.position, positionHaute));
        }else if(myMaterial.color.ToString() == colorGreen.ToString()){
            
        }else if(myMaterial.color.ToString() == colorPurple.ToString()){
            
        }else if(myMaterial.color.ToString() == colorRed.ToString()){
            
        }else if(myMaterial.color.ToString() == colorWhite.ToString()){
            gameObject.SetActive(false);
        }
        Debug.Log("ERROR COLOR");
    }

    //La couleur de l'objet redevient à sa couleur d'orignie en fonction du temps qui passe
    public void RetourNormalColor(){
        if(myMaterial.GetFloat("_ShaderX") == 0.75f && timeColor == 30.0f ){
            gameObject.SetActive(true);
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
