using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManager : MonoBehaviour
{
    public List<ShopUIManager.ShopItem> cartItems = new List<ShopUIManager.ShopItem>();

    public void AddToCart(ShopUIManager.ShopItem item)
    {
        cartItems.Add(item);
        Debug.Log($"Added {item.itemName} to the cart.");
    }

    public void RemoveFromCart(ShopUIManager.ShopItem item)
    {
        if (cartItems.Contains(item))
        {
            cartItems.Remove(item);
            Debug.Log($"Removed {item.itemName} from the cart.");
        }
    }
}