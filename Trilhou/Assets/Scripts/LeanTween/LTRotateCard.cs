using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LTRotateCard : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] SOInteger cardsCount;

    MeshRenderer mat1;
    MeshRenderer mat2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mat1 = transform.GetChild(0).GetComponent<MeshRenderer>();
        mat2 = transform.GetChild(1).GetComponent<MeshRenderer>();
    }
    private void OnMouseDown()
    {
        mat1.material.color = Color.green;
        mat2.material.color = Color.green;
        cardsCount.value++;
        if (cardsCount.value == 7)
        {
            StartCoroutine(NextScene());
        }
        LeanTween.rotateY(gameObject, 180, 2);
        audioSource.Play();
    }  

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
