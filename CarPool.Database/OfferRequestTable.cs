//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarPool.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferRequestTable
    {
        public string ID { get; set; }
        public string RiderId { get; set; }
        public string RideeId { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Status { get; set; }
        public int NumberOfPassengers { get; set; }
    }
}