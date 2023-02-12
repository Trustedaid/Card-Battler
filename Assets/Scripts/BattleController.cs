using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    private void Awake()
    {
        instance = this;
    }

    public int startingMana = 4, maxMana = 12;
    public int playerMana;
    private int currentPlayerMaxMana;

    public int startingCardsAmount = 5;
    public int cardsToDrawPerTurn = 1;


    public enum TurnOrder
    {
        playerActive,
        playerCardAttacks,
        enemyActive,
        enemyCardAttacks
    }

    public TurnOrder currentPhase;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayerMaxMana = startingMana;
        FillPlayerMana();
        DeckController.instance.DrawMultipleCards((startingCardsAmount));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AdvanceTurn();
        }
    }

    public void SpendPlayerMana(int amountToSpend)
    {
        playerMana -= amountToSpend;

        if (playerMana < 0)
        {
            playerMana = 0;
        }

        UIController.instance.SetPlayerManaText(playerMana);
    }

    public void FillPlayerMana()
    {
        // playerMana = startingMana;
        playerMana = currentPlayerMaxMana;
        UIController.instance.SetPlayerManaText(playerMana);
    }

    public void AdvanceTurn()
    {
        currentPhase++;
        if ((int)currentPhase >= Enum.GetValues(typeof(TurnOrder)).Length)
        {
            currentPhase = 0;
        }

        switch (currentPhase)
        {
            case TurnOrder.playerActive:
                UIController.instance.endTurnButton.SetActive(true);
                UIController.instance.drawCardButton.SetActive(true);
                
                if (currentPlayerMaxMana < maxMana)
                {
                    currentPlayerMaxMana++;
                }
                FillPlayerMana();
                
                DeckController.instance.DrawMultipleCards(cardsToDrawPerTurn);
                
                break;
            
            case TurnOrder.playerCardAttacks:
                Debug.Log("skipping player card attacks");
                // AdvanceTurn();
                break;
            
            case TurnOrder.enemyActive:
                Debug.Log("skipping enemy actions");
                AdvanceTurn();
                break;
            
            case TurnOrder.enemyCardAttacks:
                Debug.Log("skipping enemy card attacks");
                AdvanceTurn();
                break;
        }
    }

    public void EndPlayerTurn()
    {
        UIController.instance.endTurnButton.SetActive(false);
        UIController.instance.drawCardButton.SetActive(false);

        AdvanceTurn();
    }
}