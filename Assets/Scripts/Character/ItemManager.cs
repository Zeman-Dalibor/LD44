﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int count;
    public string name;
    public string description;
}

public class ItemManager : MonoBehaviour
{
    public Dictionary<string, Item> owned_items;
    // Start is called before the first frame update
    void Start()
    {
        owned_items = new Dictionary<string, Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem(Item item)
    {
        if (owned_items.ContainsKey(item.name))
        {
            owned_items[item.name].count++;
        }
        else
        {
            owned_items.Add(item.name, item);
        }
    }
}
