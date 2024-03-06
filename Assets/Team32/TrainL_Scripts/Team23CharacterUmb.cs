using UnityEngine;
using UnityEngine.InputSystem;

public class Team32CharacterUmb : MicrogameInputEvents
{
    public PhysicsMaterial2D material1; // 第一个物理材质，在 Unity 编辑器中指定
    public PhysicsMaterial2D material2; // 第二个物理材质，在 Unity 编辑器中指定

    private bool material1Applied = true; // 跟踪第一个物理材质是否已应用，默认为 true
    private bool material2Applied = false; // 跟踪第二个物理材质是否已应用，默认为 false

    public delegate void CollisionEventHandler();
    public event CollisionEventHandler OnCollisionWithUmbrella;

    protected override void OnGameStart()
    {
        // 游戏开始时默认应用第一个物理材质
        ApplyMaterial(material1);
    }

    protected override void OnButton1Pressed(InputAction.CallbackContext context)
    {
        // 按下按钮1时切换物理材质
        if (material1Applied)
        {
            ApplyMaterial(material2);
        }
        else if (material2Applied)
        {
            ApplyMaterial(material1);
        }
    }

    private void ApplyMaterial(PhysicsMaterial2D material)
    {
        // 应用指定的物理材质，并更新标志状态
        GetComponent<Collider2D>().sharedMaterial = material;
        material1Applied = material == material1;
        material2Applied = material == material2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰撞时判断当前应用的物理材质，如果是第二个，则切换回第一个
        if (material2Applied)
        {
            ApplyMaterial(material1);
            // 触发事件
            OnCollisionWithUmbrella?.Invoke();
        }
    }
}