using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject center;
    public GameObject itemslot;
    public DropInventory module;
    public bool toDestroy;

    public void reloaditem()
    {
        Destroy(itemslot);
        itemslot = Instantiate(Prefab);
        itemslot.transform.SetParent(transform);
        itemslot.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        itemslot.transform.position = center.transform.position;
 
        module = itemslot.gameObject.GetComponent<DropInventory>();
        module.inventorySlot = this;
        itemslot.SetActive(false);
    }

    public void Start()
    {
        reloaditem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && !other.GetComponent<XRGrabInteractable>().isSelected) // id de layer grabable
        {
            if (!itemslot.active)
            {
                toDestroy = other.gameObject.GetComponent<GetPrefab>() != null;


                if (toDestroy)
                {
                    Debug.Log("To Destroy enter");
                    itemslot.SetActive(true);
                    module.toDestroy = other.gameObject;
                    module.inInventory = true;
                    module.item = other.GetComponent<GetPrefab>().Prefab;
                    other.transform.position = new Vector3(-666, -666, -666);
                   
                }
                else
                {
                    Debug.Log("To -Destroy enter");
                    itemslot.SetActive(true);
                    module.inInventory = true;
                    module.item = other.gameObject;
                    module.item.SetActive(false);
                    module.item.transform.SetParent(transform);
                }



            }
        }
    }
}
