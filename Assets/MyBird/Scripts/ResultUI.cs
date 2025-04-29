using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace MyBird
{
    //게임 결과 보여주기: 베스트 스코어, 스코어 보여주고 다시하기, 메뉴가기 버튼 기능 구현
    public class ResultUI : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string sceneToLoad = "Title";

        public TextMeshProUGUI score;
        public TextMeshProUGUI bestScore;
        public TextMeshProUGUI newText;
        #endregion

        private void OnEnable()
        {
            newText.text = "";
            //GameManager.BestScore와 GameManager.Score 비교
            if (GameManager.Score > GameManager.BestScore)
            {
                //최고점수 갱신
                GameManager.BestScore = GameManager.Score;
                //파일 저장
                PlayerPrefs.SetInt("BestScore", GameManager.Score);
                newText.text = "NEW";
            }

            bestScore.text = GameManager.BestScore.ToString();
            score.text = GameManager.Score.ToString();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Retry();
            }
        }


        //다시하기
        public void Retry()
        {
            //현재 씬 다시 불러오기
            fader.FadeTo(SceneManager.GetActiveScene().name);
        }

        //메뉴 가기 버튼
        public void Menu()
        {
            fader.FadeTo(sceneToLoad);
        }
    }

}
