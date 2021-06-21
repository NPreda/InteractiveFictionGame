using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUiPlayerHand : IUiCardPile
{
    List<IUiCard> Cards { get; }
    Action<IUiCard> OnCardPlayed { get; set; }
    Action<IUiCard> OnCardSelected { get; set; }
    IUiCard selectedCard { get; }
    void Unselect();
    GameObject GetPrefab();
    void SelectCard(IUiCard uiCard);
    void UnselectCard();
    void DiscardAllCards();
    void DrawCard(Card card);
    void ExpendSelectedCard();
    void UpdateCounters(int deckCount, int graveCount);
    int ReturnHandCount();
}
