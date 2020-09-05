using UnityEngine;
using DG.Tweening;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private float punchTime = 0.5f;

    [SerializeField]
    private float punchScale = 1.5f;

    private bool isPunch = false;

    private RectTransform _rectTransform;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreText.text = "".ToString();
    }

    public void ViewScoreText(int value)
    {
        scoreText.text = value.ToString();

        PunchText();
    }

    private void PunchText()
    {
        //重複して呼ばれないように
        if (isPunch) return;
        isPunch = true;

        transform.DOScale(_rectTransform.localScale * punchScale, punchTime / 2)
            .OnComplete(() =>
            {
                transform.DOScale(_rectTransform.localScale / punchScale, punchTime / 2)
                .OnComplete(() =>
                {
                    isPunch = false;
                });
            });
    }
}
