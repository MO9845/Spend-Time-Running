using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Player : MonoBehaviour
{
    public int coins, coinMultiplier;
    private int side;

    public float speed;
    private float originX, targetX, swipeTime;

    private bool swiping, swiped;
    public bool rolling, onGround;

    private string currentAnimation;

    private Rigidbody rigidbody;
    private Animator animator;
    public GameObject hitObject;
    public LoseWidget loseWidget;
    public AudioSource swipe, punch;
    public GameObject respawnCapsule;

    private List<Transform> transforms = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    private List<Rigidbody> rigidbodies = new List<Rigidbody>();

    void Start()
    {
        speed = 5;
        coinMultiplier = 1;
        onGround = true;

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
            if (rigidbody.gameObject != this.gameObject && rigidbody.gameObject.name != "Player End")
            {
                rigidbody.isKinematic = true;

                rigidbodies.Add(rigidbody);

                transforms.Add(rigidbody.transform);
                positions.Add(rigidbody.transform.position);
                rotations.Add(rigidbody.transform.rotation);
            }

        foreach (Collider collider in GetComponentsInChildren<Collider>())
            if (collider.gameObject != this.gameObject && collider.gameObject.name != "Player End")
                collider.enabled = false;

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        string path = Application.persistentDataPath + "/coins.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            coins = (int)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    void Update()
    {
        if (!animator.enabled)
            return;

        transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));

        if (!swiping && transform.position.x != side * 4.7f)
            transform.position = new Vector3(side * 4.7f, transform.position.y, transform.position.z);

        bool swipeLeft = false;
        bool swipeRight = false;
        bool jump = false;
        bool roll = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                swiped = false;

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.deltaPosition.x < -50 && !swiped)
                {
                    swipeLeft = true;
                    swiped = true;
                }

                if (touch.deltaPosition.x > 50 && !swiped)
                {
                    swipeRight = true;
                    swiped = true;
                }

                if (touch.deltaPosition.y > 50 && !swiped)
                {
                    jump = true;
                    swiped = true;
                }

                if (touch.deltaPosition.y < -50 && !swiped)
                {
                    roll = true;
                    swiped = true;
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.Q) || swipeLeft) && !swiping && side != -1)
        {
            originX = transform.position.x;
            targetX = originX - 4.7f;
            side--;

            swiping = true;

            swipe.pitch = Random.Range(0.95f, 1.05f);
            swipe.Play();
        }

        if ((Input.GetKeyDown(KeyCode.D) || swipeRight) && !swiping && side != 1)
        {
            originX = transform.position.x;
            targetX = originX + 4.7f;
            side++;

            swiping = true;

            swipe.pitch = Random.Range(0.95f, 1.05f);
            swipe.Play();
        }

        if ((Input.GetKeyDown(KeyCode.Space) || jump) && onGround)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 7, rigidbody.velocity.z);

        if ((Input.GetKeyDown(KeyCode.S) || roll) && !rolling)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -20, rigidbody.velocity.z);
            StartCoroutine(Roll());

            if (!onGround)
                animator.CrossFade("Roll", 0.2f);
        }

        if (swiping)
        {
            swipeTime += Time.deltaTime * 10;

            float swipeValue = Mathf.Lerp(originX, targetX, swipeTime);
            float sz = Mathf.Lerp(0, 0.1f, swipeTime);


            transform.position = new Vector3(swipeValue, transform.position.y, transform.position.z + sz);

            if (swipeTime >= 1)
            {
                swiping = false;
                swipeTime = 0;
            }
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z - 2);

        animator.SetBool("OnGround", onGround);
    }

    IEnumerator Roll()
    {
        animator.SetBool("Rolling", rolling = true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("Rolling", rolling = false);
    }

    public void Die()
    {
        if (!animator.enabled || FindObjectOfType<RespawnCapsule>() != null)
            return;

        swiping = false;

        punch.Play();

        loseWidget.gameObject.SetActive(true);
        loseWidget.SetCoinsNeededText();

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            transforms[i] = rigidbodies[i].transform;
            positions[i] = rigidbodies[i].transform.position;
            rotations[i] = rigidbodies[i].transform.rotation;
        }

        animator.enabled = false;

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
            if (rigidbody.gameObject != gameObject)
                rigidbody.isKinematic = false;

        foreach (Collider collider in GetComponentsInChildren<Collider>())
            if (collider.gameObject != gameObject)
                collider.enabled = true;
    }

    public void Respawn()
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].transform.position = positions[i];
            rigidbodies[i].transform.rotation = rotations[i];
        }

        Instantiate(respawnCapsule, transform.position, Quaternion.identity);
        Destroy(hitObject);

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
            if (rigidbody.gameObject != gameObject && rigidbody.gameObject.name != "Player End")
                rigidbody.isKinematic = true;

        foreach (Collider collider in GetComponentsInChildren<Collider>())
            if (collider.gameObject != gameObject && collider.gameObject.name != "Player End")
                collider.enabled = false;

        animator.enabled = true;
        loseWidget.gameObject.SetActive(false);

        transform.position = new Vector3(side * 4.7f, transform.position.y, transform.position.z);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            if (collision.contacts[0].normal.y > 0)
                onGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            if (transform.position.y - collision.gameObject.transform.position.y > 0)
                onGround = false;
    }
}