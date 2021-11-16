using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IInventory : MonoBehaviour
{
    public List<IObject> inventory;
    public Text inventarioText;
    int x = 0, xMax= -1;
    public Transform inventoryUI;
    public ISlotUI slotUIPrefab;
    private string select1, select2;
    private GameObject objeto1, objeto2;
    private int num1, num2;
    public GameObject espada, arco, pistola, flecha;
    private ControlJugador controljugador;
    public Transform aparicion;
    private void Start()
    {
        UpdateUI();
        controljugador = FindObjectOfType<ControlJugador>();
    }
    private void Update()
    {
        Instaciador();
        elimStac();
        EspacioInventario();
        crafting();
    }

    public void Add(IObject item)
    {
        //var newSlot = new ISlot(item.name, item.description, item.quantity, item.stackable);
        //inventory.Add(newSlot);

        if (Find(item.name) == null)
        {
            inventory.Add(item);
        }
        else if (Find(item.name).stackable)
        {
            Find(item.name).quantity++;
        }
    }

    public IObject Find(string name) => inventory.Find((item) => item.name == name);

    public void Remove(IObject item)
    {
        inventory.Remove(item);
        UpdateUI();
    }

    public void UpdateUI()
    {
        IStatus status = FindObjectOfType<IStatus>();
        foreach (Transform child in inventoryUI) if (child.gameObject != slotUIPrefab.gameObject) Destroy(child.gameObject);
        foreach (IObject item in inventory)
        {
            
            ISlotUI slot = Instantiate(slotUIPrefab.gameObject, inventoryUI).GetComponent<ISlotUI>();
            slot.itemName.text = item.name + " x " + item.quantity;
            slot.delete.onClick.AddListener(() => {
                Destroy(item.gameobject);
                Remove(item); });
            slot.removeOne.onClick.AddListener(() => 
            {
                if (!item.stackable) {
                    Destroy(item.gameobject);
                    Remove(item);
                } 
                else status.Elim1();
            } );
            slot.select.onClick.AddListener(() => {
                if (!(select1 == null) && select2 == null) Asigncrafting(item.name, item.gameobject, inventory.IndexOf(item), "segundo");
                if (select1 == null) Asigncrafting(item.name, item.gameobject, inventory.IndexOf(item), "primero");
            });
            slot.gameObject.SetActive(true);
        }
    }
    void Asigncrafting(string name, GameObject objeto, int i,string cual)
    {
        if(cual== "primero") { select1 = name; objeto1 = objeto; num1 = i; }

        if (cual == "segundo") { select2 = name; objeto2 = objeto; num2 = i; }
         
        
        
    }
    void crafting()
    {
        if (!(select1 == null && select2 == null))
        {
            if ((select1 == "Palo" && select2 == "Piedra") || (select1 == "Piedra" && select2 == "Palo"))
            {
                Instantiate(flecha,aparicion.position,aparicion.rotation);
                
                DestroyCrafting();
                UpdateUI();
            }
            else if ((select1 == "Cargador" && select2 == "Hierro") || (select1 == "Hierro" && select2 == "Cargador"))
            {
                Instantiate(pistola, aparicion.position, aparicion.rotation);

                DestroyCrafting();
                UpdateUI();
            }
            else if ((select1 == "Palo" && select2 == "Hierro") || (select1 == "Hierro" && select2 == "Palo"))
            {
                Instantiate(espada, aparicion.position, aparicion.rotation);

                DestroyCrafting();
                UpdateUI();
            }
            else if ((select1 == "Palo" && select2 == "Tela Araña") || (select1 == "Tela Araña" && select2 == "Palo"))
            {
                Instantiate(arco, aparicion.position, aparicion.rotation);

                DestroyCrafting();
                UpdateUI();
            }
            else
            {
                if (!(select2 == null))
                {
                    IStatus status = FindObjectOfType<IStatus>();
                    status.alerta("Crafteo invalido");
                    select1 = null;
                    select2 = null;
                }
            }
        }
    }

    private void DestroyCrafting()
    {
        Destroy(objeto1);
        Destroy(objeto2);
        Remove(inventory[num1]);
        Remove(inventory[num2]);
        select1 = null;
        select2 = null;
    }

    void Instaciador()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (x == 0) x = xMax;
            else x--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (x == xMax) x = 0;
            else x++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && xMax >= 0) x = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && xMax >= 1) x = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && xMax >= 2) x = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && xMax >= 3) x = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5) && xMax >= 4) x = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6) && xMax >= 5) x = 5;

        if (xMax >= 0) { 
            for (int i = 0; i < inventory.Count; i++)
            {
                if (i != x) { inventory[i].gameobject.SetActive(false); inventory[i].activo = false; };
            }
        inventory[x].gameobject.SetActive(true);
        inventory[x].activo = true;
    }
    }
    public void elimStac ()
        {
        foreach (IObject i in inventory)
        {
            if (i.stackable && i.quantity == 0)
            {
                Destroy(i.gameobject);
                Remove(i);
            }
        }
        }
    void EspacioInventario() 
    {
        int i = -1;
        foreach (IObject item in inventory)
        {           
            i++;
            xMax= i;
        }
    }
  
}
