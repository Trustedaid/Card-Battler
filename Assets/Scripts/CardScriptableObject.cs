using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)] // creates Asset menu
public class CardScriptableObject : ScriptableObject
{
   public string cardName;
   [TextArea] public string actionDescription, cardLore; // text area creates a bigger textAreaBox to write strings
   
   public int currentHealth, attackPower, manaCost;
   
   public Sprite characterSprite, bgSprite;
}
