using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The Player Delivery Inventory that keeps track of, and manages all Delivery Items.
/// </summary>
public class QSM_ObjectiveTracker
{
    private readonly Dictionary<string, Item> _items;

    /// <summary>
    /// Returns a boolean indicating whether or not all objectives are complete.
    /// </summary>
    /// <returns></returns>
    public bool AreObjectivesComplete => _items.All(item => item.Value.CurrentAmount >= item.Value.AmountNeeded);

    public QSM_ObjectiveTracker(List<QSM_Objective> items)
    {
        _items = new Dictionary<string, Item>();
        items.ForEach(item => Additem(item.Name, item.RequiredAmount, item.UIReference));
    }

    /// <summary>
    /// Adds an amount of a certain item to the item Inventory.
    /// </summary>
    /// <param name="item">The name of the item.</param>
    /// <param name="amount">The amount to add.</param>
    /// <returns>The total amount of the specific item, in the item Inventory. Returns -1 if the item was not found.</returns>
    public int Additem(string item, int amount = 1)
    {
        if (!_items.ContainsKey(item))
            return -1;
        return Additem(item, amount, null);
    }

    /// <summary>
    /// Adds an amount of a certain item to the item Inventory. Also enables the uiReference GameObject.
    /// </summary>
    /// <param name="item">The name of the item.</param>
    /// <param name="amount">The amount to add.</param>
    /// <param name="uiReference">The UI representation of the item.</param>
    /// <returns>The total amount of the specific item, in the item Inventory.</returns>
    private int Additem(string item, int amount, GameObject uiReference)
    {
        if (_items.ContainsKey(item))
            _items[item].CurrentAmount += amount;
        else
        {
            _items.Add(item, new Item(amount, uiReference));
            uiReference.SetActive(true);
        }

        return _items[item].CurrentAmount;
    }

    /// <summary>
    /// Removes a certain amount of an item from the item Inventory.
    /// </summary>
    /// <param name="item">The name of the item.</param>
    /// <param name="amount">The amount to remove.</param>
    /// <returns>The remaining amount of the item. Returns -1 if the item wasn't found.</returns>
    public int Removeitem(string item, int amount = 1)
    {
        if (_items.ContainsKey(item))
        {
            _items[item].CurrentAmount -= amount;

            if (_items[item].CurrentAmount <= 0)
            {
                _items[item].CurrentAmount = 0;
            }

            return _items[item].CurrentAmount;
        }

        return -1;
    }

    /// <summary>
    /// Gets the amount of a certain item. Returns -1, if the item doesn't exists.
    /// </summary>
    /// <param name="item">The item to get the amount of.</param>
    /// <returns>The amount of the specified item in the Player item Inventory.</returns>
    public int GetAmount(string item)
    {
        if (!_items.ContainsKey(item))
            return -1;
        return _items[item].AmountNeeded;
    }

    /// <summary>
    /// Returns the Gameobject representing the related UI element.
    /// </summary>
    /// <param name="item">The name of the item.</param>
    /// <returns>The Gameobject representing the related UI element, or null, if none was found.</returns>
    public GameObject GetUIReference(string item)
    {
        if (!_items.ContainsKey(item))
            return null;
        return _items[item].UIReference;
    }

    /// <summary>
    /// Resets all progress back to zero.
    /// </summary>
    public void Reset()
    {
        foreach (var keyValueItem in _items)
            keyValueItem.Value.CurrentAmount = 0;
    }

    private class Item
    {
        public int CurrentAmount { get; set; }
        public int AmountNeeded { get; }
        public GameObject UIReference { get; set; }

        public bool HasUIReference => UIReference != null;

        public Item() : this(1, null) { }

        public Item(int amount) : this(amount, null) { }

        public Item(GameObject uiReference) : this(1, uiReference) { }

        public Item(int amount, GameObject uiReference)
        {
            AmountNeeded = amount;
            UIReference = uiReference;
        }
    }
}
