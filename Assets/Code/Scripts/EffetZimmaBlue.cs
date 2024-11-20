using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetZimmaBlue : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private Rigidbody myRigidbody;

    private float timeColor;

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
        if(myMaterial.GetFloat("_ShaderX") == 0.75f && timeColor == 30.0f ){
            myMaterial.SetFloat("_ShaderX",0.5f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 15.0f ){
            myMaterial.SetFloat("_ShaderX",0.25f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f && timeColor == 0.0f ){
            myMaterial.SetFloat("_ShaderX",0.0f);
        }
    }

    public void ChangeColor(Color newcolor){
        Debug.Log("CHANGECOLOR");
        if(myMaterial.GetFloat("_ShaderX") == 0.0f){
            myMaterial.color = newcolor;
            myMaterial.SetFloat("_ShaderX",0.25f);
            timeColor= 5f;
            StartCoroutine(timerColor());
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f && myMaterial.color == newcolor){
            myMaterial.SetFloat("_ShaderX",0.5f);
            timeColor += 5f ; 
        }else if (myMaterial.GetFloat("_ShaderX") == 0.50f && myMaterial.color == newcolor){
            myMaterial.SetFloat("_ShaderX",0.75f);
            timeColor += 35f ; 
            if ( myMaterial.color == new Color(21,5,255))
            {
                myRigidbody.isKinematic = false;
            }
            // Modif here


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
