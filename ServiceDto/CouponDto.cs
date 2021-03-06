﻿using System;

namespace ServiceDto
{
    public class CouponDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 使用条件：生效时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 使用条件：结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 使用条件：最小金额（分）
        /// </summary>
        public int MinFee { get; set; }

        /// <summary>
        /// 使用条件：最小数量
        /// </summary>
        public int MinNum { get; set; }

        /// <summary>
        /// 使用条件：用户最小等级
        /// </summary>
        public int MinUserLevel { get; set; }

        /// <summary>
        /// 适用商品关系表ID
        /// </summary>
        public int CouponProductId { get; set; }

        /// <summary>
        /// 减免金额（分）
        /// </summary>
        public int RebateFee { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 用户最多可拥有数量
        /// </summary>
        public int MaxOwnLimit { get; set; }

        /// <summary>
        /// 用户领取后有效期：天
        /// </summary>
        public int ValidDays { get; set; }

        /// <summary>
        /// 是否长期有效
        /// </summary>
        public int IsPersistent { get; set; }

        public DateTime UpdateTime { get; set; }

        public string StartTimeStr { get { return StartTime.ToString("yyyy.MM.dd"); } }

        public string EndTimeStr { get { return EndTime.ToString("yyyy.MM.dd"); } }

        public string MinFeeStr
        {
            get { return ((float)(MinFee / 100.00)).ToString("F2").TrimEnd('0').TrimEnd('0').TrimEnd('.'); }
        }

        public string RebateFeeStr
        {
            get { return ((float)(RebateFee / 100.00)).ToString("F2").TrimEnd('0').TrimEnd('0').TrimEnd('.'); }
        }

        public int CanUse { get; set; }
        public int OwnCount { get; set; }

    }
}