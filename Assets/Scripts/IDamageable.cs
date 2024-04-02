/// <summary>
/// HP관련 인터페이스
/// </summary>
public interface IDamageable
{
    float HP { get; set; }
    void TakeDamage(float amount);
}