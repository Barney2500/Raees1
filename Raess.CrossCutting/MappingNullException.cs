using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raess.CrossCutting.Utils;

public class MappingNullException : Exception
{
    public MappingNullException() : base(Properties.Resources.ErrorMapperIsNull)
    {
    }

    public MappingNullException(string message) : base(message)
    {
    }
}
