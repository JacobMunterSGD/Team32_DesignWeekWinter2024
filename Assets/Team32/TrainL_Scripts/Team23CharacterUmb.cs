using UnityEngine;
using UnityEngine.InputSystem;

public class Team32CharacterUmb : MicrogameInputEvents
{
    public PhysicsMaterial2D material1; // 第一个PhysicsMaterial2D在Unity编辑器中指定
    public PhysicsMaterial2D material2; // 第二个PhysicsMaterial2D在Unity编辑器中指定

    private bool material1Applied = false; // 跟踪第一个物理材质是否已应用
    private bool material2Applied = false; // 跟踪第二个物理材质是否已应用

    protected override void OnGameStart()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = material1;
        material1Applied = true; // 更新标志状态
    }

    protected override void OnButton1Pressed(InputAction.CallbackContext context)
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (!material1Applied && !material2Applied)
        {
            // 如果没有应用物理材质，则应用第一个物理材质
            collider.sharedMaterial = material1;
            material1Applied = true;
        }
        else if (material1Applied && !material2Applied)
        {
            // 如果应用了第一个物理材质，则切换到第二个物理材质
            collider.sharedMaterial = material2;
            material1Applied = false;
            material2Applied = true;
        }
        else if (!material1Applied && material2Applied)
        {
            // 如果应用了第二个物理材质，则切换回第一个物理材质
            collider.sharedMaterial = material1;
            material1Applied = true;
            material2Applied = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (material2Applied)
        {
            // 当应用了第二个物理材质时，碰撞后切换回第一个物理材质
            material1Applied = true;
            material2Applied = false;
        }
    }
}