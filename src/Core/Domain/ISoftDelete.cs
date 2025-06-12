using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    void Delete();
    void Restore();
}
