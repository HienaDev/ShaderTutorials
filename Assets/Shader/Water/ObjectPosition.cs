using UnityEngine;

public class ObjectPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_ObjectPosition", new Vector4(transform.position.x, transform.position.y, transform.position.z, transform.localScale.x));
    }
}
