using Shirzad.Core.publicClasses;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Context;
using Shirzad.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.Repository.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        private GenericClass<ApplicationUser> _userManager;
        private GenericClass<Product> _product;
        private GenericClass<ProductGallery> _gallery;
        private GenericClass<Category> _category;
        private GenericClass<Slider> _slider;
        private GenericClass<ContactUs> _contactUs;
        private GenericClass<Pricing> _pricing;
        private GenericClass<Service> _services;
        private GenericClass<EmailRegister> _emailRegister;
        private GenericClass<WebPayOnline> _wePayOnline;

        //WebPay Online
        public GenericClass<WebPayOnline> webPayOnlineUW
        {
            get
            {
                if (_wePayOnline == null)
                {
                    _wePayOnline = new GenericClass<WebPayOnline>(_context);
                }
                return _wePayOnline;
            }
        }


        //Email Register
        public GenericClass<EmailRegister> emailRegisterUW
        {
            get
            {
                if (_emailRegister == null)
                {
                    _emailRegister = new GenericClass<EmailRegister>(_context);
                }
                return _emailRegister;
            }
        }


        //Services
        public GenericClass<Service> serviceUW
        {
            get
            {
                if (_services == null)
                {
                    _services = new GenericClass<Service>(_context);
                }
                return _services;
            }
        }



        //Pricing
        public GenericClass<Pricing> pricingUW
        {
            get
            {
                if (_pricing == null)
                {
                    _pricing = new GenericClass<Pricing>(_context);
                }
                return _pricing;
            }
        }

        //Contact Us
        public GenericClass<ContactUs> contactUsUW
        {
            get
            {
                if (_contactUs == null)
                {
                    _contactUs = new GenericClass<ContactUs>(_context);
                }
                return _contactUs;
            }
        }


        // Slider
        public GenericClass<Slider> sliderUW
        {
            get
            {
                if (_slider == null)
                {
                    _slider = new GenericClass<Slider>(_context);
                }
                return _slider;
            }
        }

        // Category
        public GenericClass<Category> categoryUW
        {
            get
            {
                if (_category == null)
                {
                    _category = new GenericClass<Category>(_context);
                }
                return _category;
            }
        }

        // ProductGallery
        public GenericClass<ProductGallery> galleryUW
        {
            get
            {
                if (_gallery == null)
                {
                    _gallery = new GenericClass<ProductGallery>(_context);
                }
                return _gallery;
            }
        }

        // Product
        public GenericClass<Product> productUW
        {
            get
            {
                if (_product == null)
                {
                    _product = new GenericClass<Product>(_context);
                }
                return _product;
            }
        }

        // UserManager
        public GenericClass<ApplicationUser> userManagerUW
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new GenericClass<ApplicationUser>(_context);
                }
                return _userManager;
            }
        }

        public async Task saveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
