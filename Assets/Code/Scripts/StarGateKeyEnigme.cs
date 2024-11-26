using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGateKeyEnigme : MonoBehaviour
{
    public GameObject Door;

    public Color k0;
    public Color k1;
    public Color k2;
    public Color k3;

    List<Color> colors = new List<Color>();
    List<Color> Solutioncolors = new List<Color>();

    public void Start()
    {
        Solutioncolors.Add(k0);
        Solutioncolors.Add(k1);
        Solutioncolors.Add(k2);
        Solutioncolors.Add(k3);
    }


    public void MessageAddCode(Color input) { 
        colors.Add(input);

    }

    public void MessageRemoveCode(Color input)
    {
        colors.Remove(input);
    }

    public bool checkValue()
    {
        if(colors.Count == Solutioncolors.Count)
        {
            Solutioncolors.Sort();
            colors.Sort();

            return Solutioncolors.Equals(colors);
        }

        return false;
    }

    public void MessageTest() { 
        if (checkValue())
        {
            Destroy(Door);  // MOVE DOOR
        }
    
    }

 
}
