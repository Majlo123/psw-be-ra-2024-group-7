using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IQuizRepository
    {
        Quiz Create(Quiz quiz);
        Quiz Get(long id);
    }
}
