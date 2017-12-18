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

namespace NFX.WinForms.Elements
{
  public abstract class TextElement : Element
  {
    #region .ctor

    public TextElement(ElementHostControl host)
      : base(host)
    {
    }

    #endregion


    #region Private Fields

    private string m_Text;

    #endregion


    #region Properties

    public string Text
    {
      get { return m_Text ?? string.Empty; }
      set
      {
        m_Text = value;
        OnTextChanged(EventArgs.Empty);
        Invalidate();
      }
    }

    #endregion


    #region Events

    public event EventHandler TextChanged;
    #endregion

    #region Public


    #endregion


    #region Protected

    protected virtual void OnTextChanged(EventArgs e)
    {
      if (TextChanged != null) TextChanged(this, e);
    }

    #endregion


    #region Private Utils


    #endregion

  }

}
