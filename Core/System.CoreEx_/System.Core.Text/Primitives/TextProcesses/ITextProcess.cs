//[assembly: Instinct.Pattern.Environment.Attribute.FactoryConfiguration("textProcess", typeof(Instinct.Primitive.TextProcessBase))]
namespace System.Primitives
{
    /// <summary>
    /// Abstract class used as a base for all text merging types in Instinct.
    /// </summary>

    public interface ITextProcess
    {
        string Process(string[] text, Nattrib attrib);
        string[] Tokenize(string text, Nattrib attrib);
    }
}