using AutoMapper;
using ClientDto.Api;
using Model;
using ServiceDto;
using CouponDto = ServiceDto.CouponDto;

namespace Api.App_Start
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
			Mapper.CreateMap<CouponDto, ClientDto.Api.CouponDto>();
			Mapper.CreateMap<GenerateOrderDto, Order>();
		}
	}

	public static class AutoMapperExtension
	{
		public static ProductDetailDto ToProductDetailDto(this Product model)
		{
			return model != null ? Mapper.Map<Product, ProductDetailDto>(model) : null;
		}

		public static ClientDto.Api.CouponDto ToCouponDto(this CouponDto model)
		{
			return model != null ? Mapper.Map<CouponDto, ClientDto.Api.CouponDto>(model) : null;
		}

		public static Order ToOrder(this GenerateOrderDto dto)
		{
			return dto != null ? Mapper.Map<GenerateOrderDto, Order>(dto) : null;
		}
	}
}