using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ������ �ƴ� Ŭ���� ��� �������� ���� �̸� �տ� m_�� �ٿ��� ǥ���� �� �ִ�.
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

        //����/ ���� �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();//������ ���͸� ǥ��ȭ�Ѵ�(���⿡ ���� ������ �ӵ��� �����̰�)

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);//�� �Է��� 0�� ���� ������ �Է��� bool Ȱ��ȭ

        bool isWalking = hasHorizontalInput || hasVerticalInput;// �����̵� �����̵� �����̰� ������ isWalking = true

        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);//desiredForward = ĳ���� ȸ�� ����. -> ������ �������� ȸ���ӵ�*deltatime��ŭ �̵�

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

    private void OnAnimatorMove()//�ִϸ����� �����϶� �Լ�?
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        //Rigidbody�� ���� ��ġ���� m_Movement*�ִϸ����� �������� ������ŭ ���ؼ� ���Ѵ�. -> ĳ���Ͱ� ������ �����̴� ��ŭ �̵���Ŵ.

        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
