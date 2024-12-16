

using Arch.Core;

public record struct PositionComponent(float x, float y);

public record struct VelocityComponent(float vx, float vy);

public record struct BoundColliderComponent(float w, float h, bool isTrigger, List<Entity> contactsEntity);

public record struct PacketSendTimer(double sendTime, double elasedTime);

public record struct EntityTypeComponent(EEntityType type);

