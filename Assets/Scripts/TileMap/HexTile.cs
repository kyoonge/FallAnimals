using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private float _pushVelocity = 0.07f;
    [SerializeField] private float _destroyDelay = 0.2f;
    private bool isPushed = false;
    [SerializeField] private Material mat;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player") && !isPushed)
        {
            isPushed = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -1.7f, 0), _pushVelocity);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 2f, 0), _pushVelocity);
            gameObject.GetComponent<MeshRenderer>().material = mat;
            Invoke("ResetDestroy", 1.5f);
        }
    }

    private void ResetDestroy()
    {
        //transform.Translate(new Vector3(0, +0.1f, 0));
        Destroy(this.gameObject, _destroyDelay);
    }


}
