public struct PositionComponent : IComponent
{
    float x;
    float y;
}
public struct RotationComponent : IComponent
{

}
public struct BoxColliderComponent : IComponent
{
    float cx, cy;
    float w, h;
}
public struct VelocityComponent : IComponent
{
    float dx, dy;
}