using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    interface IOfferMapper
    {
        Offer Find(string offerId);

        List<Offer> GetAll();

        void Add(Offer offer);

        void Update(Offer offer);

        void Delete(Offer offer);
    }
}
