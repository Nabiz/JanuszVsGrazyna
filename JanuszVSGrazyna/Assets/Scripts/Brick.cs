using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private float offset = 0.5f;
    private int shift = 0;
    public bool isFalling = false;
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(shift * offset, transform.position.y, transform.position.z);

        if(Mathf.Abs(transform.position.x) >= offset*3 && !isFalling)
        {
            StartCoroutine(MakeFalling());
            /*
            isFalling = true;
            if (!gameObject.GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
            }*/
        }
    }
    IEnumerator MakeFalling()
    {
        yield return new WaitForSeconds(0.2f);
        if (Mathf.Abs(transform.position.x) >= offset * 3 && !isFalling)
        {
            isFalling = true;
            if (!gameObject.GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shift += other.gameObject.GetComponent<PlayerController>().attackDirection;
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            if(GetComponent<Rigidbody>() == null)
                Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("GameOver"))
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            explosion.Play();
            GetComponent<AudioSource>().Play();
            gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
    }
}
