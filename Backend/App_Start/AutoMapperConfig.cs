using AutoMapper;
using ClientDto.Backend;
using Model;

namespace Backend.App_Start
{
	public class AutoMapperConfig
	{
		private static AutoMapperConfig _strapper;

		public static AutoMapperConfig Instance
		{
			get { return _strapper ?? (_strapper = new AutoMapperConfig()); }
		}

		public void Initialise()
		{
			Mapper.CreateMap<Product, ProductDetailDto>();
			Mapper.CreateMap<Coupon, CouponDetailDto>();
			Mapper.CreateMap<CouponDetailDto, Coupon>();
			Mapper.CreateMap<Order, OrderDetailDto>();
		}
	}

	public static class AutoMapperExtension
	{
		public static ProductDetailDto ToProductDetailDto(this Product model)
		{
			return model != null ? Mapper.Map<Product, ProductDetailDto>(model) : null;
		}

		public static CouponDetailDto ToCouponDetailDto(this Coupon model)
		{
			return model != null ? Mapper.Map<Coupon, CouponDetailDto>(model) : null;
		}

		public static Coupon ToCoupon(this CouponDetailDto dto)
		{
			return dto != null ? Mapper.Map<CouponDetailDto, Coupon>(dto) : null;
		}

		public static OrderDetailDto ToOrderDetailDto(this Order model)
		{
			return model != null ? Mapper.Map<Order, OrderDetailDto>(model) : null;
		}
	}
}