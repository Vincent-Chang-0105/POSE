using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public string description;
        public Sprite icon;
    }

    public ShopItem[] shopItems; // Array to hold item data
    public GameObject shopItemPrefab; // The prefab for each item
    public CartManager cartManager; // Reference to CartManager


    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
   
            foreach (ShopItem item in shopItems)
            {
                GameObject newItem = Instantiate(shopItemPrefab);

                newItem.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
                newItem.transform.Find("Name").GetComponent<Text>().text = item.itemName;
                newItem.transform.Find("Description").GetComponent<Text>().text = item.description;

                Button addToCartButton = newItem.transform.Find("AddToCartButton").GetComponent<Button>();
                addToCartButton.onClick.AddListener(() => cartManager.AddToCart(item));

                
            }
       
    }
}