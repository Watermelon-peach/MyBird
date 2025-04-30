using UnityEngine;

namespace MyBird
{
    public class Title : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string sceneToLoad = "PlayScene";

        //치트키
        [SerializeField] private bool isCheat = false;
        #endregion

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResetSaveData();
            }
#endif
        }

        public void Play()
        {
            fader.FadeTo(sceneToLoad);
        }

        public void Quit()
        {
            Application.Quit();
        }

        void ResetSaveData()
        {
            if (!isCheat)
                return;

            PlayerPrefs.DeleteAll();
            //Debug.Log("초기화 됐지롱");
        }
    }

}
