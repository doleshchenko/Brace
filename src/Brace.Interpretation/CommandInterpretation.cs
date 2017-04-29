using System.Collections.Generic;

namespace Brace.Interpretation
{
    public class CommandInterpretation
    {
        private static CommandInterpretation _emptyInterpretation;

        public static CommandInterpretation EmptyInterpretation => _emptyInterpretation ?? (_emptyInterpretation = new CommandInterpretation{Command = "void"});

        public string Command { get; set; }
        public string Argument { get; set; }

        /// <summary>
        /// Command parameters.
        /// The key is parameter name. The value is its setting.
        /// For instance "getcontent -decrypt[password]" - will be interpreted as 
        /// decrypt         - key
        /// password        - value
        /// 
        /// If parameter doesn't have setting the value will be string string.Empty. For instance "enumerate -visible" - will be interpeted as
        /// visible         - key
        /// string.Empty    - value
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}