using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Brick", fileName = "Brick")]
public class Brick_GO : ScriptableObject
{
    
    public Sprite[] sprites;
    [Range(0, 1)]
    public float dropChance;
    public int healthStart;
    public int points;
}
