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
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResetSaveData();
            }
        }

        public void Play()
        {
            fader.FadeTo(sceneToLoad);
        }

        void ResetSaveData()
        {
            if (!isCheat)
                return;

            PlayerPrefs.DeleteAll();
            Debug.Log("초기화 됐지롱");
        }
    }

}
