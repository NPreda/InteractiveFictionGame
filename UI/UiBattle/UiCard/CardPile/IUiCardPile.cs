using System;

//A pile of cards
public interface IUiCardPile
{
    Action<IUiCard[]> OnPileChanged { get; set; }
    void AddCard(IUiCard uiCard);
    void RemoveCard(IUiCard uiCard);
}
