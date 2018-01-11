/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2018 Agnicore Inc. portions ITAdapter Corp. Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
</FILE_LICENSE>*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFX.CodeAnalysis
{

    /// <summary>
    /// Performs parsing of token streams provided by lexers
    /// </summary>
    public abstract class Parser<TLexer> : CommonCodeProcessor, IParser  where TLexer : ILexer
    {
        protected Parser(IAnalysisContext context, IEnumerable<TLexer> input,  MessageList messages = null, bool throwErrors = false) :
          base(context, messages, throwErrors)

        {
              m_Input = input.ToList();
        }


        private bool m_HasParsed;
        private List<TLexer> m_Input;

        /// <summary>
        /// Returns lexers that feed this parser
        /// </summary>
        public IEnumerable<TLexer> Input { get { return m_Input; } }

        /// <summary>
        /// Lists source lexers that supply token stream for parsing
        /// </summary>
        public IEnumerable<ILexer> SourceInput { get { return (IEnumerable<ILexer>)m_Input; } }

        /// <summary>
        /// Indicates whether Parse() already happened
        /// </summary>
        public bool HasParsed { get {return m_HasParsed;} }


        /// <summary>
        /// Performs parsing if it has not been performed yet
        /// </summary>
        public void Parse()
        {
            try
            {
                DoParse();
            }
            finally
            {
                m_HasParsed = true;
            }
        }


        /// <summary>
        /// Override to perform actual parsing
        /// </summary>
        protected abstract void DoParse();

    }
}