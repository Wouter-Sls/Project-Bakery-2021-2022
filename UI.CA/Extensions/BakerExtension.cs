using System;
using ProjectBakery.Domain;

namespace ProjectBakery.UI.CA.Extensions
{
    public static class BakerExtension
    {
        public static string GetInfo(this Baker baker)
        {
            return $"{baker.Name} born: {baker.BirthDate}";
        }
    }
}