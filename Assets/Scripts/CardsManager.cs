using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardsManager : MonoBehaviour
{
    private static CardsManager instance;
    public static CardsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardsManager>();
            }

            return instance;
        }
    }

    private Card firstCard;
    private Card secondCard;

    public List<Card> cards = new List<Card>();
    
    [SerializeField]
    private List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {
        SetCards();
        Invoke(nameof(HideCards), 1.5f);
    }

    private void SetCards()
    {
        int pair = 0;
        int randomSprite = 0;
        int randomCard = 0;
        for (int i = 0; i < cards.Count; i++)
        {

            if (pair == 0)
            {
                randomSprite = Random.Range(0, sprites.Count);
            }

            randomCard = Random.Range(0, cards.Count);
            cards[i].setCardSprite(sprites[randomSprite]);
            pair++;
            if (pair == 2)
            {
                sprites.RemoveAt(randomSprite);
                pair = 0;
            }

        }
        Shuffle();
    }
    private void Shuffle()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (var card in cards)
            positions.Add(card.transform.position);
        for (int i = 0; i < positions.Count; i++)
        {
            int randomIndex = Random.Range(i, positions.Count);
            (positions[i], positions[randomIndex]) = (positions[randomIndex], positions[i]);
        }

        for (int i = 0; i < cards.Count; i++)
            cards[i].transform.position = positions[i];
    }

    public void CheckForMatch(Card card )
    {
        if (firstCard == null)
        {
            firstCard = card;
            return;
        }
        secondCard = card;

        if (firstCard.cardName == secondCard.cardName)
        {
            Debug.Log("MAtch");
            firstCard.PlayEffects();
            secondCard.PlayEffects();
        }
        else
        {
            Debug.Log("Unmatch");
            firstCard.Undoflip();
            secondCard.Undoflip();
        }
        firstCard = null;
        secondCard = null;
    }

    private void HideCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Undoflip();
        }
    }
}
