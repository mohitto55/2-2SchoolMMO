

public record struct PositionComponent(float x, float y);

public record struct VelocityComponent(float vx, float vy);

public record struct BoundColliderComponent(float cx, float cy, float w, float h);

public record struct PacketSendTimer(float sendTime, float elasedTime);

public record struct EntityTypeComponent(EEntityType type);