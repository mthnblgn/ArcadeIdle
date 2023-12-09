using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class Ingredient : PooledObject
{
    private int _id { get; set; }
    public int ID { get { return _id; } set { _id = value; } }
    private int _sellValue { get; set; }
    public int sellValue { get { return _sellValue; } set { _sellValue = value; } }

    [SerializeField] Renderer _renderer;

    Transform _followPoint;

    private void FixedUpdate()
    {
        if (_followPoint != null)
        {
            transform.position = Vector3.Slerp(transform.position,_followPoint.transform.position,0.5f-(_followPoint.position.y-1.4f)/12.5f);
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
    public void Put()
    {
        Carrier c = _followPoint.GetComponent<Carrier>();
        c._isEmpty = true;
        c.ExplodeParticle();
        _followPoint=null;
        Release(0);
    }
}
