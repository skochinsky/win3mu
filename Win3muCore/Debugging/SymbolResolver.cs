using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore
{
    public class SymbolResolver : ISymbolScope
    {
        public SymbolResolver(Machine machine)
        {
            _machine = machine;
        }

        Machine _machine;
        Dictionary<string, Symbol> _symbolMap;

        public Symbol ResolveSymbol(string name)
        {
            if (_symbolMap == null)
                BuildSymbolMap();

            Symbol sym;
            if (_symbolMap.TryGetValue(name, out sym))
                return sym;

            return null;
        }

        void BuildSymbolMap()
        {
            _symbolMap = new Dictionary<string, Symbol>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var m in _machine.ModuleManager.AllModules.OfType<Module32>())
            {
                foreach (var ord in m.GetExports())
                {
                    var name = m.GetNameFromOrdinal(ord);
                    var addr = m.GetProcAddress(ord);
                    _symbolMap.Add(name, new LiteralSymbol(new FarPointer(addr)));
                }
            }

            foreach (var mname in MessageNames.All)
            {
                _symbolMap.Add(mname.Value, new LiteralSymbol(mname.Key));
            }
        }
    }
}
