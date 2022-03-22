using UnityEngine;

public class Spluter : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    [Range(1000, 100000)]
    private int minimumFrames = 0;

    [SerializeField]
    [Range(1000, 100000)]
    private int maximumFrames = 0;

    [SerializeField]
    private int framesUntilNextSplurt;
    #endregion

    #region Fields
    private ParticleSystem particleSystem;

    private AudioSource audioSource;

    #endregion

    #region Properties

    #endregion

    #region Initialisation Functions
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        framesUntilNextSplurt = Random.Range(minimumFrames, maximumFrames);
    }
    #endregion

    #region Update Functions
    private void FixedUpdate()
    {
        framesUntilNextSplurt--;

        if (framesUntilNextSplurt == 0) Splurt();
    }
    #endregion

    #region Splurt Functions
    public void Splurt()
    {
        particleSystem.Play();
        audioSource.Play();
        framesUntilNextSplurt = Random.Range(minimumFrames, maximumFrames);
    }
    #endregion
}
