using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IInventory : MonoBehaviour
{
    public List<ISlot> inventory;
    public Text inventarioText;
    public int x = 0, xMax= -1;
    public Transform inventoryUI, aparicion2;
    public ISlotUI slotUIPrefab;
    public GameObject espada, arco, pistola, flecha, barricada;
    private ControlJugador controljugador;
    private Transform aparicion;
    public GameObject camara;
    public Transform mano;
    public Transform CraftUI,BuildUI;
    public ISlotUI CraftingPrefab;
    public string crafteo;
    public List<string> crafteos;
    public List<string> buildings;
    public ISlotUI BuildingPrefab;
    private void Start()
        {
            UpdateUI();
    }

    private void Update()
    {
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out RaycastHit hit, 5f))
        {
            if (Input.GetKey(KeyCode.E))
                if (hit.collider.CompareTag("agarrable") || hit.collider.CompareTag("Building"))
                {
                    IObject objeto = hit.collider.GetComponentInParent<IObject>();
                    Add(objeto);
                }
            if (hit.collider.CompareTag("mesa"))
            {
                aparicion = hit.collider.transform.GetChild(0).gameObject.transform;
            }

        }
        Instaciador();
        setmax();
    }

        public void Add(IObject item)
        {
            var newSlot = new ISlot(item.name, item.description, item.quantity, item.stackable,item.gameobject);
        item.gameobject.SetActive(false);
            
            if (Find(item.name) == null || !Find(item.name).stackable)
            {
            inventory.Add(newSlot);
             }
            else
            {
                Find(item.name).quantity += item.quantity;
            }
        UpdateUI();
        }

        public ISlot Find(string name) => inventory.Find((item) => item.name == name);

        public void Remove(string name, int quantity, GameObject objeto)
        {
            var slot = Find(name);

        if (slot.quantity - quantity <= 0)
        {
            inventory.Remove(Find(name));
            Destroy(objeto);
        }
        else
            slot.quantity -= quantity;

            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (Transform child in inventoryUI) if (child.gameObject != slotUIPrefab.gameObject) Destroy(child.gameObject);
            foreach (var item in inventory)
            {
                ISlotUI slot = Instantiate(slotUIPrefab.gameObject, inventoryUI).GetComponent<ISlotUI>();
                slot.itemName.text = item.name + " x " + item.quantity;
                slot.delete.onClick.AddListener(() => Remove(item.name, item.quantity,item.objeto));
                slot.removeOne.onClick.AddListener(() => Remove(item.name, 1,item.objeto));
                slot.gameObject.SetActive(true);
            }
        foreach (Transform child in CraftUI) if (child.gameObject != CraftingPrefab.gameObject) Destroy(child.gameObject);
        Asigncrafting();
        foreach (var item in crafteos)
        {           
                ISlotUI slot = Instantiate(CraftingPrefab.gameObject, CraftUI).GetComponent<ISlotUI>();
                slot.itemName.text = item;
                slot.craft.onClick.AddListener(() => crafting(item));
                slot.gameObject.SetActive(true);
        }
        foreach (Transform child in BuildUI) if (child.gameObject != BuildingPrefab.gameObject) Destroy(child.gameObject);
        foreach (var item in buildings)
        {
            ISlotUI slot = Instantiate(BuildingPrefab.gameObject, BuildUI).GetComponent<ISlotUI>();
            slot.itemName.text = item;
            slot.build.onClick.AddListener(() => building(item));
            slot.gameObject.SetActive(true);
        }
    }
    void building(string i)
    {
        if (i == "Barricada") Instantiate(barricada, aparicion2.position, Quaternion.identity);
    }
    void Asigncrafting()
    {
        bool Palo = false;
        bool piedra = false;
        bool hierro = false;
        bool TelaAraña = false;
        bool Cargador = false;
        crafteos.Clear();
            foreach (var i in inventory)
        {
            if (i.name == "Palo") Palo = true;
            if (i.name == "Piedra") piedra = true;
            if (i.name == "Hierro") hierro = true;
            if (i.name == "Tela Araña") TelaAraña = true;
            if (i.name == "Cargador") Cargador = true;
        }
       
        if (Palo && piedra  ) crafteos.Add("flecha");
        if (Palo && hierro  ) crafteos.Add("espada");
        if (Palo && TelaAraña ) crafteos.Add("arco");
        if (Cargador && hierro ) crafteos.Add("pistola");
        

    }
    void crafting(string crafteo)
    {
     if(crafteo== "flecha")
        {
            for (int i = 0; i < inventory.Count; i++)               
                if (inventory[i].name == "Palo") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);

            for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].name == "Piedra") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);    
            
            Instantiate(flecha, aparicion.position, Quaternion.identity);
        }
        if (crafteo == "espada")
        {
            for (int i = 0; i < inventory.Count; i++)

                if (inventory[i].name == "Palo") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].name == "Hierro") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            Instantiate(espada, aparicion.position, Quaternion.identity);
        }
        if (crafteo == "arco")
        {
            for (int i = 0; i < inventory.Count; i++)

                if (inventory[i].name == "Palo") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].name == "Tela Araña") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            Instantiate(arco, aparicion.position, Quaternion.identity);
        }
        if (crafteo == "pistola")
        {
            for (int i = 0; i < inventory.Count; i++)

                if (inventory[i].name == "Cargador") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].name == "Hierro") Remove(inventory[i].name, inventory[i].quantity, inventory[i].objeto);
            Instantiate(pistola, aparicion.position, Quaternion.identity);
        }
        UpdateUI();
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

        if (xMax >= 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if(!(i==x))
                inventory[i].objeto.SetActive(false);

            }
            inventory[x].objeto.transform.rotation = mano.rotation;
            inventory[x].objeto.transform.position = mano.position;
                inventory[x].objeto.SetActive(true);
            FuncionObjeto obj = inventory[x].objeto.GetComponent<FuncionObjeto>();
            obj.funcion();


        }
    }
    void setmax()
    {
        int x = -1;
        for (int i = 0; i < inventory.Count; i++)
        {

            x++;
        }
        xMax = x;
    }
        [System.Serializable]
    public class ISlot
    {
        public string name, description;
        public int quantity;
        public bool stackable;
        public GameObject objeto;

        public ISlot(string name, string description, int quantity, bool stackable,GameObject objeto)
        {
            this.name = name;
            this.description = description;
            this.quantity = quantity;
            this.stackable = stackable;
            this.objeto = objeto;
        }
    } }