using UnityEngine;

public class Paintable : MonoBehaviour
{
    public float strength;
    public float hardness;
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int PaintPosition = Shader.PropertyToID("_PaintPosition");
    private static readonly int PaintColor = Shader.PropertyToID("_PaintColor");
    private static readonly int PaintRadius = Shader.PropertyToID("_PaintRadius");
    private static readonly int PaintHardness = Shader.PropertyToID("_PaintHardness");
    private static readonly int PaintStrength = Shader.PropertyToID("_PaintStrength");
    private RenderTexture _renderTexture;
    private Renderer _renderer;

    public void Paint(Vector3 collisionPosition, Color paintColor, float paintRadius)
    {
        var material = _renderer.material;
        material.SetVector(PaintPosition, collisionPosition);
        material.SetColor(PaintColor, paintColor);
        material.SetFloat(PaintRadius, paintRadius);
        material.SetFloat(PaintHardness, hardness);
        material.SetFloat(PaintStrength, strength);
        material.SetTexture(MainTex, _renderTexture);


        var tmpTexture = RenderTexture.GetTemporary(_renderTexture.width, _renderTexture.height, depthBuffer: 0);
        Graphics.Blit(_renderTexture, tmpTexture);
        Graphics.Blit(tmpTexture, _renderTexture, material);
        RenderTexture.ReleaseTemporary(tmpTexture);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        var material = _renderer.material;
        _renderTexture = new RenderTexture(material.mainTexture.width, material.mainTexture.height, depth: 0);
    }

    private void OnDestroy()
    {
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }
    }
}