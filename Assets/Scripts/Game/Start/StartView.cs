using TMPro;
using UnityEngine;

namespace Game.Start
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText;
        
        public void ViewStartSign()
        {
            startText.enabled = true;
        }
    }
}
