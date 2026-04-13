using UnityEngine;
using System.Collections;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;
    public float horizontalBoundary = 22;
    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;
    public Transform modelParent;
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    [Header("Power Up Stats")]
    public float powerUpMoveSpeed = 30f;
    public float powerUpShootInterval = 0.1f;
    public float duration = 10f;

    private float originalMoveSpeed;
    private float originalShootInterval;

    void Start()
    {
        LoadModel();
        originalMoveSpeed = movementSpeed;
        originalShootInterval = shootInterval;
    }

    private void LoadModel()
    {
        if (modelParent.childCount > 0)
            Destroy(modelParent.GetChild(0).gameObject);

        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;
            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;
            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }

    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        if (SoundManager.Instance != null) SoundManager.Instance.PlayShootClip();
    }

    public void ActivateSpeedPowerUp()
    {
        StopAllCoroutines();
        StartCoroutine(PowerUpRoutine());
    }

    private IEnumerator PowerUpRoutine()
    {
        movementSpeed = powerUpMoveSpeed;
        shootInterval = powerUpShootInterval;

        yield return new WaitForSeconds(duration);

        movementSpeed = originalMoveSpeed;
        shootInterval = originalShootInterval;
    }
}