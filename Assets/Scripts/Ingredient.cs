
using DG.Tweening;  
using UnityEngine;
using UnityEngine.Pool;

public class Ingredient : MonoBehaviour
{
    private int _id { get; set; }
    public int ID { get { return _id; } set { _id = value; } }

    [SerializeField] Renderer _renderer;

    private IObjectPool<Ingredient> objectPool;
    public IObjectPool<Ingredient> ObjectPool { set => objectPool = value; }
    Carrier _carryPoint;

    private void FixedUpdate()
    {
        if (_carryPoint != null)
        {
            transform.DOMove(_carryPoint.transform.position, (_carryPoint.transform.position.y * 0.05f));
        }
    }
    public void ChangeMaterial(Material mat)
    {
        _renderer.material = mat;
    }
    public void Carry(Carrier carryPoint)
    {
        if (carryPoint != null)
        {
            _carryPoint = carryPoint;
            transform.parent = _carryPoint.transform;
            _carryPoint._isEmpty = false;
        }
    }
}
