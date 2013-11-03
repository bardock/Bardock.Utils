
namespace Bardock.Utils.Web.Mvc.Validation
{
	public class DocumentFileExtensions : HttpFileExtensionsAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentFileExtensions" /> class.
		/// </summary>
		public DocumentFileExtensions() 
            : base()
		{
            //TODO: init Extensions prop

			//Extensions = Core.Helpers.ConfigSection.Default.Documents.Extensions.Replace("|", ",");
		}
	}
}