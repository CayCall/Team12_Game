using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    public TMP_Text itemCount;
    public GameObject itemImageHolder;
    Sprite itemImage;

    public InventorySlot invSlot;

    bool highlightInvSlot;
    //[SerializeField] Image backdropImage;
    Color backdropColor;

    [SerializeField] MouseItemData mouseInvItem;

    public RectTransform invPanel;
    private AudioManager _audioManager;

    //public Button activeButton;
    //public Button dropButton;

    //public DropLoot dropLoot;

    //GameObject weaponControl;
    bool activeWeapon;

    //Stats detailed display
    //[SerializeField] Image statDisplayImage;
    //[SerializeField] TMP_Text statDisplayText;


    public BlockManager blockManager;

    public bool Hotbar = false;

    void Awake()
    {
        // itemImage = itemImageHolder.GetComponent<Image>().sprite;
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("We've clicked on an inventory slot");
        _audioManager.PlaySound("UI Item");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("We left clicked the slot");
            
            if (invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData == null) //Begin Drag
            {

                //HighlightOn();

                Debug.Log("Clicked a slot");
                mouseInvItem.UpdateMouseSlot(invSlot);
                invSlot.ClearSlot();
                mouseInvItem.mouseCanvasGroup.blocksRaycasts = false;
                //HighlightOff();

                return;


            }

            else if (invSlot.itemData == null && mouseInvItem.asgInvSlot.itemData != null) // Drop
            {



                Debug.Log("Drop made");
                invSlot.AsignSlot(mouseInvItem.asgInvSlot);
                mouseInvItem.ClearSlot();
                
                if (Hotbar)
                {
                    blockManager.AvailableBuildingBlocks.Add(invSlot.itemData);
                }



            }

            else if (invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData != null)
            {
                if (invSlot.itemData == mouseInvItem.asgInvSlot.itemData)
                {
                    invSlot.AddToStack(mouseInvItem.asgInvSlot.stackSize);
                    mouseInvItem.asgInvSlot.RemoveFromStack(mouseInvItem.asgInvSlot.stackSize);
                    return;
                }

                else 
                {
                    InventorySlot switchSlot = new InventorySlot();
                    switchSlot.UpdateInventorySlot(invSlot.itemData, invSlot.stackSize);

                    invSlot.AsignSlot(mouseInvItem.asgInvSlot);
                    mouseInvItem.UpdateMouseSlot(switchSlot);

                    return;
                }
            }

        }


        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("We right clicked");

            if (invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData == null) //Pick up half
            {

                //Creating a slot to store values just incase this slot is cleared
                InventorySlot saveSlot = new InventorySlot();
                saveSlot.UpdateInventorySlot(invSlot.itemData, invSlot.stackSize);

                
                int stackRemoved = invSlot.RemoveRightClick();
                mouseInvItem.UpdateMouseSlot(saveSlot,stackRemoved);

         
                mouseInvItem.mouseCanvasGroup.blocksRaycasts = false;
                

                return;


            }

            else if (invSlot.itemData == null && mouseInvItem.asgInvSlot.itemData != null) // Drop just 1
            {

                //initialiaze, add 1 to inventory, and make sure it stays stationary in this position

                Debug.Log("Drop made");
                //invSlot.AsignSlot(mouseInvItem.asgInvSlot);

                invSlot.UpdateInventorySlot(mouseInvItem.asgInvSlot.itemData, 1); //for the case where we add 1 to a null slot

                mouseInvItem.asgInvSlot.RemoveFromStack(1);

                if (Hotbar)
                {
                    blockManager.AvailableBuildingBlocks.Add(invSlot.itemData);
                }



            }

            else if(invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData != null)
            {
                if(invSlot.itemData == mouseInvItem.asgInvSlot.itemData)
                {
                    //just add one to the stack 
                    invSlot.AddToStack(1);
                    mouseInvItem.asgInvSlot.RemoveFromStack(1);
                }
               
            }


        }



    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //So if the slot we click on has an item, and the mouse does not have an item, we want to pick up


      /* if (invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData == null)
        {
            Debug.Log("Drag begin");
            mouseInvItem.UpdateMouseSlot(invSlot);
            invSlot.ClearSlot();
            mouseInvItem.mouseCanvasGroup.blocksRaycasts = false;
            //HighlightOff();

            return;
        }*/


    }
    public void OnDrag(PointerEventData eventData)
    {




    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
        /* 
             //slot doesnt have an item
             if (invSlot.itemData == null && mouseInvItem.asgInvSlot.itemData != null)
             {
                 Debug.Log("Drag ended" + this.gameObject);
                 //mouseInvItem.mouseCanvasGroup.blocksRaycasts = true;
                 invSlot.AsignSlot(mouseInvItem.asgInvSlot);
                 mouseInvItem.ClearSlot();
                 //HighlightOff();
             }
             else
             {
                 Debug.Log("First condition didnt work");
             }
         */

    }

    public void OnDrop(PointerEventData eventData)
    {


        Debug.Log("Drop made");

     /*   if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            //slot doesnt have an item
            if (invSlot.itemData == null && mouseInvItem.asgInvSlot.itemData != null)
            {
                Debug.Log("Drop made");
                invSlot.AsignSlot(mouseInvItem.asgInvSlot);
                mouseInvItem.ClearSlot();
            }
            else
            {

            }

        }
*/

    }



    public void UpdateSlot()
    {
        if (invSlot.itemData != null)
        {
            
            itemCount.text = invSlot.stackSize.ToString();

            itemImageHolder.GetComponent<Image>().sprite = invSlot.itemData.itemIcon;
        }

        else if (invSlot.itemData == null)
        {
            itemCount.text = "";

            itemImageHolder.GetComponent<Image>().sprite = null;
        }

    }

  /*  public void HighlightOn()
    {

        backdropImage.color = Color.yellow;
        highlightInvSlot = true;
        this.gameObject.GetComponentInParent<InventorySystem>().CheckHighlights(invSlot);

        if (invSlot.itemData.weaponType)
        {
            activeButton.interactable = true;
            dropButton.interactable = true;

        }


        DisplayStat();

    }
*/


/*
    public void HighlightOff()
    {
        if (highlightInvSlot == true)
        {

            highlightInvSlot = false;
            backdropImage.color = backdropColor;

            activeButton.interactable = false;
            dropButton.interactable = false;

            HideStat();
        }

    }*/
/*
    public void HighligtActive()
    {
        if (highlightInvSlot == true && activeWeapon == true)
        {
            backdropImage.color = Color.green;
            highlightInvSlot = true;
            this.gameObject.GetComponentInParent<InventorySystem>().CheckHighlights(invSlot);

            activeButton.interactable = false;
            dropButton.interactable = true;
        }
    }

*/


   /* public void LootToDrop()
    {
        //Drop the item
        //Removing it from inventory and instantiating it in the game world
        Debug.Log("Drop Key Pressed");

        dropLoot.LootDrop(invSlot.itemData);
        if (activeWeapon == true) //invSlot.GetComponent<InventoryDisplay>().activeWeapon == true
        {
            weaponControl.GetComponent<ActiveWeapon>().NoWeapon(invSlot.itemData);
            activeWeapon = false;
        }
        invSlot.ClearSlot();
        HighlightOff();
    }*/


    public void SlotBecomeEmpty()
    {
        if (invSlot.stackSize == 0)
        {
            invSlot.ClearSlot();
           // HighlightOff();
        }
    }

    public bool GetInvSlotHighlight()
    {
        return highlightInvSlot;
    }




   /* public void ActivateWeapon()
    {
        Debug.Log("The weapon should work now");

        weaponControl.GetComponent<ActiveWeapon>().SetWeapon(invSlot.itemData);

        this.gameObject.GetComponentInParent<InventorySystem>().CheckActive(invSlot);
        activeWeapon = true;

    }*/


    public void ActivateOff()
    {
        if (activeWeapon == true)
        {
            activeWeapon = false;
        }

    }

    public void DefaultActivate()
    {
        activeWeapon = true;
    }

    public void DisplayStat()
    {
        /*if (invSlot.itemData.weaponType)
        {

            statDisplayImage.color = Color.white;
            statDisplayImage.sprite = invSlot.itemData.itemIcon;

          *//*  statDisplayText.text = invSlot.itemData.Description +
                                   "\n " +
                                   "\n Damage  : " + invSlot.itemData.damage +
                                   "\n Fire Rate  : " + invSlot.itemData.fireRate + " s" +
                                   "\n Reload Speed  : " + invSlot.itemData.reloadSpeed + " s" +
                                   "\nAmmo Type  : " + invSlot.itemData.ammoNeeded +
                                   "\n Magazine Size : " + invSlot.itemData.magazineSize +
                                   "\n Maximum Ammo  : " + invSlot.itemData.ammoMax +
                                   "\n Accuracy  : " + invSlot.itemData.accuracy + "%";*//*
        }

        else
        {
            statDisplayImage.color = Color.white;
            statDisplayImage.sprite = invSlot.itemData.itemIcon;

            statDisplayText.text = invSlot.itemData.Description;
        }*/
    }

    public void HideStat()
    {
       /* statDisplayImage.color = Color.clear;
        statDisplayText.text = "";*/
    }

    


    // Start is called before the first frame update
    void Start()
    {
        //backdropColor = backdropImage.color;
        highlightInvSlot = false;
        //itemImage = itemImageHolder.GetComponent<Image>().sprite;

        //activeButton.interactable = false;
        //dropButton.interactable = false;


        //weaponControl = GameObject.Find("ActiveWeapon");

        //StatDisplay
        //GameObject statObj = GameObject.Find("ItemInformation");
        //statDisplayImage = statObj.GetComponent<Image>();

        //GameObject wordObj = GameObject.Find("Stat");
        //statDisplayText = wordObj.GetComponent<TextMeshProUGUI>();

        //statDisplayImage.color = Color.clear;
        //statDisplayText.text = "";


        //Mouse Object
        if(mouseInvItem == null)
        {
            GameObject mouseObj = GameObject.Find("MouseObject");
            mouseInvItem = mouseObj.GetComponent<MouseItemData>();
        }


        blockManager = GameObject.FindGameObjectWithTag("Player").GetComponent<BlockManager>();



    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();

        //HighligtActive();

        SlotBecomeEmpty();


        if (Input.GetMouseButtonDown(0))
        {

        }


        if (highlightInvSlot == true && invSlot.itemData.weaponType)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                //LootToDrop();
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                //ActivateWeapon();
            }
        }
    }
}
