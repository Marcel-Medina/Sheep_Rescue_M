using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay;
    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;
    public float heartOffset; 
    public GameObject heartPrefab;
    private bool isDropping;// Added to prevent multiple drops of same sheep
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);

        hitByHay = true; 
        runSpeed = 0; 

        Destroy(gameObject, gotHayDestroyDelay);
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; 
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.SavedSheep();

    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Hay") && !hitByHay) 
        {
            Destroy(other.gameObject); 
            HitByHay(); 
        }
        else if (other.CompareTag("DropSheep") && !isDropping)
        {
            Drop();
        }
    }
    private void Drop()
    {
        GameStateManager.Instance.DroppedSheep();
        Camera.main.GetComponent<CameraShake>().Shake();
        sheepSpawner.RemoveSheepFromList(gameObject);
        isDropping = true;
        myRigidbody.isKinematic = false; 
        myCollider.isTrigger = false; 
        Destroy(gameObject, dropDestroyDelay);
        SoundManager.Instance.PlaySheepDroppedClip();
    }
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
