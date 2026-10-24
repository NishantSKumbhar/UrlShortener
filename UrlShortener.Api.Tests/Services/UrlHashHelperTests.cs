using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Tests.Services
{

    
    public class UrlHashHelperTests
    {
        [Fact]
        public void Compute_ValidUrl_Return64CharacterHash()
        {
            var orginalUrl = "https://example.com/products";

            var hash = UrlHashHelper.Compute(orginalUrl);

            Assert.Equal(64, hash.Length);
        }
        

        [Fact]
        public void Compute_SameUrl_ReturnsSameHash()
        {
            var orginalUrl = "https://example.com/products";

            var firstHash = UrlHashHelper.Compute(orginalUrl);
            var secondHash = UrlHashHelper.Compute(orginalUrl);

            Assert.Equal(firstHash, secondHash);
        }

        [Fact]
        public void Compute_DifferentUrls_ReturnsDifferentHashes()
        {
            var firstUrl = "https://example.com/products";
            var secondUrl = "https://example.com/orders";

            var firstHash = UrlHashHelper.Compute(firstUrl);
            var secondHash = UrlHashHelper.Compute(secondUrl);

            Assert.NotEqual(firstHash, secondHash);
        }

    }
}
