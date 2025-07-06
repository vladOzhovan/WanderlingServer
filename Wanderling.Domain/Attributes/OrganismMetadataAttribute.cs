namespace Wanderling.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OrganismMetadataAttribute : Attribute
    {
        public string TypeKey { get; }
        public Type StrategyType { get; }

        public OrganismMetadataAttribute(string typeKey, Type strategyType)
        {
            TypeKey = typeKey;
            StrategyType = strategyType;
        }
    }
}
