namespace CreatingDbContextForNorthwind
{
    using System.Data.Linq;

    public partial class Employee
    {
        public EntitySet<Territory> Territory { get; set; }
    }
}
