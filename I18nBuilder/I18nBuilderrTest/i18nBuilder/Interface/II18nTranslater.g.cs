
namespace I18nBuilderrTest.I18nBuilder.Interface
{
    public interface II18nTranslater
    {
        II18nTranslation I18nTranslation { get; }

        void SetValue(II18nTranslation value);
    }
}