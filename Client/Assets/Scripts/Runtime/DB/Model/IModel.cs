namespace Runtime.DB.Model
{
    /// <summary>
    /// Have only property, various Field
    /// </summary>
    public interface IModel
    {

    }
    /// <summary>
    /// Have only property, various Field
    /// </summary>
    public abstract class Model<TStaticData> : IModel
    {
        public TStaticData sd;

        public Model(TStaticData sd)
        {
            this.sd = sd;
        }
    }
}