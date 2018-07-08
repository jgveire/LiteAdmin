namespace LiteAdmin
{
    using System.Collections.Generic;

    public class LiteAdminOptions
    {
        public string Role { get; set; }

        public IEnumerable<string> Tables { get; set; } = new List<string>();

        public string Username { get; set; }
    }
}