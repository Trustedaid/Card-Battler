using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;


public class Card : MonoBehaviour
{
    public CardScriptableObject cardSO;

    public int currentHealth;
    public int attackPower;
    public int manaCost;

    public TMP_Text healthText, attackText, costText, nameText, actionDescriptionText, loreText;

    public Image characterArt, bgArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;

    public float moveSpeed = 5f;
    public float rotateSpeed = 540f;

    public bool inHand;
    public int handPosition;

    private HandController theHC;


    // Start is called before the first frame update
    void Start()
    {
        SetUpCard();

        theHC = FindObjectOfType<HandController>();
    }

    public void SetUpCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;

        healthText.text = currentHealth.ToString();
        attackText.text = attackPower.ToString();
        costText.text = manaCost.ToString();

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        characterArt.sprite = cardSO.characterSprite;
        bgArt.sprite = cardSO.bgSprite;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,targetPoint,moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
    }

    private void OnMouseOver()
    {
        if (inHand)
        {
            MoveToPoint(theHC.cardPositions[handPosition] + new Vector3(0f, 1f, 0.5f), quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        if (inHand)
        {
            MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
        }
    }
}