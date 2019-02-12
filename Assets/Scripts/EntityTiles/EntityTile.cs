using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityTile : MonoBehaviour
{
    public EntityType EntityName { get; set; }
}


public enum EntityType
{
    Laser,
    Rockfall,
    Spikes,
    Tnt,
    Gem
}