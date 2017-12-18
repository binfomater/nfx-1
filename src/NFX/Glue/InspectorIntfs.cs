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
using NFX.Environment;
using NFX.Glue.Protocol;

namespace NFX.Glue
{
    /// <summary>
    /// Denotes an entity that can inspect messages
    /// </summary>
    public interface IMsgInspector : INamed, IOrdered, IConfigurable
    {

    }

    /// <summary>
    /// Inspects messages on the client side. ClientInspectors may be registered on ClientEndPoint, Binding or Glue levels
    /// </summary>
    public interface IClientMsgInspector : IMsgInspector
    {
       /// <summary>
       /// Intercepts client call during dispatch and optionaly allows to change the RequestMsg
       /// </summary>
       RequestMsg ClientDispatchCall(ClientEndPoint endpoint, RequestMsg request);

       /// <summary>
       /// Intercepts server response message before it arrives into CallSlot and optionaly allows to change it
       /// </summary>
       ResponseMsg ClientDeliverResponse(CallSlot callSlot, ResponseMsg response);
    }

    /// <summary>
    /// Inspects messages on the server side. ServerInspectors may be registered on ServerEndPoint, Binding or Glue levels
    /// </summary>
    public interface IServerMsgInspector : IMsgInspector
    {
       /// <summary>
       /// Intercepts RequestMsg that arrived from particular ServerEndPoint and optionaly allows to change it
       /// </summary>
       RequestMsg ServerDispatchRequest(ServerEndPoint endpoint, RequestMsg request);

       /// <summary>
       /// Intercepts ResponseMsg generated by server before it is sent to client and optionaly allows to change it
       /// </summary>
       ResponseMsg ServerReturnResponse(ServerEndPoint endpoint, RequestMsg request, ResponseMsg response);
    }


    /// <summary>
    /// Provides general configuration reading logic for message inspectors
    /// </summary>
    public static class MsgInspectorConfigurator
    {
       #region CONSTS

            public const string CONFIG_SERVER_INSPECTORS_SECTION = "server-inspectors";
            public const string CONFIG_CLIENT_INSPECTORS_SECTION = "client-inspectors";

            public const string CONFIG_INSPECTOR_SECTION = "inspector";

            public const string CONFIG_NAME_ATTR = "name";
            public const string CONFIG_POSITION_ATTR = "position";

       #endregion

       public static void ConfigureServerInspectors(Registry<IServerMsgInspector> registry, IConfigSectionNode node)
       {
         node = node[CONFIG_SERVER_INSPECTORS_SECTION];
         if (!node.Exists) return;

         foreach(var inode in node.Children.Where(c => c.IsSameName(CONFIG_INSPECTOR_SECTION)))
         {
           var si = FactoryUtils.MakeAndConfigure<IServerMsgInspector>(inode);
           registry.Register(si);
         }

       }


       public static void ConfigureClientInspectors(Registry<IClientMsgInspector> registry, IConfigSectionNode node)
       {
         node = node[CONFIG_CLIENT_INSPECTORS_SECTION];
         if (!node.Exists) return;

         foreach(var inode in node.Children.Where(c => c.IsSameName(CONFIG_INSPECTOR_SECTION)))
         {
           var ci = FactoryUtils.MakeAndConfigure<IClientMsgInspector>(inode);
           registry.Register(ci);
         }

       }

    }


}
