using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;
using static UnityEngine.Rendering.DebugUI;

public class ZimmaBlueJustColor : MonoBehaviour
{
    [SerializeField]
    private Material[] myMaterial;
    private Rigidbody myRigidbody;
    private BoxCollider myObject;
    private float[] timer;


    private Color colorBlue = new Color(0.082f,0.004f,1.0f,1.0f);
    private Color colorYellow = new Color(0.996f,1.0f,0.004f,1.0f);
    private Color colorGreen = new Color(0.133f,1.0f,0.004f,1.0f);
    private Color colorPurple = new Color(0.761f,0.004f,1.0f,1.0f);
    private Color colorRed = new Color(1.0f,0.004f,0.004f,1.0f);
    private Color colorWhite = new Color(1.0f,1.0f,1.0f,1.0f);

    private bool launche = false;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().materials;
        timer = new float[myMaterial.Length];
        foreach (var tps in myMaterial)
        {
            tps.SetFloat("_ShaderX", 0.0f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        RetourNormalColor();
    }

    public void ChangeColor(Color newcolor, int materialhit){
        Debug.Log("Material hit : " + materialhit);
        // si pas de couleur donnée par le joueur
        if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.0f){
            // object prend la couleur du laser 
            myMaterial[materialhit].color = newcolor;
            myMaterial[materialhit].SetFloat("_ShaderX", 0.25f);
            // ajoute du temps supplementaire 
            timer[materialhit] = 5f;

            // commencement du timer 
            if (!launche)
            {
                StartCoroutine(timerColor());
                launche = true;
            }
        }
        // si objet même couleur que le laser et si il est remplis à 0.25
        else if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.25f && myMaterial[materialhit].color == newcolor){
            myMaterial[materialhit].SetFloat("_ShaderX", 0.5f);
            // ajoute du temps supplementaire 
            timer[materialhit] = 10f;

        }
        // si objet même couleur que le laser et si il est remplis à 0.50
        else if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.50f && myMaterial[materialhit].color == newcolor){
            myMaterial[materialhit].SetFloat("_ShaderX", 0.75f);
            // ajoute du temps supplementaire 
            timer[materialhit] = 30f;

        }
    }

    //La couleur de l'objet redevient à sa couleur d'orignie en fonction du temps qui passe
    public void RetourNormalColor(){
        for (int i = 0; i < myMaterial.Length; i++)
        {
            float tpstimeColor = timer[i];
            float tpsShaderX = myMaterial[i].GetFloat("_ShaderX");

            if (tpsShaderX == 0.75f && tpstimeColor <= 10f)
            {
                myMaterial[i].SetFloat("_ShaderX", 0.5f);
            }
            else if (tpsShaderX == 0.5f && tpstimeColor <= 5f)
            {
                myMaterial[i].SetFloat("_ShaderX", 0.25f);
            }
            else if (tpsShaderX == 0.25f && tpstimeColor <= 0.0f)
            {
                myMaterial[i].SetFloat("_ShaderX", 0.0f);
            }


        }
            
    }

    public void timepass()
    {
        for (int i = 0;i < myMaterial.Length;i++) 
        {
            float timePass = timer[i];
            if(timePass > 0)
            {
                timer[i]-=1f;
            }
        }
    }

    public bool checkTime()
    {
        foreach (float val in timer)
        {
            if(val>0f) return true;
        }
        return false;
    }
  
    IEnumerator timerColor(){
        while(checkTime()){
            timepass();
            yield return new WaitForSeconds(1f);
        }
        launche = false;
        Debug.Log("END TIMER");
    }

}
