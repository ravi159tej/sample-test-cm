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



    [SerializeField] private Card firstCard;
    [SerializeField] private Card secondCard;

    [HideInInspector]public List<Card> Cards = new List<Card>();
    
    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private List<Sprite> dummySprites;

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStarted += SetCards;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnLevelStarted -= SetCards;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetCards();
    }

    public void SetCards()
    {
        foreach (Sprite spr in sprites)
            dummySprites.Add(spr);

            int pair = 0;
        int randomSprite = 0;
        for (int i = 0; i < Cards.Count; i++)
        {

            if (pair == 0)
            {
                randomSprite = Random.Range(0, dummySprites.Count);
            }

            Cards[i].setCardSprite(dummySprites[randomSprite]);
            pair++;
            if (pair == 2)
            {
                dummySprites.RemoveAt(randomSprite);
                pair = 0;
            }

        }
        dummySprites.Clear();
        Shuffle();
        Invoke(nameof(HideCards), 1.5f);
    }
    private void Shuffle()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (var card in Cards)
            positions.Add(card.transform.position);
        for (int i = 0; i < positions.Count; i++)
        {
            int randomIndex = Random.Range(i, positions.Count);
            (positions[i], positions[randomIndex]) = (positions[randomIndex], positions[i]);
        }

        for (int i = 0; i < Cards.Count; i++)
            Cards[i].transform.position = positions[i];
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
            GameManager.Instance.AddMatches();
            GameManager.Instance.AddTurns();
            firstCard.PlayEffects();
            secondCard.PlayEffects();
            if(GameManager.Instance.Matches() == Cards.Count / 2)
            {
                GameManager.Instance.LevelCompleted();
                Cards.Clear();
            }
        }
        else
        {
            GameManager.Instance.AddTurns();
            Debug.Log("Unmatch");
            firstCard.Undoflip();
            secondCard.Undoflip();
        }

        firstCard = null;
        secondCard = null;

    }

    private void HideCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Undoflip();
        }
    }

}
