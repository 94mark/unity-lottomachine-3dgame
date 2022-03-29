using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    public GameObject[] ball;
    int ballNum;
    public TextMeshPro billBoard;
    bool ready;
    public Transform exit;

    List<int> winningNum = new List<int>();
    public TextMeshPro[] num;

    public GameObject sound_SetBall;
    public GameObject sound_Ready;
    public AudioSource sound_GetBall;
    public AudioSource sound_Btn;

    private void Start()
    {
        StartCoroutine("StartMachine");
    }

    void SetBall()
    {
        for(int i = 0; i < ball.Length; i++)
        {
            if(!ball[i].activeSelf)
            {
                ball[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator StartMachine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            SetBall();
            ballNum += 1;

            if(ballNum >= 45)
            {
                StopCoroutine("StartMachine");
                billBoard.text = "Ready";
                ready = true;
                sound_SetBall.SetActive(false);
                sound_Ready.SetActive(true);
            }
        }
    }

    public void PressGetBtn()
    {
        sound_Btn.Play();

        if (ready)
        {
            ready = false; //get 버튼 입력 시 한번만 작동
            StartCoroutine("Draw");
            billBoard.gameObject.SetActive(false);
        }
    }

    IEnumerator Draw()
    {
        while(winningNum.Count<6)
        {
            GetBall();
            yield return new WaitForSeconds(2);
        }
    }

    void GetBall()
    {
        int r = Random.Range(0, ball.Length);

        if(winningNum.Contains(r+1))
        {
            GetBall();
            return;
        }

        if(!winningNum.Contains(r+1))
        {
            winningNum.Add(r + 1);
            ball[r].GetComponent<Ball>().StopAllCoroutines();
            ball[r].GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball[r].transform.position = exit.position;
            sound_GetBall.Play();

            for (int i = 0; i < num.Length; i++)
            {
                if(!num[i].gameObject.activeSelf)
                {
                    num[i].text = string.Format("{0}", r + 1);
                    num[i].gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    public void PressResetBtn()
    {
        sound_Btn.Play();
        Invoke("ReStart", 0.5f);
    }

    void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
}
