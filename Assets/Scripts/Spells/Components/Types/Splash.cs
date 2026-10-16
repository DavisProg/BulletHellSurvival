using UnityEngine;

public class Splash : MonoBehaviour
{
    IEffect[] effects;
    Collider2D col;
    Animator animator;
    bool colliderActivated = false;
    [SerializeField] AudioClip[] soundFX;
    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        animator = GetComponent<Animator>();
        if(soundFX.Length > 0)
        {
            SoundFXManager.instance.playSoundEffect(soundFX, transform, 1f);
        }
    }

    public void Init(float size, IEffect[] effects, float duration){
        this.effects = effects;
        
        gameObject.transform.localScale = new Vector3(size, size, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy")){
		    if (collision.TryGetComponent(out Enemy enemy)){
                foreach (var eff in effects)
                {
                    eff.Apply(collision.gameObject);
                }
            }
        }   
    }
    void ActivateCollider()
    {
        if (colliderActivated)
            return;

        col.enabled = true;
        colliderActivated = true;
    }

    void DestroySplash()
    {
        Destroy(gameObject);
    }
}
