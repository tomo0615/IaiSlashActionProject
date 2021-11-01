using TMPro;
using UnityEngine;

namespace Game.Score
{
    public class ScoreView : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;

        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            scoreText.text = "0";
        }

        public void ViewScoreText(int value)
        {
            scoreText.text = value.ToString();
        }
    }
}
