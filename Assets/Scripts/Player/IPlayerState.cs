
public enum PlayerState
{
    LockOnTarget,
    ShootTarget
}

public interface IPlayerState
{
    void UpdateState();
}