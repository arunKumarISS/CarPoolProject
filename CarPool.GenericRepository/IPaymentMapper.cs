using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    interface IPaymentMapper
    {
        Payment Find(string paymentId);

        List<Payment> GetAll();

        void Add(Payment payment);

        void Update(Payment payment);

        void Delete(Payment payment);
    }
}
