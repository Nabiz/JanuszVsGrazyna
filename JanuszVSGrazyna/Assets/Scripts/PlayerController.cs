using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode attackKey;
    public int attackDirection = 1;
    private State currnetState;
    private enum State
    {
        Default,
        Attack,
        Recoll,
        Cooldown
    }
    private AudioSource collisionAudioSource;

    private float attackSpeed = 10f;
    private float verticalSpeed = 2f;
    private float cooldownTime = 0.5f;

    public float defaultPosX = 6f;
    private float maxHeight = 12f, minHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        collisionAudioSource = GetComponent<AudioSource>();
        currnetState = State.Default;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= maxHeight)
        {
            verticalSpeed = -Mathf.Abs(verticalSpeed);
        }
        else if (transform.position.y <= minHeight)
        {
            verticalSpeed = Mathf.Abs(verticalSpeed);
        }
        if(Mathf.Abs(transform.position.x) > Mathf.Abs(defaultPosX) && currnetState == State.Recoll)
        {
            transform.position = new Vector3(defaultPosX, transform.position.y, transform.position.z);
            currnetState = State.Cooldown;
            Invoke("SetStateToDefault", cooldownTime);
        }


        if(currnetState == State.Default || currnetState == State.Cooldown)
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
        }
        else if (currnetState == State.Attack)
        {
            transform.Translate(Vector3.right * attackSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * attackSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(attackKey) && currnetState == State.Default)
        {
            currnetState = State.Attack;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Tower") || (other.gameObject.CompareTag("Brick"))) && currnetState == State.Attack)
        {
            currnetState = State.Recoll;
            collisionAudioSource.Play();
        }
    }

    private void SetStateToDefault()
    {
        currnetState = State.Default;
    }

    public void ChangeSpeed(int level)
    {
        switch (level)
        {
            case 0:
                verticalSpeed = 0;
                break;
            case 1:
                verticalSpeed = Mathf.Sign(verticalSpeed) * 5f;
                break;
            case 2:
                verticalSpeed = Mathf.Sign(verticalSpeed) * 10f;
                break;
            case 3:
                verticalSpeed = Mathf.Sign(verticalSpeed) * 15f;
                break;
        }
        if(verticalSpeed!=0)
            cooldownTime = 1f / Mathf.Abs(verticalSpeed);
    }

    public void FreezMovment()
    {
        attackSpeed = 0;
        verticalSpeed = 0;
    }
    public void EnableAttack()
    {
        currnetState = State.Attack;
        attackSpeed = 10f;
    }
}
