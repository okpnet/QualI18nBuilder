
using WpfBlazorI18nTest.I18nBuilder.Interface;

namespace WpfBlazorI18nTest.I18nBuilder
{
    public partial class Page1:II18nTranslation  
    {
        public string this[string key] 
        {
            get
            {
                return key switch
                {
                    "Name" => this.Name,
                    "Label" => this.Label,
                    _=>key
                };
            }
        }
        public string Name { get; set; }
        public string Label { get; set; }
    }
}