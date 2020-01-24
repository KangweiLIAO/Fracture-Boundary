using UnityEngine;

public class ShotBehavior : MonoBehaviour {

	public Vector3 target;
    public GameObject collisionExplossion;
    public float speed;

    private void Update()
    {
        float step = speed + Time.deltaTime;

        if(target != null)
        {
            if(transform.position == target)
            {
                Explode();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    void Explode()
    {
        if(collisionExplossion != null)
        {
            GameObject explosion = Instantiate(collisionExplossion, transform.position, transform.rotation);
            //Destroys the lazer itself
            Destroy(gameObject);
            //Destroys the explosion
            Destroy(explosion, 1f);
        }
    }
}
