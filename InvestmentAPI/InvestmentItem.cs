using System;

namespace InvestmentAPI
{
    public class InvestmentItem
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public InvestmentItem() { }
        public InvestmentItem(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
