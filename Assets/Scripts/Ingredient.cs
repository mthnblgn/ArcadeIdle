
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class Ingredient : MonoBehaviour
{
    private int _id { get; set; }
    public int ID { get { return _id; } set { _id = value; } }
    private int _sellValue { get; set; }
    public int sellValue { get { return _id; } set { _id = value; } }

    [SerializeField] Renderer _renderer;

    private IObjectPool<Ingredient> objectPool;
    public IObjectPool<Ingredient> ObjectPool { set => objectPool = value; }
    Transform _followPoint;

    private void FixedUpdate()
    {
        if (_followPoint != null)
        {
            transform.DOMove(_followPoint.transform.position, (_followPoint.transform.position.y * 0.05f));
        }
    }
    public void ChangeMaterial(Material mat)
    {
        _renderer.material = mat;
    }
    public void Carry(Transform followPoint)
    {
        if (followPoint != null)
        {
            _followPoint = followPoint;
            Carrier c = followPoint.GetComponent<Carrier>();
            c._isEmpty = false;
            c._ingredient = this;
        }
    }
    public void Release()
    {
        Carrier c = _followPoint.GetComponent<Carrier>();
        c._isEmpty = true;
        c.ExplodeParticle();
        _followPoint=null;
        objectPool.Release(this);
    }
}
