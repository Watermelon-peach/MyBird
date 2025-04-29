using UnityEngine;

namespace MyBird
{
    //배경 이동 롤링 구현
    public class GroundMove : MonoBehaviour
    {
        #region Variables
        //스크롤 이동 속도
        [SerializeField] private float moveSpeed = 5f;
        #endregion

        //배경을 왼쪽으로 이동시킨다,
        //배경의 x좌표가 -8.4보다 같거나 작으면 제자리로 놓는다

        private void Update()
        {
            Move();
        }

        void Move()
        {
            if (!GameManager.IsStart) return;

            float speed = (GameManager.IsDeath) ? moveSpeed / 4f : moveSpeed;

            //왼쪽으로 moveSpeed만큼 이동
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

            //배경 롤링
            if (transform.localPosition.x <= -8.4f)
            {
                transform.localPosition = Vector2.zero;
            }
        }
    }

}
