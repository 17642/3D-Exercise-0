using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaneEnding : MonoBehaviour
{
    public float displayImageDuration = 1f;
    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackGroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource CaughtAudio;

    bool m_hasAudioPlayed;
    // Start is called before the first frame update
    bool m_IsPlayerAtExit = false;
    bool m_IsPlayerCaught = false;
    float m_Timer;
    void Update()
    {

        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false,exitAudio);
        }else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackGroundImageCanvasGroup,true,CaughtAudio);
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    void EndLevel(CanvasGroup ImageCanvasGroup, bool doRestart,AudioSource audio)
    {
        m_Timer += Time.deltaTime;
        ImageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (!m_hasAudioPlayed)
        {
            m_hasAudioPlayed = true;

            audio.Play();
        }
        if (m_Timer > fadeDuration+displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);//씬 다시 가져오기
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
