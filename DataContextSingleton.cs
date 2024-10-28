using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienSuToCoffee
{
    public sealed class DataContextSingleton
    {
        private static readonly Lazy<MYCOFFEEEntitiesS> instance = new Lazy<MYCOFFEEEntitiesS>(() => new MYCOFFEEEntitiesS());

        public static MYCOFFEEEntitiesS Instance => instance.Value;

        private DataContextSingleton() { }
    }
}
