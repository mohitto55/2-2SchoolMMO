using Runtime.DB.Model;
using Runtime.DB.SD;

namespace Runtime.DB.Model
{
    [System.Serializable]
    public class EntityModel : Model<SDEntity>
    {
        public EntityModel(SDEntity sd) : base(sd)
        {
        }
    }
}