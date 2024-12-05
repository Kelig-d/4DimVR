using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RadialSelection : MonoBehaviour
{
    public InputActionReference spawnButton;

    [Range(2,10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public Transform handTransform;
    public float angleBetweenPart = 10;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    private List<string> TextMenue;
    private bool select;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.action.started += ButtonWasPressed;
        // spawnButton.action.canceled += ButtonWasReleased;
        /*
        TextMenue = new List<string>();
        TextMenue.Add("Menu");
        TextMenue.Add("Param");
        TextMenue.Add("XXX");
       */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(select == true){
            GetSelectedRadialPart();
        }
        */
    }
    
    public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        StartCoroutine(LoadNewDimension());
        
        //SpawnRadialPart();
        select = true;

    }

    private IEnumerator LoadNewDimension()
    {
        string oldscene = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        // Désactiver l'ancienne scène
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }

        SceneManager.UnloadSceneAsync(oldscene);
    }
    /*
    public void ButtonWasReleased(InputAction.CallbackContext context){
        HideAndTriggerSelected();
        select = false;
    }

    public void HideAndTriggerSelected(){
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart(){
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if(angle < 0){
            angle += 360;
        }

        currentSelectedRadialPart = (int)angle * numberOfRadialPart /360;

        for(int i = 0; i < spawnedParts.Count; i++){
            if(i == currentSelectedRadialPart){
                spawnedParts[i].GetComponent<Image>().color = Color.green;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            } else {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
        }

    }

    public void SpawnRadialPart(){
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts){
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i=0; i < numberOfRadialPart; i++){
            float angle = - i *360 /numberOfRadialPart - angleBetweenPart /2;
            Vector3 radialPartEulerAngle = new Vector3(0,0,angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab,radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;
            
//            TextMesh test = TextMesh. GameObject.FindGameObjectWithTag("txtMenu");

           //Destroy(spawnedRadialPart.GetComponent(TextMesh));
  //          print(test.text);

            //test.text = "Hello";//TextMenue[i];

            spawnedRadialPart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenPart/360);


            spawnedParts.Add(spawnedRadialPart);
        }

        TextMesh test = FindAnyObjectByType<TextMesh>();
        test.bool();
    }*/
}
