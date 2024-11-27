using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VersionControl.Git;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class StarGateKeyEnigme : MonoBehaviour
{
    public GameObject Door;

    public GameObject Indice0;
    public GameObject Indice1;
    public GameObject Indice2;
    public GameObject Indice3;
    public GameObject Machine;

    public Color Value0;
    public Color Value1;
    public Color Value2;
    public Color Value3;
    public Color Value4;
    public Color Value5;
    public Color Value6;
    public Color Value7;
    public Color Value8;
    public Color Value9;

    private XRGrabInteractable grabInteractable;
    private BtnStarGate[] btnStarGates;

    readonly List<Color> AllColors = new List<Color>();
    List<Color> colors = new List<Color>();
    List<Color> Solutioncolors = new List<Color>();

    public void Start()
    {
        getColor();
        SelectRandColor();
        
        int i = 0;
        
        btnStarGates = Machine.GetComponentsInChildren<BtnStarGate>(false);


        foreach (var btn in btnStarGates)
        {
            print(i + " / " + AllColors.Count);
            btn.SetColor(AllColors[i]);
            i++;
        }

        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable manquant sur cet objet !");
        }

    }

    private void getColor()
    {
        AllColors.Add(Value0);
        AllColors.Add(Value1);
        AllColors.Add(Value2);
        AllColors.Add(Value3);
        AllColors.Add(Value4);
        AllColors.Add(Value5);
        AllColors.Add(Value6);
        AllColors.Add(Value7);
        AllColors.Add(Value8);
        AllColors.Add(Value9);

    }

    private void SelectRandColor()
    {
        print("start SelectRandColor");

        List<Color> ListColorSelect = new List<Color>();

        AllColors.ForEach( delegate(Color value){ ListColorSelect.Add(value);});
       
        int rand;
        for (int i = 0; i < 4; i++)
        {
            rand = Random.Range(0, ListColorSelect.Count);
            Solutioncolors.Add(ListColorSelect[rand]);
            ListColorSelect.RemoveAt(rand);
        }

        Indice0.GetComponent<Renderer>().material.color = Solutioncolors[0];
        Indice1.GetComponent<Renderer>().material.color = Solutioncolors[1];
        Indice2.GetComponent<Renderer>().material.color = Solutioncolors[2];
        Indice3.GetComponent<Renderer>().material.color = Solutioncolors[3];


    }

    public void MessageAddCode(Color input)
    {
        colors.Add(input);

    }
    public void MessageRemoveCode(Color input)
    {
        colors.Remove(input);
    }



    private void OnGrab(SelectEnterEventArgs args)
    {
        if (checkValue())
        {
            Destroy(Door);  // MOVE DOOR
        }
    }

    bool AreAllColorsInListB()
    {
       
        foreach (Color color in colors)
        {
            if (!Solutioncolors.Contains(color)) 
            {
                return false;
            }
        }
        return true; 
    }

    private bool checkValue()
    {

        foreach (var btn in btnStarGates)
        {
            btn.Reset();
        }

        if (colors.Count == Solutioncolors.Count)
        {
            bool result = AreAllColorsInListB();
            colors.Clear();
            return result;
        }

        return false;
    }

}
