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
    [SerializeField] Image backdropImage;
    Color backdropColor;

    [SerializeField] MouseItemData mouseInvItem;

    public RectTransform invPanel;

    public Button activeButton;
    public Button dropButton;

    //public DropLoot dropLoot;

    GameObject weaponControl;
    bool activeWeapon;

    //Stats detailed display
    [SerializeField] Image statDisplayImage;
    [SerializeField] TMP_Text statDisplayText;




    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("We've clicked on an inventory slot");

        if (invSlot.itemData != null)
        {

            HighlightOn();

        }

        else if (invSlot.itemData == null)
        {

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //So if the slot we click on has an item, and the mouse does not have an item, we want to pick up
        if (invSlot.itemData != null && mouseInvItem.asgInvSlot.itemData == null)
        {
            Debug.Log("Drag begin");
            mouseInvItem.UpdateMouseSlot(invSlot);
            invSlot.ClearSlot();
            HighlightOff();

            return;
        }


    }
    public void OnDrag(PointerEventData eventData)
    {




    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //slot doesnt have an item
        if (invSlot.itemData == null && mouseInvItem.asgInvSlot.itemData != null)
        {
            Debug.Log("Drag ended" + this.gameObject);
            invSlot.AsignSlot(mouseInvItem.asgInvSlot);
            mouseInvItem.ClearSlot();
            HighlightOff();
        }
        else
        {

        }

    }

    public void OnDrop(PointerEventData eventData)
    {

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
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

    public void HighlightOn()
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

    }

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
            HighlightOff();
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
        if (invSlot.itemData.weaponType)
        {

            statDisplayImage.color = Color.white;
            statDisplayImage.sprite = invSlot.itemData.itemIcon;

          /*  statDisplayText.text = invSlot.itemData.Description +
                                   "\n " +
                                   "\n Damage  : " + invSlot.itemData.damage +
                                   "\n Fire Rate  : " + invSlot.itemData.fireRate + " s" +
                                   "\n Reload Speed  : " + invSlot.itemData.reloadSpeed + " s" +
                                   "\nAmmo Type  : " + invSlot.itemData.ammoNeeded +
                                   "\n Magazine Size : " + invSlot.itemData.magazineSize +
                                   "\n Maximum Ammo  : " + invSlot.itemData.ammoMax +
                                   "\n Accuracy  : " + invSlot.itemData.accuracy + "%";*/
        }

        else
        {
            statDisplayImage.color = Color.white;
            statDisplayImage.sprite = invSlot.itemData.itemIcon;

            statDisplayText.text = invSlot.itemData.Description;
        }
    }

    public void HideStat()
    {
        statDisplayImage.color = Color.clear;
        statDisplayText.text = "";
    }



    void Awake()
    {
        // itemImage = itemImageHolder.GetComponent<Image>().sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        backdropColor = backdropImage.color;
        highlightInvSlot = false;
        //itemImage = itemImageHolder.GetComponent<Image>().sprite;

        activeButton.interactable = false;
        dropButton.interactable = false;


        weaponControl = GameObject.Find("ActiveWeapon");

        //StatDisplay
        GameObject statObj = GameObject.Find("ItemInformation");
        statDisplayImage = statObj.GetComponent<Image>();

        GameObject wordObj = GameObject.Find("Stat");
        statDisplayText = wordObj.GetComponent<TextMeshProUGUI>();

        statDisplayImage.color = Color.clear;
        statDisplayText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();

        HighligtActive();

        SlotBecomeEmpty();

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
