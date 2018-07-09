namespace LiteAdmin.Core
{
    public class LookupModel
    {
        public LookupModel()
        {
        }

        public LookupModel(object id, string name)
        {
            Id = id;
            Name = name;
        }

        public object Id { get; set; }

        public string Name { get; set; }
    }
}
