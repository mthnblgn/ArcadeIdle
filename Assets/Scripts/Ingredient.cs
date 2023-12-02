
using UnityEngine;
using UnityEngine.Pool;

public class Ingredient :MonoBehaviour
{
    Renderer _renderer;
    private IObjectPool<Ingredient> objectPool;
    public IObjectPool<Ingredient> ObjectPool { set => objectPool = value; }
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    public void ChangeColor(Material mat)
    {
        _renderer.material=mat;
    }
}
