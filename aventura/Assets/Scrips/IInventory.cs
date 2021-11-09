using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IInventory : MonoBehaviour
{
    public List<IObject> inventory;
    public Text inventarioText;
    int x = 0, xMax= -1;


    private void Update()
    {
        SetearTexto();
        Instaciador();
        elimStac();
        EspacioInventario();
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
    }

    void SetearTexto()
    {
        string inventariot = "";
        int boton = 0;
        foreach (var item in inventory)
        {
            string texto = "";
            string nombre = item.name;
            boton++;
            if (item.stackable)
            {
                int cantidad = 0;
                cantidad = item.quantity;
                texto = nombre + "( cant: " + cantidad + ")";
            }
            else texto = nombre;
            inventariot = inventariot + boton + " " + texto + "\n";
        }
        inventarioText.text = inventariot;

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
