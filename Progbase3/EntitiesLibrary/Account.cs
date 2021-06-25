using System;

namespace EntitiesLibrary
{
    [Serializable]
	public class Account : IEntity
	{
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
