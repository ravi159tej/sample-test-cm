using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    private bool isFlipped = true;
    public string cardName = "";
    [SerializeField] private GameObject particleEffect;

    void Awake()
    {
        CardsManager.Instance.cards.Add(this);
    }
    public void setCardSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
        cardName = renderer.sprite.name;
    }

    private void OnMouseDown()
    {
        if (isFlipped)
            return;
        Debug.Log("isFlipped");
        transform.DORotate(new Vector3(0,0,0), 0.5f).OnComplete(() => {
            isFlipped = true;
            CardsManager.Instance.CheckForMatch(this);
        });
        transform.DOLocalMoveY(0.3f, 0.25f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo);
    }

    public void Undoflip()
    {
        transform.DORotate(new Vector3(0, 0, 180), 0.5f).OnComplete(() => isFlipped = false);
        transform.DOLocalMoveY(0.3f, 0.25f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo);
    }

    public void PlayEffects()
    {
        particleEffect.transform.parent = null;
        particleEffect.SetActive(true);
        Destroy(particleEffect, 2f);
        Destroy(this.gameObject,0.3f);
    }
}
