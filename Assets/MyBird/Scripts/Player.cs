using UnityEngine;
using TMPro;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        //애니메이터
        public Animator animator;
       
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

        //대기
        //아래로 떨어지지 않을만큼의 새를 받히는 힘
        [SerializeField] private float readyForce = 1f;

        //UI
        public GameObject readyUI;
        public GameObject resultUI;
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
            

            if (!GameManager.IsStart)
            {
                //버드 대기
                ReadyBird();
                return;
            }

            //버드 회전
            RotateBird();

            //버드 이동
            MoveBird();
        }

        private void FixedUpdate()
        {
            if (GameManager.IsDeath)
                return;
            if (keyJump)
            {
                Debug.Log("점프");
                JumpBird();
                keyJump = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //collision : 부딛힌 콜라이더 정보를 가지고 있다
            if (collision.gameObject.tag == "Ground")
            {
                DieBird();
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //collision : 부딛힌 콜라이더 정보를 가지고 있다
            if (collision.gameObject.tag == "Point")
            {
                GameManager.Score++;
                
                Debug.Log($"점수 획득: {GameManager.Score}");
            }

            if (collision.gameObject.tag == "Pipe")
            {
                DieBird();
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

            //게임 start
            if (!GameManager.IsStart && keyJump)
            {
                StartMove();
            }
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

        //버드 대기
        void ReadyBird()
        {
            //아래쪽에서 떨어지지 않도록 위쪽으로 힘을 준다
            if (rb2D.linearVelocity.y < 0f)
            {
                rb2D.linearVelocity = Vector2.up * readyForce;
            }
            
        }

        //버드 이동하기
        void MoveBird()
        {
            if (!GameManager.IsStart || GameManager.IsDeath)
                return;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
        }

        //버드 죽음
        void DieBird()
        {
            //두번 죽음 체크
            if (GameManager.IsDeath)
                return;

            GameManager.IsDeath = true;
            animator.enabled = false;
            rb2D.linearVelocity = Vector2.zero;

            //VFX, SFX

            //UI
            resultUI.SetActive(true);
        }

        //버드 이동 시작
        void StartMove()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }
        #endregion
    }
}
