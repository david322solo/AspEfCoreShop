using EFDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataLibrary.Interfaces
{
    interface ICart<T>
    {
        void AddLine(Product product, int quantity);
        void Clear();
        void RemoveLine(Product product);
        IEnumerable<T> Lines { get; }
        decimal ComputeTotalValue();
        
    }
}
