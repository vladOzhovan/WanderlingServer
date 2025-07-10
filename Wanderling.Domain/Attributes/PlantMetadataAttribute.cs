namespace Wanderling.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class PlantMetadataAttribute : Attribute
    {
        public string TypeKey { get; }

        public PlantMetadataAttribute(string typeKey)
        {
            TypeKey = typeKey;
        }
    }
}
