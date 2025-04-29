using UnityEngine;

namespace MyBird
{
    //게임의 전체 플로우를 관리하는 클래스
    //대기 -> 이동 -> 죽음(결과처리)
    //static 처리 : 싱글톤 클래스, 정적(static) 변수
    public class GameManager : MonoBehaviour
    {
        
        #region Property
        //정적(static) 변수
        //이동 시작 체크
        public static bool IsStart { get; set; }
        //죽음 체크
        public static bool IsDeath { get; set; }

        //게임 스코어
        public static int Score { get; set; }

        //최고 스코어
        public static int BestScore { get; set; }
        #endregion

        private void Start()
        {
            //최고 점수 가져오기,
            BestScore = PlayerPrefs.GetInt("BestScore", 0);
            //초기화
            IsStart = false;
            IsDeath = false;
            Score = 0;
        }

    }

}
