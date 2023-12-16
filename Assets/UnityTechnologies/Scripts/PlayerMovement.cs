using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 공용이 아닌 클래스 멤버 변수에는 변수 이름 앞에 m_을 붙여서 표시할 수 있다.
    Animator m_Animator;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    public float turnSpeed=20f;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //수직/ 수평 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();//움직임 벡터를 표준화한다(방향에 따라 일정한 속도로 움직이게)

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);//각 입력이 0과 같지 않으면 입력중 bool 활성화

        bool isWalking = hasHorizontalInput || hasVerticalInput;// 수직이든 수평이든 움직이고 있으면 isWalking = true

        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);//desiredForward = 캐릭터 회전 벡터. -> 전방을 기준으로 회전속도*deltatime만큼 이동

        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            if(m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
        }

    }

    private void OnAnimatorMove()//애니메이터 움직일때 함수?
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        //Rigidbody를 현재 위치에서 m_Movement*애니메이터 움직임의 배율만큼 곱해서 더한다. -> 캐릭터가 실제로 움직이는 만큼 이동시킴.

        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
