using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    private bool isFlipped = false;
    // Start is called before the first frame update

    public void setCardSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }
    void Awake()
    {
        CardsManager.Instance.cards.Add(this);
    }

    private void OnMouseDown()
    {
        if (isFlipped)
            return;
        Debug.Log("isFlipped");
        transform.DORotate(new Vector3(0,0,360), 0.5f).OnComplete(() => {
            isFlipped = true;
            CardsManager.Instance.CheckForMatch(this.renderer);
        });
    }

    public void Unflip()
    {
        transform.DORotate(new Vector3(0, 0, -180), 0.5f).OnComplete(() => isFlipped = false);
    }
}
