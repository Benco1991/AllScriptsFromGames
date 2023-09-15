using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    [SerializeField]
    private AudioClip effectsClip;
    Collider2D col;
    public ParticleSystem.MainModule particle;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    /*void OnDestroy()
    {
        GameManager.PlayAudioClip -= PlayAudio;
    }*/

    void PlayAudio(AudioClip clip)
    {
        GameManager manager = FindObjectOfType<GameManager>();
        AudioSource audioSource = manager.gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Destroy(audioSource, 1);
        }
        Debug.Log("Play");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Vulnerable"))
        {
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager != null) 
            {
                GameManager.PlayAudioClip += PlayAudio;
                GameManager.TriggerAudioEvent(effectsClip);
                manager.CheckScoreToLVL();
                Debug.Log("score");
                Destroy(gameObject);
                //StartCounting();
            }
        }
    }
    /*private void StartCounting()
    { StartCoroutine(Counting()); }
    private IEnumerator Counting()
    {
        float startTime = Time.time;
        float duration = 1f;
        float fadeDuration = 1;
        particle = GetComponentInChildren<ParticleSystem>().main;
        particle.startColor = new Color(particle.startColor.color.r, particle.startColor.color.g, particle.startColor.color.b, 1f);
        Color startColor = particle.startColor.color;
        Color targetColor = new Color(particle.startColor.color.r, particle.startColor.color.g, particle.startColor.color.b, 0f);
        while (Time.time - startTime < duration)
        {
            yield return null;
        }

        float startTime1 = Time.time;
        while (Time.time - startTime1 < fadeDuration)
        {
            float elapsed = Time.time - startTime1;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            Color newColor = Color.Lerp(startColor, targetColor, t);
;
            particle.startColor = newColor;

            yield return null;
        }
        if (gameObject != null)
            Destroy(gameObject);
    }*/
}
