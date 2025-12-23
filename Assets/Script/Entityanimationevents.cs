using UnityEngine;

public class Entityanimationevents : MonoBehaviour
{
    private Entity entity;


    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }


    public void DamageTargets() => entity.DamageTargets();


    private void DiasabledMovementAndJump() => entity.EnableMovementAndJump(false);

    private void EnableMovementAndJump() => entity.EnableMovementAndJump(true);
}
