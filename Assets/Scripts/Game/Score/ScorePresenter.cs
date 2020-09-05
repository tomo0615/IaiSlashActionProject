using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour ,IScoreUpdatable
{
    private ScoreModel _scoreModel = null;

    [SerializeField]
    private ScoreView _scoreView = null;

    [SerializeField]
    private LevelPresenter _levelPresenter = null;

    private void Awake()
    {
        _scoreModel = new ScoreModel();
    }
    private void Start()
    {
        //スコア更新購読
        _scoreModel.Scoring
            .Where(value => value > 0)
            .Subscribe(_scoreView.ViewScoreText)
            .AddTo(gameObject);

        //スコア参照して10の位の変化を購読
        _scoreModel.Scoring
            .Where(value => (value / 10) == _levelPresenter.GetCurrentLevel())
            .Subscribe(_ =>
            { 
                _levelPresenter.OnChangeLevel(1);
            });
    }

    public void OnChangeScore(int value)
    {
        _scoreModel.UpdateScoreValue(value);
    }
    
    public int GetScoreValue()
    {
        return _scoreModel.Scoring.Value;
    }
}