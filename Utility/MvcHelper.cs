using System.Text;

//using System.Web.Mvc;

namespace Utility
{
	public static class MvcHelper
	{
		//	/// <summary>
		//	/// 生成 input Html标签
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="type">type="text/checkbox/radio......"</param>
		//	/// <param name="name">name</param>
		//	/// <param name="id">Id</param>
		//	/// <param name="class">css class</param>
		//	/// <param name="style">样式</param>
		//	/// <param name="value">value</param>
		//	/// <param name="checked">比如：type="checkbox/radio..."</param>
		//	/// <param name="readonly">只读条件</param>
		//	/// <param name="addition">自定义属性</param>
		//	/// <returns></returns>
		//	public static MvcHtmlString RenderInput(this HtmlHelper helper,
		//		string type = "",
		//		string name = "",
		//		string id = "",
		//		string @class = "",
		//		string style = "",
		//		object value = default(object),
		//		bool @checked = false,
		//		bool @readonly = false,
		//		string addition = ""
		//		)
		//	{
		//		var paraString = new StringBuilder();

		//		if (!string.IsNullOrEmpty(type))
		//			paraString.AppendFormat(" type=\"{0}\" ", type);
		//		if (!string.IsNullOrEmpty(name))
		//			paraString.AppendFormat(" name=\"{0}\" ", name);
		//		if (!string.IsNullOrEmpty(id))
		//			paraString.AppendFormat(" id=\"{0}\" ", id);
		//		if (!string.IsNullOrEmpty(@class))
		//			paraString.AppendFormat(" class=\"{0}\" ", @class);
		//		if (!string.IsNullOrEmpty(style))
		//			paraString.AppendFormat(" style=\"{0}\" ", style);
		//		if (value != null)
		//			paraString.AppendFormat(" value=\"{0}\" ", value);
		//		if (!string.IsNullOrEmpty(addition))
		//			paraString.Append(addition);
		//		if (@checked)
		//			paraString.Append(" checked=\"checked\" ");
		//		if (@readonly)
		//			paraString.Append(" readonly ");

		//		return new MvcHtmlString(string.Format(@"<input {0} />", paraString));
		//	}

		//	/// <summary>
		//	/// 生成 select option Html项
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="value">value</param>
		//	/// <param name="text">选项内容</param>
		//	/// <param name="selected"></param>
		//	/// <param name="addition">自定义属性</param>
		//	/// <returns></returns>
		//	public static MvcHtmlString RenderOption(this HtmlHelper helper,
		//		object value = default(object),
		//		string text = "",
		//		bool selected = false,
		//		string addition = ""
		//		)
		//	{
		//		var paraString = new StringBuilder();

		//		if (value != null)
		//			paraString.AppendFormat(" value=\"{0}\" ", value);
		//		if (!string.IsNullOrEmpty(addition))
		//			paraString.Append(addition);
		//		if (selected)
		//			paraString.Append(" selected=\"selected\" ");

		//		return new MvcHtmlString(string.Format(@"<option {0} >{1}</option>", paraString, text));
		//	}

		//	/// <summary>
		//	/// 显示图片
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="list"></param>
		//	/// <param name="defaultCover">默认图</param>
		//	/// <returns></returns>
		//	public static string RenderImage(this HtmlHelper helper,
		//		string[] list,
		//		DefaultCover defaultCover = DefaultCover.none)
		//	{
		//		if (!list.IsNullOrEmpty())
		//		{
		//			string defaultUrl = "/content/img/default/";

		//			switch (defaultCover)
		//			{
		//				case DefaultCover.usericon:
		//					defaultUrl += "user.png";
		//					break;

		//				case DefaultCover.novellist:
		//				case DefaultCover.noveldetail:
		//				case DefaultCover.recommend:
		//					defaultUrl += "book.png";
		//					break;

		//				case DefaultCover.ad:
		//					defaultUrl += "banner.png";
		//					break;

		//				case DefaultCover.novelprops:
		//					defaultUrl += "props.png";
		//					break;

		//				case DefaultCover.rewardlevel:
		//					defaultUrl += "reward.png";
		//					break;

		//				default:
		//					defaultUrl = "";
		//					break;
		//			}

		//			return StringHelper.GetImage(list, StringHelper.PrefixUrl, defaultUrl);
		//		}

		//		return "";
		//	}

		//	/// <summary>
		//	/// 返回首个非空字符串（获取推荐位标题，描述等）
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="list"></param>
		//	/// <returns></returns>
		//	public static string RenderText(this HtmlHelper helper, string[] list, int length = 15)
		//	{
		//		if (list.IsNullOrEmpty()) return "";

		//		foreach (var item in list)
		//		{
		//			if (!string.IsNullOrEmpty(item))
		//				return item;
		//		}

		//		return "";
		//	}

		//	/// <summary>
		//	/// 显示文本
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="text"></param>
		//	/// <returns></returns>
		//	public static string RenderText(this HtmlHelper helper, string text, int length = 15)
		//	{
		//		return text;
		//	}

		//	/// <summary>
		//	/// 显示Meta
		//	/// </summary>
		//	/// <param name="helper"></param>
		//	/// <param name="value"></param>
		//	/// <returns></returns>
		//	public static string RenderPrefixMeta(this HtmlHelper helper, string value)
		//	{
		//		return string.IsNullOrEmpty(value) ? "" : value + ",";
		//	}
		//}

		//public enum DefaultCover
		//{
		//	none, usericon, novellist, noveldetail, ad, recommend, novelprops, rewardlevel
		//};
	}
}