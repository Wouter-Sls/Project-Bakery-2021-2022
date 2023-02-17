using ProjectBakery.Domain;

namespace ProjectBakery.UI.CA.Extensions
{
    public static class PastrieExtension
    {
        public static string GetInfo(this Pastrie pastrie)
        {
           return $"{pastrie.Name} {pastrie.Price} euro ({pastrie.Type})";
           
        }
    }
}