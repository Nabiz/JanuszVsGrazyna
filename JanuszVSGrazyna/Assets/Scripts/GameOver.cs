using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool gameOver = false;
    private Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        characterAnimator.SetTrigger("Sexy_Dance");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            characterAnimator.SetBool("Death_b", true);
            gameOver = true;
        }
    }
}
