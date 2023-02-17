using ProjectBakery.Domain;

namespace ProjectBakery.UI.CA.Extensions
{
    public static class BakeryExtension
    {
        public static string GetInfo(this Bakery bakery)
        {
            return $"{bakery.Name} -> {bakery.Location} (Total employees: {bakery.Employees}) Baker: {bakery.Baker.Name}";
        }
    }
}