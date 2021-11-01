using UniRx;
using UnityEngine;

namespace Game.Score
{
    public class ScorePresenter : MonoBehaviour
    {
        private ScoreModel _scoreModel = null;

        [SerializeField]
        private ScoreView scoreView = null;
    
        private void Awake()
        {
            _scoreModel = new ScoreModel();
        }
        private void Start()
        {
            //スコア更新購読
            _scoreModel.Scoring
                .Where(value => value > 0)
                .Subscribe(scoreView.ViewScoreText)
                .AddTo(gameObject);
        }

        public void OnChangeScore(int value)
        {
            _scoreModel.UpdateScoreValue(value);
        }
        
        //最終スコア取得用
        public int GetScoreValue()
        {
            return _scoreModel.Scoring.Value;
        }
    }
}