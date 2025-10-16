namespace EazyOnRent.Model
{
    public class UserInformation
    {
        public List<Lister>? lister { get; set; } = new List<Lister>();
        public List<Renter>? renter { get; set; } = new List<Renter> { };
    }
}
