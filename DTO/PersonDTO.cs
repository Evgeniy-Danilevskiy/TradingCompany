using System;


namespace DTO
{
    public class PersonDTO
    {
        public int ID_Person { get; set; }
        public int ID_Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public DateTime RowInsertTime { get; set; }
        public DateTime RowUpdateTime { get; set; }
        public Guid Salt { get; set; }
    }
}
