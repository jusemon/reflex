using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    [SerializeField] List<Renderer> backgroundRenderer;
    [SerializeField][Min(1)] float speedFactor;
    private List<Material> backgroundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = backgroundRenderer.Select(renderer => renderer.material).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.ForEach(material => material.mainTextureOffset += new Vector2(0.01f * speedFactor, 0) * Time.deltaTime);
    }
}
