using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class InventoryUI : MonoBehaviour, IDisplayable
{
    IDisplayableStorer inventory;
    [SerializeField] GameObject container;
    [SerializeField] Image backgroundImage;
    [SerializeField] int imageSize;
    [SerializeField, Range(1, 50)] int gap = 5;
    [SerializeField, Range(1, 50)] int padding = 5;

    bool updateDisplay;

    private void Update()
    {
        //Destroying gameobjects can't be dont from onvalidate
        //this is the work around
        if (updateDisplay)
        {
            UpdateDisplay();
            updateDisplay = false;
        }
        
    }

    public void Initialize(GameObject container, Image backgroundImage, int imageSize, int gap)
    {
        this.container = container;
        this.backgroundImage = backgroundImage;
        this.imageSize = imageSize;
        this.gap = gap;
    }

    public void SetImageWidth(int width)
    {
        imageSize = width;
    }

    public void SetGap(int gap)
    {
        this.gap = gap;
    }

    public void ToggleDisplay(bool isDisplaying)
    {
        gameObject.SetActive(isDisplaying);
    }

    public void UpdateDisplay()
    {
        if (inventory == null)
            return;

        int columns = inventory.GetColumns();
        int rows = inventory.GetRows();


        //imageSize + gap is the width for every column except the last, which is just imageSize
        //similar logic applies for height
        int containerWidth = (imageSize + gap) * columns - gap;
        int containerHeight = (imageSize + gap) * rows - gap;

        ResetContainer(containerWidth, containerHeight);

        for (int x = 0; x < columns; x++)
        {

            for (int y = 0; y < rows; y++)
            {
                AddInventorySlot(x, y, containerWidth, containerHeight);
            }
        }
    }


    void AddInventorySlot(int column, int row, float containerWidth, float containerHeight)
    {
        //initialize the background image
        Image backgroundImg = InitializeImage(backgroundImage, container, imageSize);
        RectTransform backgroundTransform = backgroundImg.GetComponent<RectTransform>();

        //calculate the image position
        Vector3 position = CalculateImagePosition(column, row, containerWidth, containerHeight);

        //set the position
        backgroundTransform.localPosition = position;

        //get the item in the inventory slot and display it's image
        AddItemImage(column, row, backgroundTransform);

    }

    Vector3 CalculateImagePosition(int column, int row, float containerWidth, float containerHeight)
    {

        //calculate their new position
        float x = -1 * containerWidth / 2; //left most position
        x += (imageSize + gap) * column; //offset from the furthest left

        float y = -1 * containerHeight / 2; //bottom most position
        y += (imageSize + gap) * row; //offset from bottom

        return new Vector3(x, y, 0);

    }

    Image InitializeImage(Image imagePrefab, GameObject parent, float size)
    {
        //instantiate a clone of imagePrefab as a child of container
        Image img = Instantiate(imagePrefab, parent.transform);

        //set the anchors to 0 so that the position is calculated from the bottom left corner of the container
        RectTransform imgTransform = img.GetComponent<RectTransform>();
        imgTransform.anchorMax = Vector2.zero;
        imgTransform.anchorMin = Vector2.zero;

        //set the pivot to 0 so that the position is calculated from the bottom left corner of the image
        imgTransform.pivot = Vector2.zero;

        //set the image width / height (square)
        imgTransform.sizeDelta = new Vector2(size, size);

        return img;
    }

    void AddItemImage(int column, int row, RectTransform backgroundTransform)
    {

        //get the child item
        IStorable item = inventory.GetItem(column, row);

        if (item == null)
            return;

        //get it's image
        Image itemImg = item.GetDisplayImage();

        if (itemImg == null)
            return;

        //add the image
        Image instantiated = Instantiate(itemImg, backgroundTransform);

        //center the image and put it infront of the background
        instantiated.transform.localPosition = new Vector3(0, 0, 1);

        //scale the image to the right size
        RectTransform itemTransform = instantiated.GetComponent<RectTransform>();
        itemTransform.sizeDelta = backgroundTransform.sizeDelta - new Vector2(padding, padding);
    }

    void ResetContainer(float width, float height)
    {

        //set the container's size
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);


        //remove all the container's children
        Transform[] children = container.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].gameObject.Equals(container))
                continue;

            if(Application.isPlaying)
                Destroy(children[i].gameObject);

            else
                DestroyImmediate(children[i].gameObject);
        }

    }

    void GetInventory()
    {


        Component[] components = gameObject.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component is IDisplayableStorer)
            {
                inventory = (IDisplayableStorer)component;
                return;
            }
        }

        inventory = null;
        Debug.LogWarning($"Game Object \"{name}\" has an InventoryUI component but is missing a component of type IDisplayableStorer!");
    }

     

    private void OnValidate()
    {
        GetInventory();
        updateDisplay = true;
    }
}
