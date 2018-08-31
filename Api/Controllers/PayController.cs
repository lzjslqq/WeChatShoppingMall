using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;
using Common;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.TenPayLibV3;
using Service;
using Utility;

namespace Api.Controllers
{
    public class PayController : ApiController
    {
        private readonly IOrderService _orderService;

        public PayController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        ///  注：若响应的 xml 格式有误，将会导致通知微信不成功，导致频繁通知。
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public HttpResponseMessage Notify()
        {
            var respData = new Dictionary<string, string>();

            try
            {
                // 读取微信支付返回结果
                var resp = ReadFromInputStream();
                var dict = ReadXml(resp);

                LogHelper.Info("\n\nnotify 接收内容：\n" + JsonHelper.Serialize(dict));

                // 微信订单号
                string transactionId, tradeNo;
                dict.TryGetValue("transaction_id", out transactionId);
                dict.TryGetValue("out_trade_no", out tradeNo);

                #region 1. 检查支付结果中transaction_id是否存在

                if (string.IsNullOrEmpty(transactionId))
                {
                    //若transaction_id不存在，则立即返回结果给微信支付后台
                    respData.Add("return_code", "FAIL");
                    respData.Add("return_msg", "支付结果中微信订单号不存在");

                    var xml = DictToXml(respData);
                    LogHelper.Info("\n\nnotify 响应内容：\n" + xml);

                    return RetMessage(xml);
                }

                #endregion

                #region 2. 同步查询订单，判断订单真实性

                var appId = ConfigurationManager.AppSettings["appId"];
                var mchId = ConfigurationManager.AppSettings["mchId"];
                var key = ConfigurationManager.AppSettings["appKey"];

                var reqData = new TenPayV3OrderQueryRequestData(appId, mchId, transactionId, TenPayV3Util.GetNoncestr(), "", key);
                var queryData = TenPayV3.OrderQuery(reqData);

                if (queryData.result_code == "SUCCESS" && queryData.result_code == "SUCCESS")
                {//查询订单成功

                    // 业务逻辑：更新订单状态
                    if (_orderService.UpdateOrderStatus(tradeNo, (int)Enums.OrderStatus.未发货))
                    {
                        respData.Add("return_code", "SUCCESS");
                        respData.Add("return_msg", "OK");

                        var xml = DictToXml(respData);
                        LogHelper.Info("\n\nnotify 响应内容：\n" + xml);

                        return RetMessage(xml);
                    }
                    else
                    {
                        respData.Add("return_code", "FAIL");
                        respData.Add("return_msg", "订单状态修改失败");

                        var xml = DictToXml(respData);
                        LogHelper.Info("\n\nnotify 响应内容：\n" + xml);

                        return RetMessage(xml);
                    }
                }
                else
                {//若订单查询失败，则立即返回结果给微信支付后台
                    respData.Add("return_code", "FAIL");
                    respData.Add("return_msg", "订单查询失败");

                    var xml = DictToXml(respData);
                    LogHelper.Info("\n\nnotify 响应内容：\n" + xml);

                    return RetMessage(xml);
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Info("\n\nnotify 异常：\n", ex);
                respData.Add("return_code", "FAIL");
                respData.Add("return_msg", "系统异常：" + ex.Message);

                var xml = DictToXml(respData);
                return RetMessage(xml);
            }
        }

        private string ReadFromInputStream()
        {
            var stream = HttpContext.Current.Request.InputStream;

            int count;
            var buffer = new byte[1024];
            var builder = new StringBuilder();
            while ((count = stream.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            stream.Flush();
            stream.Close();
            stream.Dispose();

            return builder.ToString();
        }

        private Dictionary<string, string> ReadXml(string xml)
        {
            var dict = new Dictionary<string, string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            // 获取到根节点<xml>
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList nodes = xmlNode.ChildNodes;

            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                dict[xe.Name.Trim()] = HttpUtility.UrlDecode(xe.InnerText.Trim());
            }

            return dict;
        }

        private string DictToXml(Dictionary<string, string> dict)
        {
            var xml = new StringBuilder("<xml>");
            Dictionary<string, string> sortedDic = dict.OrderBy(a => a.Key).ToDictionary(a => a.Key, b => b.Value);

            foreach (KeyValuePair<string, string> pair in sortedDic)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                {
                    int intVal;
                    xml.AppendFormat(
                        int.TryParse(pair.Value, out intVal) ? "<{0}>{1}</{0}>" : "<{0}><![CDATA[{1}]]></{0}>",
                        pair.Key,
                        HttpUtility.UrlEncode(pair.Value)
                    );
                }
            }

            return xml.Append("</xml>").ToString();
        }

        private HttpResponseMessage RetMessage(string msg)
        {
            var resp = Request.CreateResponse();
            resp.Content = new StringContent(msg, new UTF8Encoding(false), "text/plain");
            return resp;
        }
        
    }
}