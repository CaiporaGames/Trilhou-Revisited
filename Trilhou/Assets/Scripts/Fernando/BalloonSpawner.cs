using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Timoteo
{
    public class BalloonSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] points;
        [SerializeField] GameObject balloon;
        [SerializeField] float maxTimeBetweenBalloons;
        [SerializeField] SOGeneralVariables variables;
        [SerializeField] SOBalloonOptions options;
        [SerializeField] GameObject messagePanel;

        float timeBetweenBalloons;
        bool runOnce = true;

        private void Start()
        {
            timeBetweenBalloons = maxTimeBetweenBalloons;
            SpawnObjects();
            options.AddWordsToBallonOptions();
            variables.RestartPlayerHearts();
        }

        private void Update()
        {
            timeBetweenBalloons -= Time.deltaTime;
            if (!variables.gamePaused)
            {
                SpawnObjects();
            }
            if (options.ballonsCounter == 0 && runOnce)
            {
                runOnce = false;
                //PointsBar.Instance.AddPointsToBar();
                StartCoroutine(NextScene());
            }
            if (variables.playerHearts <= 0)
            {
                messagePanel.SetActive(true);
                messagePanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    "Oh, voc? n?o acertou. N?o desista, tente novamente!";
                StartCoroutine(ReloadScene());
            }
           
        }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator NextScene()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        void SpawnObjects()
        {
            if (timeBetweenBalloons <= 0)
            {
                timeBetweenBalloons = maxTimeBetweenBalloons;

                for (int i = 0; i < points.Length; i++)
                {
                    Instantiate(balloon, points[i].transform.position, Quaternion.identity);
                }
            }
        }

        public void UnpauseGame()
        {
            variables.gamePaused = true;
            LeanTween.resumeAll();
        }
    }
}