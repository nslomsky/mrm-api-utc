using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
    public class ProductServiceMock : ProductService
    {
        public ProductServiceMock(IProductIssueRepository productIssueRepository = null,
            IProductFamilyRepository productFamilyRepository = null,
            IProductRepository productRepository = null)
            : base(productRepository, productFamilyRepository, productIssueRepository)
        {

        }

    }
}
