using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetZimmaBlue : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private float timeColor = 30f;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_ShaderX",0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(myMaterial.GetFloat("_ShaderX") == 0.75f && timeColor == 60.0f ){
            myMaterial.SetFloat("_ShaderX",0.5f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.5f && timeColor == 30.0f ){
            myMaterial.SetFloat("_ShaderX",0.25f);
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f&& timeColor == 0.0f ){
            myMaterial.SetFloat("_ShaderX",0.0f);
        }
    }

    public void ChangeColor(){
        Debug.Log("CHANGECOLOR");
        if(myMaterial.GetFloat("_ShaderX") == 0.0f){
            myMaterial.SetFloat("_ShaderX",0.25f);
            StartCoroutine(timerColor());
        }else if (myMaterial.GetFloat("_ShaderX") == 0.25f){
            myMaterial.SetFloat("_ShaderX",0.5f);
            timeColor += 30 ; 
        }else if (myMaterial.GetFloat("_ShaderX") == 0.50f){
            myMaterial.SetFloat("_ShaderX",0.75f);
            timeColor += 100 ; 
        }
    }

    IEnumerator timerColor(){
        while(timeColor>= 0){
            timeColor --;
            yield return new WaitForSeconds(1f);
        }
    }
}
