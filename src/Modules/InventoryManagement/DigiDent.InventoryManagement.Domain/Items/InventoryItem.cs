﻿using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Items;

public class InventoryItem : 
    AggregateRoot, 
    IEntity<InventoryItemId, Guid>
{
    public InventoryItemId Id { get; init; }
    public ItemName Name { get; private set; }
    public ItemCategory Category { get; set; }
    public string Remarks { get; private set; }
    public Quantity Quantity { get; init; }
    
    public ICollection<Request> Requests { get; set; }
        = new List<Request>();
    public ICollection<InventoryAction> InventoryActions { get; set; }
        = new List<InventoryAction>();
    
    public InventoryItem(
        ItemName name,
        ItemCategory category,
        Quantity quantity,
        string remarks = "")
    {
        Id = TypedId.New<InventoryItemId>();
        Name = name;
        Category = category;
        Remarks = remarks;
        Quantity = quantity;
    }
}