using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Core.Entities;

namespace TechChallenge.Queue.Producer
{
    public interface IContatoProducer
    {
        void SendInsert(Contato contato);
        void SendUpdate(Contato contato);
        void SendDelete(int id);
    }
}
