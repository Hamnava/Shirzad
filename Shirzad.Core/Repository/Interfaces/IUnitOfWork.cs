using Shirzad.Core.publicClasses;
using Shirzad.DataLayer.Entities;


namespace Shirzad.Core.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        GenericClass<ContactUs> contactUsUW { get; }
        GenericClass<Slider> sliderUW { get; }
        GenericClass<Category> categoryUW { get; }
        GenericClass<ProductGallery> galleryUW { get; }
        GenericClass<Product> productUW { get; }
        GenericClass<ApplicationUser> userManagerUW { get; }
        GenericClass<Pricing> pricingUW { get; }
        GenericClass<Service> serviceUW { get; }
        GenericClass<EmailRegister> emailRegisterUW { get; }
        Task saveAsync();
    }
}
