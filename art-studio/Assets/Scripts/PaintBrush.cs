using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [SerializeField] private Material brushMaterial;
    [SerializeField] private Color baseColor;
    [SerializeField] private LayerMask colorLayer;
    [SerializeField] private Collider brushHeadCollider;
    [SerializeField] private int brushWidth = 5;
    [SerializeField] private Transform tip;

    private Color _tempCurrentColor;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _hit;
    private PaintCanvas _paintCanvas;
    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    private void Start()
    {
        brushMaterial.color = baseColor;
        _tempCurrentColor = baseColor;
        _colors = Enumerable.Repeat(brushMaterial.color, brushWidth * brushWidth).ToArray();
        _tipHeight = tip.localScale.z;
    }

    private void Update()
    {
        Draw();
    }

    public void TryAbsorbPaletteColor(Collider palette)
    {
        palette.gameObject.TryGetComponent<Renderer>(out Renderer renderer);
        _tempCurrentColor = renderer.material.color;
        brushMaterial.color = _tempCurrentColor;
        _colors = Enumerable.Repeat(brushMaterial.color, brushWidth * brushWidth).ToArray();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(colorLayer.value, 2))
        {
            TryAbsorbPaletteColor(other);
        }
    }
    private void Draw()
    {
        if (Physics.Raycast(tip.position, tip.forward, out _hit, _tipHeight*25))
        {
            if (_hit.transform.CompareTag("PaintCanvas"))
            {
                if (_paintCanvas == null)
                {
                    _paintCanvas = _hit.transform.GetComponent<PaintCanvas>();
                }

                _touchPos = new Vector2(_hit.textureCoord.x, _hit.textureCoord.y);

                var x = (int)(_touchPos.x * _paintCanvas.textureSize.x - (brushWidth / 2));
                var y = (int)(_touchPos.y * _paintCanvas.textureSize.y - (brushWidth / 2));

                if (y < 0 || y > _paintCanvas.textureSize.y || x < 0 || x > _paintCanvas.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _paintCanvas.texture.SetPixels(x, y, brushWidth, brushWidth, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _paintCanvas.texture.SetPixels(lerpX, lerpY, brushWidth, brushWidth, _colors);
                    }
                    transform.rotation = _lastTouchRot;

                    _paintCanvas.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        _paintCanvas = null;
        _touchedLastFrame = false;
    }
}
