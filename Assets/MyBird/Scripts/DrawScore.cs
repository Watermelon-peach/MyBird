using UnityEngine;
using TMPro;

namespace MyBird
{  
    //게임 스코어를 그린다
    public class DrawScore : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI scoreText;
        #endregion

        // Update is called once per frame
        void Update()
        {
            scoreText.text = GameManager.Score.ToString();
        }
    }

}
