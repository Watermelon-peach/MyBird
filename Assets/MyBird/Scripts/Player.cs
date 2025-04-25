using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //점프
        private bool keyJump = false;       //점프 키 인풋 체크
        [SerializeField]private float jumpForce = 5f;       //윗방향으로 주는 힘

        //회전
        private Vector3 birdRotation;
        //위로 올라갈 때 회전 속도
        [SerializeField]private float upRotate = 2.5f;
        //밑으로 내려갈 때 회전 속도
        [SerializeField]private float downRotate = -5f;

        //이동
        //이동속도 - Translate
        [SerializeField]private float moveSpeed = 5f;
        #endregion

        #region Unity Event Method
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //참조
            rb2D = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void Update()
        {
            //인풋처리
            InputBird();

            //버드 회전
            RotateBird();

            //버드 이동
            MoveBird();
        }

        private void FixedUpdate()
        {
            if (keyJump)
            {
                Debug.Log("점프");
                JumpBird();
                keyJump = false;
            }
        }
        #endregion

        #region Custom Method
        //인풋처리
        void InputBird()
        {
            //스페이스키 또는 마우스 왼클릭 입력받기
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
        }

        //버드 점프하기
        void JumpBird()
        {
            //아래쪽에서 위쪽으로 힘을 준다
            //rb2D.AddForce(Vector2.up * (힘));
            rb2D.linearVelocity = Vector2.up * jumpForce;
        }

        //버드 회전하기
        void RotateBird()
        {
            //올라갈 때 최대 + 30도까지 회전 : rotateSpeed = 2.5f;(upRotate)
            //내려갈 때 최소 - 90도까지 회전 : rotateSpeed = 5f;(downRotate)
            float rotateSpeed = 0f;
            if(rb2D.linearVelocity.y > 0f) // 올라갈 때
            {
                rotateSpeed = upRotate;
            }
            else if (rb2D.linearVelocity.y < 0f)    //내려갈 때
            {
                rotateSpeed = downRotate;
            }

            birdRotation = new Vector3(0f, 0f, Mathf.Clamp((birdRotation.z + rotateSpeed), -90, 30));
            transform.eulerAngles = birdRotation;
        }

        //버드 이동하기
        void MoveBird()
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
        }
        #endregion
    }
}
