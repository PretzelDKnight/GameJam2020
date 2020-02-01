using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

     [SerializeField] GameObject titleObject;
     [SerializeField] GameObject playButtonObject;
     [SerializeField] GameObject infoObject;
     [SerializeField] Animation solidColorAnim;

    public void PlayClicked()
    {
        titleObject.SetActive(false);
        infoObject.SetActive(true);
        solidColorAnim.Play();
        playButtonObject.SetActive(false);
        StartCoroutine(MoveToGame());
    }

    IEnumerator MoveToGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
