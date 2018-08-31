using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public class Constants
    {
        #region 版本

        /// <summary>
        /// 客户端版本
        /// </summary>
        public enum Version
        {
            wap = 1
        };

        #endregion 版本

        #region 请求参数

        #region 头信息

        public struct HttpHeader
        {
            /// <summary>
            /// 版本号
            /// </summary>
            public const string CLIENT_VERSION = "V";

            /// <summary>
            /// 客户端ID
            /// </summary>
            public const string CLIENT_ID = "ClientId";

            /// <summary>
            /// 渠道号
            /// </summary>
            public const string CHANNEL_ID = "ChannelId";

            /// <summary>
            /// 手机号码（本机）
            /// </summary>
            public const string TEL = "TEL";

            /// <summary>
            /// User Agent
            /// </summary>
            public const string USER_AGENT = "UA";

            /// <summary>
            /// IMSI
            /// </summary>
            public const string IMSI = "IMSI";

            /// <summary>
            /// IMEI
            /// </summary>
            public const string IMEI = "IMEI";

            /// <summary>
            /// 省份
            /// </summary>
            public const string PROVINCE = "Province";

            /// <summary>
            /// 城市
            /// </summary>
            public const string CITY = "City";
        }

        #endregion 头信息

        #endregion 请求参数

        #region 加密key

        public struct SecurityKey
        {
            public const string Host = "";

            public const string key = "2015ReaderWebBookReadChapter_Key";
            public const string IV = "2015ReaderWeb_IV";

            /// <summary>
            /// 地址栏参数
            /// 加密Key
            /// </summary>
            public const string UrlKey = "c3Ye1";

            /// <summary>
            /// 头信息
            /// </summary>
            public const string HeaderInfo_SessionName = "WebRead_HeaderInfo";

            public const string HeaderInfo_CookieName = "web_crhdrinfo";

            /// <summary>
            /// 用户信息（已登录）
            /// </summary>
            public const string LoginedUserInfo_SessionName = "WebRead_LoginedUserInfo";

            public const string LoginedUserInfo_CookieName = "web_crldrinfo";

            /// <summary>
            /// 用户登录成功后跳转到ReturnUrl
            /// </summary>
            public const string LoginedReturnUrl_SessionName = "WebRead_LoginedReturnUrl";

            /// <summary>
            /// 用户登录成功后跳转到阅读页面
            /// </summary>
            public const string LoginedChapterInfo_SessionName = "WebRead_LoginedChapterInfo";

            //public const string LoginedChapterInfo_CookieName = "web_crldcinfo";

            /// <summary>
            /// UV/PV
            /// </summary>
            public const string UserId_SessionName = "WebRead_UserId";

            public const string UserLog_CookieName = "web_crudinfo";
            public const string UserLog_Cookie_Id = "id";

            /// <summary>
            /// 小说最近阅读
            /// </summary>
            public const string NovelRecentRead_CookieName = "web_nrtrlInfo";

            /// <summary>
            /// 漫画最近阅读
            /// </summary>
            public const string CartoonRecentRead_CookieName = "web_crtrlInfo";

            /// <summary>
            /// 产生点击量
            /// </summary>
            public const string Hits_SessionName = "Web_Hits";
        }

        #endregion 加密key

        #region 第三方登录

        /// <summary>
        /// 第三方登录
        /// </summary>
        public enum AccessUserPlatform
        {
            /// <summary>
            /// 二维码
            /// </summary>
            qrcode = 1,

            /// <summary>
            /// 手机号码
            /// </summary>
            phone = 2,

            qq = 3,
            weibo = 4,
            wechat = 5
        };

        #endregion 第三方登录

        #region 用户

        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 用户
            /// </summary>
            user = 0,

            /// <summary>
            /// 作者
            /// </summary>
            author = 1,

            owner = 2
        };

        #endregion 用户

        #region 小说

        public struct Novel
        {
            /// <summary>
            /// 连载状态
            /// end=已完结，serial=连载
            /// </summary>
            public enum UpdateStatus
            {
                end = 1, serial = 2
            }

            /// <summary>
            /// 连载状态中文
            /// end=已完结，serial=连载
            /// </summary>
            public enum UpdateStatusName
            {
                已完结 = 1, 连载中 = 2
            }

            /// <summary>
            /// 计费方式
            /// free=免费，novel=按本，chapter=按章，novelchapter=按本按章
            /// </summary>
            public enum FeeType
            {
                free = 0, novel = 1, chapter = 2, novelchapter = novel | chapter
            }

            /// <summary>
            /// 计费方式
            /// free=免费，fee=按本/按章
            /// </summary>
            public enum FeeTypeFilter
            {
                free = 1, fee = 2
            }

            /// <summary>
            /// 排序
            /// 关键字，下载，收藏，评论，推荐，最热（点击、人气），相关推荐（分类），最新，畅销，字数，完本，打赏
            /// </summary>
            public enum SortBy
            {
                none, keyword, download, favorite, comment, recommend, hot, classrelation, newest, consume, wordsize, end, reward
            }

            /// <summary>
            /// 章节（向前/当前/向后）
            /// </summary>
            public enum ChapterDirection
            {
                none, pre, next
            };

            /// <summary>
            /// 小说列表显示（来源）
            /// </summary>
            public enum ShowSourceType
            {
                wap = 1,
                pc = 2,
                android = 4,
                iphone = 8
            };

            /// <summary>
            /// 小说列表显示（位置）
            /// </summary>
            public enum ShowLocation
            {
                /// <summary>
                /// 小说分类列表
                /// </summary>
                booklist = 1,

                /// <summary>
                /// 小说搜索列表
                /// </summary>
                searchlist = 2,

                /// <summary>
                /// 小说排行榜
                /// </summary>
                ranklist = 4
            };

            /// <summary>
            /// 推荐内容类型
            /// </summary>
            public enum FuncType
            {
                novel = 1, cartoon = 2
            }

            /// <summary>
            /// 作品内容类型
            /// </summary>
            public enum ContentType
            {
                小说 = 0, 听书 = 1, 漫画 = 2
            }

            /// <summary>
            /// 阅读顺序
            /// </summary>
            public enum ReadDirection
            {
                从左往右 = 1, 从右往左 = 2,
            }

            /// <summary>
            /// 版权状态
            /// </summary>
            public enum CopyrightStatus
            {
                首发 = 1, 独家 = 2, 授权 = 4
            }

            /// <summary>
            /// 作品类型
            /// </summary>
            public enum CartoonType
            {
                故事漫画 = 1, 故事绘本 = 2, 四格多格 = 3, 条漫 = 4
            }

            #region 男女频

            public enum ClassSpeType
            {
                male = 1,
                female = 2
            };

            public enum ClassSpeTypeName
            {
                男频 = 1,
                女频 = 2
            };

            #endregion 男女频

            #region 推广

            public enum PromotionType
            {
                none = 0, pmc = 1, rk = 2
            }

            #endregion 推广
        }

        /// <summary>
        /// CookieId
        /// </summary>
        public const string NovelCookieId = "YDV2_User_History";

        #endregion 小说

        #region 排序

        /// <summary>
        /// 排序
        /// asc/desc
        /// </summary>
        public enum SortDirection
        {
            asc, desc
        }

        #endregion 排序

        #region 状态

        /// <summary>
        /// 审核状态
        /// 否 = 0，是 = 1
        /// </summary>
        public enum Status
        {
            no = 0, yes = 1
        };

        #endregion 状态

        #region 支付方式

        /// <summary>
        ///  alipay = 1, sms = 2, wechat = 3, appstore = 4, paynow = 5, rdo = 6, wechat_qr = 7, woread = 13
        ///  商户号：1111, 2222, 3333, 4444, 5555, 6666, 7777, 8888
        /// </summary>
        public enum PayType
        {
            alipay = 1, sms = 2, wechat = 3, appstore = 4, paynow = 5, rdo = 6, wechat_qr = 7, paynow_alipay = 8, paynow_wechat = 9, paynow_bank = 10, paynow_pointcard = 11, paynow_rechargecard = 12, woread = 13, paynow_wechatqr = 14
        };

        public enum PayTypeName
        {
            支付宝 = 1, 短信 = 2, 微信 = 3, 苹果 = 4, 现在支付 = 5, 移动阅读支付 = 6, 微信扫码 = 7, 现在支付_支付宝 = 8, 现在支付_微信 = 9, 现在支付_网银 = 10, 现在支付_点卡 = 11, 现在支付_充值卡 = 12, 联通阅读支付 = 13, 现在支付_微信扫码 = 14
        };

        #endregion 支付方式

        #region 广告位类别

        public enum AdFuncType
        {
            link = 1, novel = 2, notice = 3, chapter = 4, cartoon = 5, cartoonchapter = 6
        }

        public enum AdFuncTypeName
        {
            超链接 = 1, 小说 = 2, 公告 = 3, 章节 = 4, 漫画 = 5, 漫画章节 = 6
        }

        #endregion 广告位类别

        #region 公告类型

        /// <summary>
        ///公告类型
        /// </summary>
        public enum NoticeClass
        {
            notice = 1, welfare = 2//, 私信 =3
        }

        #endregion 公告类型

        #region 活动

        /// <summary>
        /// 专题类型
        /// </summary>
        public enum PackageType { LimitFree = 1, Monthly = 4, Topic = 5, Rebate = 3, Free = 2 }

        /// <summary>
        /// 专题类型名称
        /// </summary>
        public enum PackageTypeName { 限时免费 = 1, 免费活动 = 2, 打折活动 = 3, 包月活动 = 4, 专题活动 = 5 }

        #endregion 活动
    }
}