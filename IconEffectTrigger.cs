using UnityEngine;

public enum IconAnimation
    {   
        Default,
        SlashFire
    }

public class IconEffectTrigger : AnimatedObject
{

    public void PlayAnimation(IconAnimation iconAnimation)
    {
        ChangeAnimationState(iconAnimation.ToString());
    }
    
    void Update()
    {
        if(this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            PlayAnimation(IconAnimation.Default);
        }

    }
}
