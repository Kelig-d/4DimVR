using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity;
using static UnityEngine.Rendering.DebugUI;

public class ZimmaBlueColor : MonoBehaviour
{

    public bool Effet = false;

    public float targetMin = 0.5f;
    public float targetHeight = 5f;  // Hauteur cible à laquelle le cube doit monter
    public float moveSpeed = 1f;
    private Vector3 originalScale; // Nouvelle taille lorsqu'il est violet

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
    public SpawnerZoneZimaBlue spawnerzonezimablue;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            spawnerzonezimablue = GameObject.Find("SpawnerZimaBlue").GetComponent<SpawnerZoneZimaBlue>();
        }
        catch (NullReferenceException _)
        {
            
        }
        
        myRigidbody = GetComponent<Rigidbody>();
        myMaterial = GetComponent<Renderer>().materials;
        timer = new float[myMaterial.Length];
        foreach (var tps in myMaterial)
        {
            tps.SetFloat("_ShaderX", 0.0f);
        }
        Renderer renderer = GetComponent<Renderer>();
        originalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (Effet)
            EffetColor();
    
        RetourNormalColor();
    }

    public void ChangeColor(Color newcolor, int materialhit){
        // Dans le cas ou il y'a un seul material les calcul peuvent être compliquer donc on peut prendre 1
        if (materialhit == -1)
        {
            materialhit = 0;
        }
        try
        {
            // si pas de couleur donnée par le joueur
            if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.0f)
            {
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
            else if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.25f && myMaterial[materialhit].color == newcolor)
            {
                myMaterial[materialhit].SetFloat("_ShaderX", 0.5f);
                // ajoute du temps supplementaire 
                timer[materialhit] = 10f;

            }
            // si objet même couleur que le laser et si il est remplis à 0.50
            else if (myMaterial[materialhit].GetFloat("_ShaderX") == 0.50f && myMaterial[materialhit].color == newcolor)
            {
                myMaterial[materialhit].SetFloat("_ShaderX", 0.75f);
                // ajoute du temps supplementaire 
                timer[materialhit] = 30f;

            }
        }catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
       
    }

    public void EffetColor()
    {
        if (Effet)
        {
            for (int i = 0; i < myMaterial.Length; i++)
            {
                if (myMaterial[i].GetFloat("_ShaderX") == 0.75f)
                {
                    if (myMaterial[i].color.ToString() == colorBlue.ToString())
                    {
                        // Déplace le cube progressivement vers le BAS en fonction de la vitesse
                        float step = moveSpeed * Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetMin, step), transform.position.z);

                    }
                    else if (myMaterial[i].color.ToString() == colorYellow.ToString() && transform.position.y < targetHeight)
                    {
                        // Déplace le cube progressivement vers le haut en fonction de la vitesse
                        float step = moveSpeed * Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetHeight, step), transform.position.z);

                    }
                    else if (myMaterial[i].color.ToString() == colorPurple.ToString())
                    {
                        // Change la taille du cube
                        transform.localScale = Vector3.Lerp(transform.localScale, originalScale / 2f, Time.deltaTime * 2f);
                    }
                    else if (myMaterial[i].color.ToString() == colorWhite.ToString())
                    {
                        GetComponent<Renderer>().enabled = false;  // Cache le cube
                    }
                }
            }

        }



    }


    //La couleur de l'objet redevient à sa couleur d'orignie en fonction du temps qui passe
    public void RetourNormalColor(){
        for (int i = 0; i < myMaterial.Length; i++)
        {
            float tpstimeColor = timer[i];
            float tpsShaderX = myMaterial[i].GetFloat("_ShaderX");
            GetComponent<Renderer>().enabled = true;

            if (tpsShaderX == 0.75f && tpstimeColor <= 10f)
            {
                if(spawnerzonezimablue != null){
                    spawnerzonezimablue.nbMaxEnnemies += 1;
                }
                myMaterial[i].SetFloat("_ShaderX", 0.5f);
            }
            else if (tpsShaderX == 0.5f && tpstimeColor <= 5f)
            {
                myMaterial[i].SetFloat("_ShaderX", 0.25f);
            }
            else if (tpsShaderX == 0.25f && tpstimeColor <= 0.0f)
            {
                myMaterial[i].SetFloat("_ShaderX", 0.0f);
                ResetSize();
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

    private void ResetSize()
    {
        // Réinitialise la taille du cube à sa taille d'origine
        transform.localScale = originalScale;
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
