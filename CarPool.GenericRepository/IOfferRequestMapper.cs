using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    interface IOfferRequestMapper
    {
        OfferRequest Find(string requestId);

        List<OfferRequest> GetAll();

        void Add(OfferRequest offerRequest);

        void Update(OfferRequest offerRequest);

        void Delete(OfferRequest offerRequest);
    }
}
