
using ConsoleApp1.I18nBuilder.Interface;

namespace ConsoleApp1.I18nBuilder
{
    public partial class Common:II18nTranslation  
    {
        public string this[string key] 
        {
            get
            {
                return key switch
                {
                    "Unit" => this.Unit,
                    "ExceptionMsg" => this.ExceptionMsg,
                    _=>key
                };
            }
        }
        public string Unit { get; set; }
        public string ExceptionMsg { get; set; }
    }
}