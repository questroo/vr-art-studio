using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [SerializeField] private Material brushMaterial;
    [SerializeField] private Color baseColor;
    [SerializeField] private LayerMask colorLayer;
    [SerializeField] private Collider brushHeadCollider;
    private Color tempCurrentColor;

    private void Start()
    {
        brushMaterial.color = baseColor;
        tempCurrentColor = baseColor;
    }

    public void TryAbsorbPaletteColor(Collider palette)
    {
        palette.gameObject.TryGetComponent<Renderer>(out Renderer renderer);
        tempCurrentColor = renderer.material.color;
        brushMaterial.color = tempCurrentColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(colorLayer.value, 2))
        {
            TryAbsorbPaletteColor(other);

        }
    }
}
