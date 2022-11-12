using System;
using System.Collections.Generic;
using System.Linq;

namespace CouponOps
{
    public class CouponOperations : ICouponOperations
    {
        private Dictionary<string, Website> websites = new Dictionary<string, Website>();
        private Dictionary<string, Coupon> coupons = new Dictionary<string, Coupon>();
        private Dictionary<string, HashSet<Coupon>> couponsMap= new Dictionary<string, HashSet<Coupon>>();
        public CouponOperations()
        {
        }

        public void RegisterSite(Website w)
        {
            if (this.Exist(w)) throw new ArgumentException();
            this.websites.Add(w.Domain, w);
            this.couponsMap.Add(w.Domain, new HashSet<Coupon>());
        }

        public void AddCoupon(Website w, Coupon c)
        {
            if (!this.couponsMap.ContainsKey(w.Domain)) throw new ArgumentException();
            this.coupons.Add(c.Code, c);
            this.couponsMap[w.Domain].Add(c);
        }

        public Website RemoveWebsite(string domain)
        {
            if (this.websites.ContainsKey(domain) == false) throw new ArgumentException();

            var removedWebsite = this.websites[domain];
            
            this.websites.Remove(domain);
            this.couponsMap.Remove(domain);

            return removedWebsite;
        }

        public Coupon RemoveCoupon(string code)
        {
            if (this.coupons.ContainsKey(code) == false) throw new ArgumentException();
            
            var removedCoupon = this.coupons[code];
            this.coupons.Remove(code);
            foreach (var c in this.couponsMap)
            {
                if (c.Value.Contains(removedCoupon)) c.Value.Remove(removedCoupon);
            }
            
            return removedCoupon;
        }

        public bool Exist(Website w) => this.websites.ContainsKey(w.Domain);

        public bool Exist(Coupon c) => this.coupons.ContainsKey(c.Code);

        public IEnumerable<Website> GetSites() => this.websites.Values;

        public IEnumerable<Coupon> GetCouponsForWebsite(Website w)
        {
            if (this.couponsMap.ContainsKey(w.Domain) == false) throw new ArgumentException();
            return this.couponsMap[w.Domain];
        }

        public void UseCoupon(Website w, Coupon c)
        {
            if (this.websites.ContainsKey(w.Domain) == false) throw new ArgumentException();
            if (this.coupons.ContainsKey(c.Code) == false) throw new ArgumentException();
            if (this.couponsMap[w.Domain].Contains(c) == false) throw new ArgumentException();

            this.RemoveCoupon(c.Code);
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
            => this.coupons.Values.OrderByDescending(x => x.Validity).ThenByDescending(x => x.DiscountPercentage);

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
            => this.websites.Values.OrderBy(x => x.UsersCount).ThenByDescending(x => couponsMap[x.Domain].Count);
    }
}
