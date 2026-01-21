using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IEventoDominio
    {
        DateTime FechaOcurrencia { get; }
    }
}
