using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] private float scrollSpeed = 0.5f;

    private Material myMaterial;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        float offset = Time.time * scrollSpeed;

        myMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
