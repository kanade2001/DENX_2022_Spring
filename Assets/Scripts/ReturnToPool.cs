//https://github.com/yucchiy/unity-sandbox/blob/main/Assets/ObjectPoolCheck/Scripts/ReturnToPool.cs
using UnityEngine.Pool;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public ParticleSystem Particle;
    public ObjectPool<ParticleSystem> Pool;

    void Start()
    {
        Particle = GetComponent<ParticleSystem>();
        var main = Particle.main;
        // Callbackを指定すると、パーティクルが終了するときに
        // コールバックメソッドとしてOnParticleSystemStopped
        // が呼び出される
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        // パーティクルシステムが停止したときにここが呼び出される

        // プールから借りていたパーティクルを解放(返却)する
        Pool.Release(Particle);
    }
}